#if LIGHTRAY2D_URP
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.Rendering.RenderGraphModule;
#endif

[DisallowMultipleRendererFeature("2D Light Ray")]
public class LightRay2DFeature : ScriptableRendererFeature
{
    [SerializeField]
    [Tooltip("Before post-processing lets URP bloom pick up the rays. After keeps colours closer to the source.")]
    RenderPassEvent injectionPoint = RenderPassEvent.BeforeRenderingPostProcessing;

    LightRay2DPass _pass;

    public override void Create()
    {
        _pass = new LightRay2DPass { renderPassEvent = injectionPoint };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (_pass == null) return;

        Camera camera = renderingData.cameraData.camera;
        if (camera == null || camera.cameraType != CameraType.Game) return;

        LightRay2D effect = LightRay2D.ActiveFor(camera);
        if (effect == null || !effect.isActiveAndEnabled) return;

        LightRay2D.NotifyUrpFeatureActive();
        _pass.renderPassEvent = injectionPoint;
        _pass.Setup(effect);
        renderer.EnqueuePass(_pass);
    }
}

class LightRay2DPass : ScriptableRenderPass
{
    const string ProfilerTag = "2D Light Ray";

    LightRay2D _effect;

    public LightRay2DPass()
    {
#if UNITY_6000_0_OR_NEWER
        requiresIntermediateTexture = true;
#endif
    }

    public void Setup(LightRay2D effect) => _effect = effect;

#if UNITY_6000_0_OR_NEWER
    static readonly List<ShaderTagId> SpriteTags = new List<ShaderTagId>
    {
        new ShaderTagId("SRPDefaultUnlit"),
        new ShaderTagId("UniversalForward"),
        new ShaderTagId("UniversalForwardOnly"),
        new ShaderTagId("Universal2D"),
    };

    Material _occluderMaterial;

    class PassData
    {
        public Material material;
        public LightRay2D effect;
        public int lightCount;
        public TextureHandle source, sceneCopy, mask, rays, occluders;
        public bool hasOccluders;
    }

    class OccluderPassData
    {
        public RendererListHandle list;
    }

    TextureHandle RenderOccluders(RenderGraph renderGraph, ContextContainer frameData,
                                  LightRay2D effect, int width, int height)
    {
        if (effect.occlusion != LightRay2D.Occlusion.Silhouette || effect.OccluderLayers.value == 0)
            return TextureHandle.nullHandle;

        Shader shader = effect.OccluderShader;
        if (shader == null) return TextureHandle.nullHandle;

        if (_occluderMaterial == null || _occluderMaterial.shader != shader)
            _occluderMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };

        var renderingData = frameData.Get<UniversalRenderingData>();
        var cameraData = frameData.Get<UniversalCameraData>();
        var lightData = frameData.Get<UniversalLightData>();

        DrawingSettings drawing = RenderingUtils.CreateDrawingSettings(
            SpriteTags, renderingData, cameraData, lightData, SortingCriteria.None);
        drawing.overrideMaterial = _occluderMaterial;
        drawing.overrideMaterialPassIndex = 0;

        var filtering = new FilteringSettings(RenderQueueRange.all, effect.OccluderLayers);
        var listParams = new RendererListParams(renderingData.cullResults, drawing, filtering);
        RendererListHandle list = renderGraph.CreateRendererList(listParams);

        var desc = new TextureDesc(width, height)
        {
            name = "LightRay2D Occluders",
            format = GraphicsFormat.R8_UNorm,
            clearBuffer = true,
            clearColor = Color.clear,
            filterMode = FilterMode.Bilinear,
            wrapMode = TextureWrapMode.Clamp,
        };
        TextureHandle mask = renderGraph.CreateTexture(desc);

        using (var builder = renderGraph.AddRasterRenderPass<OccluderPassData>("LightRay2D Occluders", out OccluderPassData data))
        {
            data.list = list;
            builder.UseRendererList(list);
            builder.SetRenderAttachment(mask, 0);
            builder.AllowPassCulling(false);
            builder.SetRenderFunc((OccluderPassData d, RasterGraphContext context) =>
                context.cmd.DrawRendererList(d.list));
        }

        Shader.SetGlobalFloat(LightRay2D.ShaderIds.OccluderCutoff, effect.OccluderAlphaCutoff);
        return mask;
    }

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        if (_effect == null) return;

        var resources = frameData.Get<UniversalResourceData>();
        var cameraData = frameData.Get<UniversalCameraData>();
        if (resources == null || cameraData == null) return;

        TextureHandle source = resources.activeColorTexture;
        if (!source.IsValid()) return;

        TextureDesc sourceDesc = renderGraph.GetTextureDesc(source);
        if (sourceDesc.width < 4 || sourceDesc.height < 4) return;

        if (!_effect.PrepareFrame(cameraData.camera, sourceDesc.width, sourceDesc.height, out Material material)) return;
        _effect.GetBufferSize(sourceDesc.width, sourceDesc.height, out int width, out int height);

        TextureHandle occluders = RenderOccluders(renderGraph, frameData, _effect, width, height);
        _effect.UseExternalOccluderMask = occluders.IsValid();

        TextureDesc copyDesc = sourceDesc;
        copyDesc.name = "LightRay2D Scene Copy";
        copyDesc.clearBuffer = false;
        copyDesc.msaaSamples = MSAASamples.None;
        copyDesc.depthBufferBits = 0;
        copyDesc.filterMode = FilterMode.Bilinear;
        copyDesc.wrapMode = TextureWrapMode.Clamp;
        TextureHandle sceneCopy = renderGraph.CreateTexture(copyDesc);

        TextureDesc bufferDesc = copyDesc;
        bufferDesc.width = width;
        bufferDesc.height = height;
        // Illumination accumulates in alpha; the camera target may be alpha-less (B10G11R11).
        bufferDesc.format = GraphicsFormat.R16G16B16A16_SFloat;
        bufferDesc.name = "LightRay2D Mask";
        TextureHandle mask = renderGraph.CreateTexture(bufferDesc);
        bufferDesc.name = "LightRay2D Rays";
        TextureHandle rays = renderGraph.CreateTexture(bufferDesc);

        using (var builder = renderGraph.AddUnsafePass<PassData>(ProfilerTag, out PassData data))
        {
            data.material = material;
            data.effect = _effect;
            data.lightCount = _effect.ActiveLights.Count;
            data.source = source;
            data.sceneCopy = sceneCopy;
            data.mask = mask;
            data.rays = rays;
            data.occluders = occluders;
            data.hasOccluders = occluders.IsValid();

            builder.UseTexture(source, AccessFlags.ReadWrite);
            builder.UseTexture(sceneCopy, AccessFlags.ReadWrite);
            builder.UseTexture(mask, AccessFlags.ReadWrite);
            builder.UseTexture(rays, AccessFlags.ReadWrite);
            if (occluders.IsValid()) builder.UseTexture(occluders, AccessFlags.Read);
            builder.AllowPassCulling(false);

            builder.SetRenderFunc((PassData d, UnsafeGraphContext context) =>
            {
                CommandBuffer cmd = CommandBufferHelpers.GetNativeCommandBuffer(context.cmd);
                RTHandle source = d.source, sceneCopy = d.sceneCopy, mask = d.mask, rays = d.rays;

                cmd.Blit(source, sceneCopy);
                cmd.SetRenderTarget(rays);
                cmd.ClearRenderTarget(false, true, Color.clear);

                d.effect.ApplyShared(cmd);
                if (d.hasOccluders)
                {
                    RTHandle occluders = d.occluders;
                    cmd.SetGlobalTexture(LightRay2D.ShaderIds.OccluderMask, occluders);
                }
                for (int i = 0; i < d.lightCount; i++)
                {
                    d.effect.ApplyLight(cmd, i);
                    cmd.Blit(sceneCopy, mask, d.material, LightRay2D.PassMask);
                    cmd.Blit(mask, rays, d.material, LightRay2D.PassScatter);
                }

                d.effect.ApplyComposite(cmd);
                cmd.SetGlobalTexture(LightRay2D.ShaderIds.RayTex, rays);
                cmd.Blit(sceneCopy, source, d.material, LightRay2D.PassComposite);
            });
        }
    }
#else
    static readonly int SceneCopyId = Shader.PropertyToID("_LightRay2DSceneCopy");
    static readonly int MaskId = Shader.PropertyToID("_LightRay2DMask");
    static readonly int RaysId = Shader.PropertyToID("_LightRay2DRays");

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (_effect == null) return;

        ref CameraData cameraData = ref renderingData.cameraData;
        RenderTextureDescriptor sourceDesc = cameraData.cameraTargetDescriptor;
        if (sourceDesc.width < 4 || sourceDesc.height < 4) return;

        if (!_effect.PrepareFrame(cameraData.camera, sourceDesc.width, sourceDesc.height, out Material material)) return;
        _effect.GetBufferSize(sourceDesc.width, sourceDesc.height, out int width, out int height);

#if UNITY_2022_2_OR_NEWER
        RenderTargetIdentifier source = cameraData.renderer.cameraColorTargetHandle;
#else
        RenderTargetIdentifier source = cameraData.renderer.cameraColorTarget;
#endif

        RenderTextureDescriptor copyDesc = sourceDesc;
        copyDesc.depthBufferBits = 0;
        copyDesc.msaaSamples = 1;

        RenderTextureDescriptor bufferDesc = copyDesc;
        bufferDesc.width = width;
        bufferDesc.height = height;
        // Illumination accumulates in alpha; the camera target may be alpha-less (B10G11R11).
        bufferDesc.graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;

        CommandBuffer cmd = CommandBufferPool.Get(ProfilerTag);
        cmd.GetTemporaryRT(SceneCopyId, copyDesc, FilterMode.Bilinear);
        cmd.GetTemporaryRT(MaskId, bufferDesc, FilterMode.Bilinear);
        cmd.GetTemporaryRT(RaysId, bufferDesc, FilterMode.Bilinear);

        cmd.Blit(source, SceneCopyId);
        cmd.SetRenderTarget(RaysId);
        cmd.ClearRenderTarget(false, true, Color.clear);

        _effect.ApplyShared(cmd);
        int lightCount = _effect.ActiveLights.Count;
        for (int i = 0; i < lightCount; i++)
        {
            _effect.ApplyLight(cmd, i);
            cmd.Blit(SceneCopyId, MaskId, material, LightRay2D.PassMask);
            cmd.Blit(MaskId, RaysId, material, LightRay2D.PassScatter);
        }

        _effect.ApplyComposite(cmd);
        cmd.SetGlobalTexture(LightRay2D.ShaderIds.RayTex, RaysId);
        cmd.Blit(SceneCopyId, source, material, LightRay2D.PassComposite);

        cmd.ReleaseTemporaryRT(SceneCopyId);
        cmd.ReleaseTemporaryRT(MaskId);
        cmd.ReleaseTemporaryRT(RaysId);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
#endif
}
#endif

#if UNITY_EDITOR
namespace LightRay2DInternal
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
#if UNITY_2023_1_OR_NEWER
    using UnityEditor.Build;
#endif

    [InitializeOnLoad]
    static class UrpDefineSync
    {
        const string Define = "LIGHTRAY2D_URP";
        const string UrpMarkerType =
            "UnityEngine.Rendering.Universal.ScriptableRendererFeature, Unity.RenderPipelines.Universal.Runtime";

        static UrpDefineSync() => Sync();

        static void Sync()
        {
            bool urpInstalled = Type.GetType(UrpMarkerType) != null;

#if UNITY_2023_1_OR_NEWER
            NamedBuildTarget target = NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string current = PlayerSettings.GetScriptingDefineSymbols(target);
#else
            BuildTargetGroup target = EditorUserBuildSettings.selectedBuildTargetGroup;
            string current = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);
#endif
            var symbols = new List<string>(current.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            if (urpInstalled == symbols.Contains(Define)) return;

            if (urpInstalled) symbols.Add(Define);
            else symbols.Remove(Define);

            string next = string.Join(";", symbols.ToArray());
#if UNITY_2023_1_OR_NEWER
            PlayerSettings.SetScriptingDefineSymbols(target, next);
#else
            PlayerSettings.SetScriptingDefineSymbolsForGroup(target, next);
#endif
        }
    }
}
#endif
