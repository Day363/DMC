using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public partial class LightRay2D
{
    public const int PassMask = 0;
    public const int PassScatter = 1;
    public const int PassComposite = 2;

    public static class ShaderIds
    {
        public static readonly int RayTex = Shader.PropertyToID("_RayTex");
        public static readonly int SunUv = Shader.PropertyToID("_SunUV");
        public static readonly int FalloffUv = Shader.PropertyToID("_ProxUV");
        public static readonly int Threshold = Shader.PropertyToID("_Threshold");
        public static readonly int Softness = Shader.PropertyToID("_Softness");
        public static readonly int Radius = Shader.PropertyToID("_SunRadius");
        public static readonly int Aspect = Shader.PropertyToID("_Aspect");
        public static readonly int Density = Shader.PropertyToID("_Density");
        public static readonly int Decay = Shader.PropertyToID("_Decay");
        public static readonly int Exposure = Shader.PropertyToID("_Exposure");
        public static readonly int Tint = Shader.PropertyToID("_RayTint");
        public static readonly int Strength = Shader.PropertyToID("_RayStrength");
        public static readonly int SceneColor = Shader.PropertyToID("_SceneColor");
        public static readonly int DarknessData = Shader.PropertyToID("_DarkData");
        public static readonly int DarknessTints = Shader.PropertyToID("_DarkTints");
        public static readonly int Illumination = Shader.PropertyToID("_Illum");
        public static readonly int OccluderMask = Shader.PropertyToID("_OccTex");
        public static readonly int UseOccluderMask = Shader.PropertyToID("_UseOcc");
        public static readonly int OccluderShadowing = Shader.PropertyToID("_OccShadow");
        public static readonly int OccluderCutoff = Shader.PropertyToID("_OccCutoff");
    }

    const string RayShaderName = "LightRay2D";
    const string OccluderShaderName = "LightRay2DOccluder";
    const float NegligibleContribution = 0.002f;

    public static bool IsUsingSrp
    {
#if UNITY_2022_2_OR_NEWER
        get => GraphicsSettings.defaultRenderPipeline != null || QualitySettings.renderPipeline != null;
#else
        get => GraphicsSettings.renderPipelineAsset != null || QualitySettings.renderPipeline != null;
#endif
    }

    public static bool UrpFeatureSeen { get; private set; }

    public static void NotifyUrpFeatureActive() => UrpFeatureSeen = true;

    public static LightRay2D ActiveFor(Camera camera)
    {
        for (int i = 0; i < Active.Count; i++)
            if (Active[i].ResolvedCamera == camera) return Active[i];
        return null;
    }

    public List<LightSource> ActiveLights => _visible;

    public void GetBufferSize(int sourceWidth, int sourceHeight, out int width, out int height)
    {
        int scale = Mathf.Clamp(downsample, 1, 6);
        width = Mathf.Max(2, sourceWidth / scale);
        height = Mathf.Max(2, sourceHeight / scale);
    }

    public bool PrepareFrame(Camera camera, int sourceWidth, int sourceHeight, out Material material)
    {
        material = RayMaterial;
        if (material == null)
        {
            ClearVisible();
            return false;
        }

        if (CollectVisibleLights(camera, (float)sourceWidth / Mathf.Max(1, sourceHeight)) == 0) return false;

        ApplyQualityKeyword(material);
        return true;
    }

    public void ApplyShared(CommandBuffer cmd)
    {
        cmd.SetGlobalFloat(ShaderIds.Threshold, threshold);
        cmd.SetGlobalFloat(ShaderIds.Softness, softness);
        cmd.SetGlobalFloat(ShaderIds.Aspect, _aspect);
        cmd.SetGlobalFloat(ShaderIds.SceneColor, sceneColorBleed);
        cmd.SetGlobalVectorArray(ShaderIds.DarknessData, _darknessData);
        cmd.SetGlobalVectorArray(ShaderIds.DarknessTints, _darknessTints);
        cmd.SetGlobalFloat(ShaderIds.UseOccluderMask, UsingOccluderMask ? 1f : 0f);
        cmd.SetGlobalFloat(ShaderIds.OccluderShadowing, occluderShadowing);
        cmd.SetGlobalTexture(ShaderIds.OccluderMask, OccluderMaskOrBlack);
    }

    public void ApplyLight(CommandBuffer cmd, int index)
    {
        LightSource light = _visible[index];
        cmd.SetGlobalVector(ShaderIds.SunUv, _visibleUv[index]);
        cmd.SetGlobalVector(ShaderIds.FalloffUv, _visibleFalloff[index]);
        cmd.SetGlobalFloat(ShaderIds.Radius, light.radius);
        cmd.SetGlobalFloat(ShaderIds.Density, light.density);
        cmd.SetGlobalFloat(ShaderIds.Decay, light.decay);
        cmd.SetGlobalFloat(ShaderIds.Exposure, light.exposure);
        cmd.SetGlobalColor(ShaderIds.Tint, light.tint);
        cmd.SetGlobalFloat(ShaderIds.Strength, light.strength * masterStrength);
        cmd.SetGlobalFloat(ShaderIds.Illumination, light.illumination);
    }

    public void ApplyComposite(CommandBuffer cmd)
    {
        cmd.SetGlobalColor(ShaderIds.Tint, Color.white);
        cmd.SetGlobalFloat(ShaderIds.Strength, 1f);
    }

    readonly List<LightSource> _visible = new List<LightSource>();
    readonly List<Vector2> _visibleUv = new List<Vector2>();
    readonly List<Vector2> _visibleFalloff = new List<Vector2>();
    readonly List<Renderer> _hiddenForOccluderPass = new List<Renderer>();
    readonly List<Transform> _excludedCached = new List<Transform>();
    readonly List<Renderer[]> _excludedRenderers = new List<Renderer[]>();

    Material _material;
    Quality _appliedQuality = (Quality)(-1);
    Material _appliedQualityMaterial;
    float _aspect = 1f;
    readonly Vector4[] _darknessData = new Vector4[MaxLights];
    readonly Vector4[] _darknessTints = new Vector4[MaxLights];

    Shader _occluderShader;
    Camera _occluderCamera;
    RenderTexture _occluderMask;

    public bool UseExternalOccluderMask { get; set; }

    public Shader OccluderShader
    {
        get
        {
            if (_occluderShader == null)
                _occluderShader = occluderShader != null ? occluderShader : Shader.Find("Hidden/" + OccluderShaderName);
            return _occluderShader;
        }
    }

    public float OccluderAlphaCutoff => occluderAlphaCutoff;

    bool UsingOccluderMask =>
        occlusion == Occlusion.Silhouette && (_occluderMask != null || UseExternalOccluderMask);

    Texture OccluderMaskOrBlack =>
        occlusion == Occlusion.Silhouette && _occluderMask != null ? _occluderMask : (Texture)Texture2D.blackTexture;

    public LayerMask OccluderLayers => occluderLayers;

    Material RayMaterial
    {
        get
        {
            if (_material != null) return _material;

            Shader shader = rayShader != null ? rayShader : Shader.Find("Hidden/" + RayShaderName);
            if (shader == null || !shader.isSupported) return null;

            _material = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };
            return _material;
        }
    }

    void ClearVisible()
    {
        _visible.Clear();
        _visibleUv.Clear();
        _visibleFalloff.Clear();
    }

    int CollectVisibleLights(Camera camera, float aspect)
    {
        ClearVisible();
        for (int i = 0; i < MaxLights; i++)
        {
            _darknessData[i] = Vector4.zero;
            _darknessTints[i] = Vector4.zero;
        }
        if (camera == null || lights == null) return 0;

        _aspect = aspect;

        for (int i = 0; i < lights.Count && _visible.Count < MaxLights; i++)
        {
            LightSource light = lights[i];
            if (light == null || !light.enabled) continue;

            // A darkening light stays active even while it casts no rays, so its shadow
            // does not pop off when the light fades out or drifts beyond its reach.
            bool darkens = light.darkness > 0f;
            if (!darkens && (light.strength <= 0f || masterStrength <= 0f)) continue;

            Vector2 uv = light.viewport;
            if (light.follow != null)
            {
                if (!light.follow.gameObject.activeInHierarchy) continue;

                Vector3 viewportPoint = camera.WorldToViewportPoint(light.follow.position);
                if (viewportPoint.z < 0f) continue;
                uv = new Vector2(viewportPoint.x, viewportPoint.y);
            }

            Vector2 onScreen = new Vector2(Mathf.Clamp01(uv.x), Mathf.Clamp01(uv.y));
            Vector2 falloffCenter = light.distant ? onScreen : uv;

            if (!light.distant)
            {
                bool reachedByRays = ContributionAt(uv, onScreen, light) >= NegligibleContribution;
                bool reachedByDark = darkens && OnScreenDistance(uv, onScreen) < DarknessRange(light);
                if (!reachedByRays && !reachedByDark) continue;
            }

            if (darkens)
            {
                _darknessData[_visible.Count] = new Vector4(
                    falloffCenter.x, falloffCenter.y, light.darkness, DarknessRange(light));
                _darknessTints[_visible.Count] = new Vector4(
                    light.darknessTint.r, light.darknessTint.g, light.darknessTint.b, 0f);
            }

            _visible.Add(light);
            _visibleUv.Add(uv);
            _visibleFalloff.Add(falloffCenter);
        }

        return _visible.Count;
    }

    const float UnboundedDarknessRange = 1e5f;

    static float DarknessRange(LightSource light) =>
        light.darknessRange >= DarknessRangeFull - 1e-3f ? UnboundedDarknessRange : light.darknessRange;

    float OnScreenDistance(Vector2 uv, Vector2 onScreen)
    {
        float dx = (onScreen.x - uv.x) * _aspect;
        float dy = onScreen.y - uv.y;
        return Mathf.Sqrt(dx * dx + dy * dy);
    }

    float ContributionAt(Vector2 uv, Vector2 onScreen, LightSource light)
    {
        float dx = (onScreen.x - uv.x) * _aspect;
        float dy = onScreen.y - uv.y;
        float radius = Mathf.Max(light.radius, 1e-4f);
        float reach = Mathf.Exp(-(dx * dx + dy * dy) / (radius * radius));
        return reach * light.strength * masterStrength;
    }

    void ApplyQualityKeyword(Material material)
    {
        if (_appliedQuality == quality && _appliedQualityMaterial == material) return;

        material.DisableKeyword("RAYS_LOW");
        material.DisableKeyword("RAYS_MED");
        material.DisableKeyword("RAYS_HIGH");
        material.EnableKeyword(quality switch
        {
            Quality.Low => "RAYS_LOW",
            Quality.High => "RAYS_HIGH",
            _ => "RAYS_MED",
        });

        _appliedQuality = quality;
        _appliedQualityMaterial = material;
    }

    void ApplyShared(Material material)
    {
        material.SetFloat(ShaderIds.Threshold, threshold);
        material.SetFloat(ShaderIds.Softness, softness);
        material.SetFloat(ShaderIds.Aspect, _aspect);
        material.SetFloat(ShaderIds.SceneColor, sceneColorBleed);
        material.SetVectorArray(ShaderIds.DarknessData, _darknessData);
        material.SetVectorArray(ShaderIds.DarknessTints, _darknessTints);
        material.SetFloat(ShaderIds.UseOccluderMask, UsingOccluderMask ? 1f : 0f);
        material.SetFloat(ShaderIds.OccluderShadowing, occluderShadowing);
        material.SetTexture(ShaderIds.OccluderMask, OccluderMaskOrBlack);
    }

    void ApplyLight(Material material, int index)
    {
        LightSource light = _visible[index];
        material.SetVector(ShaderIds.SunUv, _visibleUv[index]);
        material.SetVector(ShaderIds.FalloffUv, _visibleFalloff[index]);
        material.SetFloat(ShaderIds.Radius, light.radius);
        material.SetFloat(ShaderIds.Density, light.density);
        material.SetFloat(ShaderIds.Decay, light.decay);
        material.SetFloat(ShaderIds.Exposure, light.exposure);
        material.SetColor(ShaderIds.Tint, light.tint);
        material.SetFloat(ShaderIds.Strength, light.strength * masterStrength);
        material.SetFloat(ShaderIds.Illumination, light.illumination);
    }

    void ApplyComposite(Material material)
    {
        material.SetColor(ShaderIds.Tint, Color.white);
        material.SetFloat(ShaderIds.Strength, 1f);
    }

    void RenderRays(Camera camera, RenderTexture source, RenderTexture destination)
    {
        if (!PrepareFrame(camera, source.width, source.height, out Material material))
        {
            Graphics.Blit(source, destination);
            return;
        }

        GetBufferSize(source.width, source.height, out int width, out int height);
        // Illumination accumulates in alpha, so the ray buffers need a format that keeps it.
        RenderTextureFormat format = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf)
            ? RenderTextureFormat.ARGBHalf : source.format;
        RenderTexture mask = RenderTexture.GetTemporary(width, height, 0, format);
        RenderTexture rays = RenderTexture.GetTemporary(width, height, 0, format);
        mask.filterMode = FilterMode.Bilinear;
        rays.filterMode = FilterMode.Bilinear;
        ClearToTransparent(rays);

        ApplyShared(material);
        for (int i = 0; i < _visible.Count; i++)
        {
            ApplyLight(material, i);
            Graphics.Blit(source, mask, material, PassMask);
            Graphics.Blit(mask, rays, material, PassScatter);
        }

        ApplyComposite(material);
        material.SetTexture(ShaderIds.RayTex, rays);
        Graphics.Blit(source, destination, material, PassComposite);

        RenderTexture.ReleaseTemporary(mask);
        RenderTexture.ReleaseTemporary(rays);
    }

    static void ClearToTransparent(RenderTexture target)
    {
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = target;
        GL.Clear(false, true, Color.clear);
        RenderTexture.active = previous;
    }

    void RenderOccluderMask(Camera source)
    {
        if (occlusion != Occlusion.Silhouette || source == null ||
            occluderLayers.value == 0 || CollectVisibleLights(source, source.aspect) == 0)
        {
            ReleaseOccluderMask();
            return;
        }

        if (_occluderShader == null)
        {
            _occluderShader = occluderShader != null ? occluderShader : Shader.Find("Hidden/" + OccluderShaderName);
            if (_occluderShader == null) return;
        }

        GetBufferSize(Mathf.Max(2, source.pixelWidth), Mathf.Max(2, source.pixelHeight), out int width, out int height);
        EnsureOccluderMask(width, height);
        SyncOccluderCamera(source);

        Shader.SetGlobalFloat(ShaderIds.OccluderCutoff, occluderAlphaCutoff);
        SetLightObjectsVisible(false);
        try { _occluderCamera.RenderWithShader(_occluderShader, "RenderType"); }
        finally
        {
            SetLightObjectsVisible(true);
            _occluderCamera.targetTexture = null;
        }
    }

    void EnsureOccluderMask(int width, int height)
    {
        if (_occluderMask != null && _occluderMask.width == width && _occluderMask.height == height) return;

        ReleaseOccluderMask();
        _occluderMask = new RenderTexture(width, height, 0, RenderTextureFormat.R8)
        {
            name = "LightRay2D Occluders",
            hideFlags = HideFlags.HideAndDontSave,
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
        };
        _occluderMask.Create();
    }

    void SyncOccluderCamera(Camera source)
    {
        if (_occluderCamera == null)
        {
            var holder = new GameObject("LightRay2D Occluder Camera") { hideFlags = HideFlags.HideAndDontSave };
            _occluderCamera = holder.AddComponent<Camera>();
            _occluderCamera.enabled = false;
        }

        _occluderCamera.transform.SetPositionAndRotation(source.transform.position, source.transform.rotation);
        _occluderCamera.orthographic = source.orthographic;
        _occluderCamera.orthographicSize = source.orthographicSize;
        _occluderCamera.fieldOfView = source.fieldOfView;
        _occluderCamera.nearClipPlane = source.nearClipPlane;
        _occluderCamera.farClipPlane = source.farClipPlane;
        _occluderCamera.aspect = source.aspect;
        _occluderCamera.cullingMask = occluderLayers;
        _occluderCamera.clearFlags = CameraClearFlags.SolidColor;
        _occluderCamera.backgroundColor = Color.clear;
        _occluderCamera.useOcclusionCulling = false;
        _occluderCamera.allowHDR = false;
        _occluderCamera.allowMSAA = false;
        _occluderCamera.depthTextureMode = DepthTextureMode.None;
        _occluderCamera.targetTexture = _occluderMask;
    }

    void SetLightObjectsVisible(bool visible)
    {
        if (visible)
        {
            for (int i = 0; i < _hiddenForOccluderPass.Count; i++)
                if (_hiddenForOccluderPass[i] != null) _hiddenForOccluderPass[i].enabled = true;
            _hiddenForOccluderPass.Clear();
            return;
        }

        _hiddenForOccluderPass.Clear();

        if (lights != null)
        {
            for (int i = 0; i < lights.Count; i++)
            {
                LightSource light = lights[i];
                if (light?.follow == null) continue;

                if (light.followCached != light.follow)
                {
                    light.followRenderers = light.follow.GetComponentsInChildren<Renderer>(true);
                    light.followCached = light.follow;
                }

                Hide(light.followRenderers);
            }
        }

        RefreshExclusionCache();
        for (int i = 0; i < _excludedRenderers.Count; i++)
            Hide(_excludedRenderers[i]);
    }

    void Hide(Renderer[] renderers)
    {
        if (renderers == null) return;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null || !renderers[i].enabled) continue;
            renderers[i].enabled = false;
            _hiddenForOccluderPass.Add(renderers[i]);
        }
    }

    void RefreshExclusionCache()
    {
        int count = occluderExclusions?.Count ?? 0;

        if (_excludedCached.Count == count)
        {
            bool changed = false;
            for (int i = 0; i < count; i++)
                if (_excludedCached[i] != occluderExclusions[i]) { changed = true; break; }
            if (!changed) return;
        }

        _excludedCached.Clear();
        _excludedRenderers.Clear();

        for (int i = 0; i < count; i++)
        {
            Transform target = occluderExclusions[i];
            _excludedCached.Add(target);
            _excludedRenderers.Add(target != null ? target.GetComponentsInChildren<Renderer>(true) : null);
        }
    }

    void ReleaseOccluderMask()
    {
        if (_occluderMask == null) return;
        _occluderMask.Release();
        DestroyImmediate(_occluderMask);
        _occluderMask = null;
    }

    void ReleaseRenderResources()
    {
        if (_material != null)
        {
            DestroyImmediate(_material);
            _material = null;
        }
        _appliedQualityMaterial = null;
        _appliedQuality = (Quality)(-1);

        ReleaseOccluderMask();
        if (_occluderCamera != null)
        {
            DestroyImmediate(_occluderCamera.gameObject);
            _occluderCamera = null;
        }
    }

    [ExecuteAlways]
    [ImageEffectAllowedInSceneView]
    class CameraHook : MonoBehaviour
    {
        internal LightRay2D owner;

        Camera _camera;

        LightRay2D Target => owner != null ? owner : (Active.Count > 0 ? Active[0] : null);

        Camera Camera => _camera != null ? _camera : _camera = GetComponent<Camera>();

        void OnPreRender()
        {
            LightRay2D target = Target;
            if (target == null || !target.isActiveAndEnabled) return;
            target.RenderOccluderMask(Camera);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            LightRay2D target = Target;
            if (target == null || !target.isActiveAndEnabled)
            {
                Graphics.Blit(source, destination);
                return;
            }
            target.RenderRays(Camera, source, destination);
        }
    }
}
