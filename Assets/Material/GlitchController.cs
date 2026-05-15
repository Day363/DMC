using UnityEngine;

/// <summary>
/// GlitchController — Attach to any GameObject with a Renderer using GlitchShader.
/// Animates glitch parameters over time for a dynamic, living effect.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class GlitchController : MonoBehaviour
{
    [Header("── Glitch Burst ──────────────────────────")]
    [Tooltip("평균 몇 초마다 강한 글리치 버스트가 발생하는지")]
    public float burstInterval   = 3.0f;
    [Tooltip("버스트 지속 시간 (초)")]
    public float burstDuration   = 0.2f;
    [Tooltip("버스트 시 글리치 강도 배수")]
    public float burstMultiplier = 2.5f;

    [Header("── Idle Glitch ──────────────────────────")]
    [Tooltip("평소 글리치 강도 (0 = 없음)")]
    [Range(0f, 1f)] public float idleGlitchIntensity = 0.15f;
    [Tooltip("평소 RGB 스플릿 양")]
    [Range(0f, 0.05f)] public float idleRGBSplit     = 0.005f;

    [Header("── Pulse ────────────────────────────────")]
    [Tooltip("강도가 살짝 맥동하도록 할지 여부")]
    public bool enablePulse    = true;
    public float pulseSpeed    = 1.2f;
    public float pulseAmplitude= 0.05f;

    // ── Internal ─────────────────────────────────────
    private Renderer    _rend;
    private Material    _mat;
    private float       _burstTimer;
    private float       _nextBurst;
    private bool        _isBursting;
    private float       _burstElapsed;

    // Cached shader property IDs for performance
    private static readonly int ID_Intensity  = Shader.PropertyToID("_GlitchIntensity");
    private static readonly int ID_RGB        = Shader.PropertyToID("_RGBSplitAmount");
    private static readonly int ID_Block      = Shader.PropertyToID("_BlockIntensity");
    private static readonly int ID_Noise      = Shader.PropertyToID("_NoiseIntensity");
    private static readonly int ID_Edge       = Shader.PropertyToID("_EdgeDistortion");
    private static readonly int ID_Corruption = Shader.PropertyToID("_ColorCorruption");

    void Awake()
    {
        _rend = GetComponent<Renderer>();
        // Instance the material so we don't modify the shared asset
        _mat  = _rend.material;
        ScheduleNextBurst();
    }

    void Update()
    {
        _burstTimer += Time.deltaTime;

        // ── Burst trigger ──────────────────────────────
        if (!_isBursting && _burstTimer >= _nextBurst)
        {
            _isBursting   = true;
            _burstElapsed = 0f;
        }

        // ── Compute target intensity ───────────────────
        float intensity, rgb, block, noise, edge, corrupt;

        if (_isBursting)
        {
            _burstElapsed += Time.deltaTime;
            float t = _burstElapsed / burstDuration;

            // Sharp in, smooth out
            float envelope = Mathf.Pow(1f - Mathf.Clamp01(t), 2f);

            intensity = Mathf.Min(idleGlitchIntensity * burstMultiplier * envelope + idleGlitchIntensity, 1f);
            rgb       = idleRGBSplit   * burstMultiplier * envelope + idleRGBSplit;
            block     = 0.05f          * burstMultiplier * envelope + 0.01f;
            noise     = 0.3f           * burstMultiplier * envelope + 0.05f;
            edge      = 0.4f           * burstMultiplier * envelope;
            corrupt   = 0.5f           * burstMultiplier * envelope;

            if (_burstElapsed >= burstDuration)
            {
                _isBursting = false;
                ScheduleNextBurst();
            }
        }
        else
        {
            // Idle with optional pulse
            float pulse = enablePulse
                ? Mathf.Sin(Time.time * pulseSpeed) * pulseAmplitude
                : 0f;

            intensity = idleGlitchIntensity + pulse;
            rgb       = idleRGBSplit;
            block     = 0.01f;
            noise     = 0.05f;
            edge      = 0.05f;
            corrupt   = 0.05f;
        }

        // ── Apply to material ──────────────────────────
        _mat.SetFloat(ID_Intensity,  intensity);
        _mat.SetFloat(ID_RGB,        rgb);
        _mat.SetFloat(ID_Block,      block);
        _mat.SetFloat(ID_Noise,      noise);
        _mat.SetFloat(ID_Edge,       edge);
        _mat.SetFloat(ID_Corruption, corrupt);
    }

    // ── Public API ─────────────────────────────────────
    /// <summary>외부에서 강제로 글리치 버스트를 트리거합니다.</summary>
    public void TriggerBurst()
    {
        _isBursting   = true;
        _burstElapsed = 0f;
    }

    private void ScheduleNextBurst()
    {
        _burstTimer = 0f;
        // Random jitter ±50 % around the interval
        _nextBurst  = burstInterval * Random.Range(0.5f, 1.5f);
    }

    void OnDestroy()
    {
        if (_mat != null) Destroy(_mat);
    }
}
