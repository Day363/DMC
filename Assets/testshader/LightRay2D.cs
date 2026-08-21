using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
[DisallowMultipleComponent]
[AddComponentMenu("Effects/2D Light Ray")]
public partial class LightRay2D : MonoBehaviour
{
    public const int MaxLights = 8;
    public const float DarknessRangeFull = 2f;

    public enum Quality { Low, Medium, High }

    public enum Occlusion { Silhouette, Brightness }

    [System.Serializable]
    public class LightSource
    {
        public string label = "Sun";
        public bool enabled = true;

        [Tooltip("Drag an object here and the light follows it. Leave empty to use Viewport.")]
        public Transform follow;
        [Tooltip("Screen position. (0,0) is bottom-left, (1,1) is top-right.")]
        public Vector2 viewport = new Vector2(0.80f, 0.78f);
        [Tooltip("Sun-like light. Keeps shining in from beyond the screen edge no matter how far it goes.\n" +
                 "Turn off for lamps: they fade out with distance and are skipped entirely when far away.")]
        public bool distant = true;

        [ColorUsage(false)] public Color tint = new Color(1f, 0.72f, 0.38f);
        [Range(0f, 3f)] public float strength = 0.85f;
        [Tooltip("How strongly this light clears darkness. 0 keeps the glow but leaves " +
                 "its surroundings dark, 2 lights them up twice as fast.")]
        [Range(0f, 2f)] public float illumination = 1f;
        [Tooltip("While this light is enabled, everything within Darkness Range that its rays cannot reach " +
                 "darkens by this much. 0 leaves the scene untouched. Where darkness overlaps, the strongest wins.")]
        [Range(0f, 1f)] public float darkness = 0f;
        [Tooltip("Colour the shadows sink toward at full darkness. Black is plain shading, a deep blue reads as night.")]
        [ColorUsage(false)] public Color darknessTint = new Color(0.05f, 0.07f, 0.12f);
        [Tooltip("How far the darkening reaches from the light, in screen heights. " +
                 "At the far end it covers the whole screen.")]
        [Range(0.1f, DarknessRangeFull)] public float darknessRange = DarknessRangeFull;
        [Tooltip("Size of the light source itself. Small means sharp shafts, large means a broad glow.")]
        [Range(0.02f, 1f)] public float radius = 0.14f;

        [Tooltip("How far the rays reach.")]
        [Range(0.1f, 1.5f)] public float density = 0.92f;
        [Range(0.90f, 0.999f)] public float decay = 0.968f;
        [Range(0f, 8f)] public float exposure = 3.0f;

        [System.NonSerialized] internal Renderer[] followRenderers;
        [System.NonSerialized] internal Transform followCached;

        public LightSource() { }

        public LightSource(string label, Vector2 viewport, Color tint)
        {
            this.label = label;
            this.viewport = viewport;
            this.tint = tint;
        }
    }

    [Header("Lights")]
    [Tooltip("Multiplies every light. FadeTo() drives this.")]
    [Range(0f, 2f)] public float masterStrength = 1f;
    public List<LightSource> lights = new List<LightSource> { new LightSource() };

    [Header("Occlusion")]
    [Tooltip("Silhouette: draws occluders once more and blocks light by their shape. Ignores colour and texture.\n" +
             "Brightness: guesses from screen brightness. No extra draw, but bright objects cannot block.")]
    public Occlusion occlusion = Occlusion.Silhouette;
    [Tooltip("Silhouette only. Layers that block light. Keep backgrounds out of this.")]
    public LayerMask occluderLayers = ~0;
    [Tooltip("Silhouette only. These never block light even if their layer is included. Children count too.")]
    public List<Transform> occluderExclusions = new List<Transform>();
    [Tooltip("Silhouette only. Sprite pixels with alpha above this count as solid. Raise it if light leaks " +
             "through window frames or soft edges, lower it if thin details stop blocking.")]
    [Range(0.01f, 1f)] public float occluderAlphaCutoff = 0.25f;
    [Tooltip("Silhouette only. 0 lets light bleed over occluder edges, 1 keeps them as hard silhouettes.")]
    [Range(0f, 1f)] public float occluderShadowing = 1f;
    [Tooltip("Brightness only. Pixels darker than this block light. Keep it below your background.")]
    [Range(0f, 2f)] public float threshold = 0.10f;
    [Tooltip("Brightness only.")]
    [Range(0.01f, 1f)] public float softness = 0.25f;

    [Header("Look")]
    [Tooltip("0 tints rays with the light colour only. 1 lets the scene's own colour through.")]
    [Range(0f, 1f)] public float sceneColorBleed = 0f;

    [Header("Performance")]
    [Tooltip("Samples per pixel: Low 24, Medium 40, High 64.")]
    public Quality quality = Quality.Medium;
    [Tooltip("Ray buffer scale. Higher is cheaper and softer.")]
    [Range(1, 6)] public int downsample = 3;

    [Header("Target")]
    [Tooltip("Leave empty to use Camera.main.")]
    public Camera targetCamera;
    public bool persistAcrossScenes;

    [Header("Shaders")]
    [Tooltip("Filled in automatically. Assign by hand only if the asset was moved and they came up empty.")]
    [SerializeField] Shader rayShader;
    [SerializeField] Shader occluderShader;

    public static LightRay2D Instance { get; private set; }

    public Camera BoundCamera => _boundCamera;

    public Camera ResolvedCamera
    {
        get
        {
            if (targetCamera != null) return targetCamera;
            if (_mainCamera == null) _mainCamera = Camera.main;
            return _mainCamera;
        }
    }

    public int LightCount => lights != null ? lights.Count : 0;

    public LightSource GetLight(int index)
    {
        return lights != null && index >= 0 && index < lights.Count ? lights[index] : null;
    }

    public LightSource GetLight(string label)
    {
        if (lights == null) return null;
        for (int i = 0; i < lights.Count; i++)
            if (lights[i] != null && lights[i].label == label) return lights[i];
        return null;
    }

    public LightSource AddLight(LightSource light)
    {
        if (light == null) return null;
        lights ??= new List<LightSource>();
        lights.Add(light);
        return light;
    }

    public void RemoveLight(int index)
    {
        if (lights != null && index >= 0 && index < lights.Count) lights.RemoveAt(index);
    }

    public void SetLightEnabled(int index, bool on)
    {
        LightSource light = GetLight(index);
        if (light != null) light.enabled = on;
    }

    public void SetSun(Transform sun) => SetSun(0, sun);

    public void SetSun(int index, Transform sun)
    {
        LightSource light = GetLight(index);
        if (light != null) light.follow = sun;
    }

    public void SetSunViewport(Vector2 viewport) => SetSunViewport(0, viewport);

    public void SetSunViewport(int index, Vector2 viewport)
    {
        LightSource light = GetLight(index);
        if (light == null) return;
        light.follow = null;
        light.viewport = viewport;
    }

    public void SetEffectEnabled(bool on) => enabled = on;

    public void FadeTo(float target, float seconds)
    {
        target = Mathf.Clamp(target, 0f, 2f);
        if (seconds <= 0f)
        {
            _fading = false;
            masterStrength = target;
            return;
        }
        _fadeFrom = masterStrength;
        _fadeTarget = target;
        _fadeDuration = seconds;
        _fadeElapsed = 0f;
        _fading = true;
    }

    public void RefreshLightRenderers()
    {
        _excludedCached.Clear();
        _excludedRenderers.Clear();

        if (lights == null) return;
        for (int i = 0; i < lights.Count; i++)
            if (lights[i] != null) lights[i].followCached = null;
    }

    public void Rebind()
    {
        _boundCamera = null;
        Bind();
    }

    static readonly List<LightRay2D> Active = new List<LightRay2D>();

    Camera _mainCamera;
    Camera _boundCamera;
    CameraHook _hook;

    bool _fading;
    float _fadeFrom, _fadeTarget, _fadeDuration, _fadeElapsed;

    void OnEnable()
    {
        Active.Add(this);

        if (Application.isPlaying)
        {
            if (Instance != null && Instance != this)
            {
                enabled = false;
                return;
            }
            Instance = this;

            if (persistAcrossScenes && transform.parent == null)
                DontDestroyOnLoad(gameObject);
        }

        Bind();
    }

    void OnDisable()
    {
        Active.Remove(this);
        if (Instance == this) Instance = null;

        Unbind();
        ReleaseRenderResources();
    }

    void Reset() => ResolveShaders();

    void OnValidate()
    {
        ResolveShaders();
        if (isActiveAndEnabled) Bind();
    }

    void ResolveShaders()
    {
#if UNITY_EDITOR
        rayShader = rayShader != null ? rayShader : FindShaderAsset("LightRay2D");
        occluderShader = occluderShader != null ? occluderShader : FindShaderAsset("LightRay2DOccluder");
#endif
    }

#if UNITY_EDITOR
    static Shader FindShaderAsset(string fileName)
    {
        foreach (string guid in AssetDatabase.FindAssets($"{fileName} t:Shader"))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (System.IO.Path.GetFileNameWithoutExtension(path) != fileName) continue;
            return AssetDatabase.LoadAssetAtPath<Shader>(path);
        }
        return null;
    }
#endif

    void LateUpdate()
    {
        Bind();

        if (!_fading || !Application.isPlaying) return;

        _fadeElapsed += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(_fadeElapsed / _fadeDuration);
        masterStrength = Mathf.Lerp(_fadeFrom, _fadeTarget, t);
        if (t >= 1f) _fading = false;
    }

    void Bind()
    {
        Camera camera = ResolvedCamera;
        if (camera == _boundCamera && _hook != null) return;

        Unbind();
        if (camera == null) return;

        _hook = camera.GetComponent<CameraHook>();
        if (_hook == null)
        {
            _hook = camera.gameObject.AddComponent<CameraHook>();
            _hook.hideFlags = HideFlags.HideInInspector | HideFlags.DontSave;
        }
        _hook.owner = this;
        _boundCamera = camera;
    }

    void Unbind()
    {
        if (_hook != null)
        {
            if (Application.isPlaying) Destroy(_hook);
            else DestroyImmediate(_hook);
        }
        _hook = null;
        _boundCamera = null;
    }

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(LightRay2D.LightSource))]
class LightRay2DLightDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty name = property.FindPropertyRelative("label");
        SerializedProperty enabled = property.FindPropertyRelative("enabled");
        SerializedProperty tint = property.FindPropertyRelative("tint");

        EditorGUI.BeginProperty(position, label, property);

        float line = EditorGUIUtility.singleLineHeight;
        var header = new Rect(position.x, position.y, position.width - 46f, line);
        var swatch = new Rect(position.xMax - 40f, position.y + 3f, 16f, line - 6f);
        var toggle = new Rect(position.xMax - 18f, position.y + 1f, 16f, line);

        string title = string.IsNullOrEmpty(name.stringValue) ? "Light" : name.stringValue;
        property.isExpanded = EditorGUI.Foldout(header, property.isExpanded, title, true);
        EditorGUI.DrawRect(swatch, tint.colorValue);
        enabled.boolValue = EditorGUI.Toggle(toggle, enabled.boolValue);

        if (property.isExpanded)
        {
            using (new EditorGUI.DisabledScope(!enabled.boolValue))
            {
                EditorGUI.indentLevel++;
                float y = position.y + line + EditorGUIUtility.standardVerticalSpacing;
                foreach (SerializedProperty child in VisibleFields(property))
                {
                    float height = EditorGUI.GetPropertyHeight(child, true);
                    EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), child, true);
                    y += height + EditorGUIUtility.standardVerticalSpacing;
                }
                EditorGUI.indentLevel--;
            }
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;
        if (!property.isExpanded) return height;

        foreach (SerializedProperty child in VisibleFields(property))
            height += EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(child, true);
        return height;
    }

    static IEnumerable<SerializedProperty> VisibleFields(SerializedProperty property)
    {
        bool follows = property.FindPropertyRelative("follow").objectReferenceValue != null;
        bool darkens = property.FindPropertyRelative("darkness").floatValue > 0f;

        SerializedProperty iterator = property.Copy();
        SerializedProperty end = iterator.GetEndProperty();
        bool enterChildren = true;

        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, end))
        {
            enterChildren = false;
            if (iterator.name == "enabled") continue;
            if (iterator.name == "viewport" && follows) continue;
            if ((iterator.name == "darknessTint" || iterator.name == "darknessRange") && !darkens) continue;
            yield return iterator.Copy();
        }
    }
}
#endif
