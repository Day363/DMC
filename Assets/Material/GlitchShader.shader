Shader "Custom/GlitchEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Base Color", Color) = (1, 1, 1, 1)

        [Header(Glitch Settings)]
        _GlitchIntensity ("Glitch Intensity", Range(0, 1)) = 0.3
        _GlitchSpeed ("Glitch Speed", Range(0, 20)) = 5.0
        _GlitchFrequency ("Glitch Frequency", Range(0, 50)) = 10.0

        [Header(Block Glitch)]
        _BlockSize ("Block Size", Range(0.01, 0.5)) = 0.05
        _BlockIntensity ("Block Displacement", Range(0, 0.3)) = 0.1

        [Header(Scanline)]
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.3
        _ScanlineSpeed ("Scanline Speed", Range(0, 10)) = 2.0
        _ScanlineDensity ("Scanline Density", Range(10, 200)) = 80.0

        [Header(RGB Split)]
        _RGBSplitAmount ("RGB Split Amount", Range(0, 0.1)) = 0.02
        _RGBSplitSpeed ("RGB Split Speed", Range(0, 20)) = 8.0

        [Header(Noise Flicker)]
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.15
        _FlickerSpeed ("Flicker Speed", Range(0, 30)) = 10.0

        [Header(Color Corruption)]
        _ColorCorruption ("Color Corruption", Range(0, 1)) = 0.2
        _CorruptionSpeed ("Corruption Speed", Range(0, 10)) = 3.0

        [Header(Edge Distortion)]
        _EdgeDistortion ("Edge Distortion", Range(0, 1)) = 0.2
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float4 pos    : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            float _GlitchIntensity;
            float _GlitchSpeed;
            float _GlitchFrequency;

            float _BlockSize;
            float _BlockIntensity;

            float _ScanlineIntensity;
            float _ScanlineSpeed;
            float _ScanlineDensity;

            float _RGBSplitAmount;
            float _RGBSplitSpeed;

            float _NoiseIntensity;
            float _FlickerSpeed;

            float _ColorCorruption;
            float _CorruptionSpeed;

            float _EdgeDistortion;

            // ─────────────────────────────────────────────
            // Utility: Hash / Noise helpers
            // ─────────────────────────────────────────────
            float hash11(float n)
            {
                return frac(sin(n) * 43758.5453123);
            }

            float hash12(float2 p)
            {
                return frac(sin(dot(p, float2(127.1, 311.7))) * 43758.5453);
            }

            float2 hash21(float p)
            {
                float2 q = float2(dot(float2(p, p), float2(127.1, 311.7)),
                                  dot(float2(p, p), float2(269.5, 183.3)));
                return frac(sin(q) * 43758.5453);
            }

            // Value noise [0,1]
            float valueNoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float2 u = f * f * (3.0 - 2.0 * f);

                float a = hash12(i + float2(0,0));
                float b = hash12(i + float2(1,0));
                float c = hash12(i + float2(0,1));
                float d = hash12(i + float2(1,1));

                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            // ─────────────────────────────────────────────
            // Glitch helpers
            // ─────────────────────────────────────────────

            // Returns a "trigger" probability — 1 = glitch on, 0 = off
            float glitchTrigger(float seed, float speed, float frequency)
            {
                float t = floor(_Time.y * speed) * 0.1;
                float n = hash11(seed + t);
                return step(1.0 - frequency * 0.02, n);
            }

            // Horizontal block displacement
            float2 blockGlitch(float2 uv)
            {
                float t   = floor(_Time.y * _GlitchSpeed);
                float row = floor(uv.y / _BlockSize);
                float rnd = hash12(float2(row, t));

                // Only trigger on some rows
                float trigger = step(0.7, rnd) * _GlitchIntensity;
                float disp    = (hash11(row + t * 7.3) - 0.5) * 2.0 * _BlockIntensity;
                uv.x += disp * trigger;
                return uv;
            }

            // Wavy distortion along the edge
            float2 edgeDistortion(float2 uv)
            {
                float t   = _Time.y * 3.0;
                float dist = sin(uv.y * _GlitchFrequency + t) * 0.5 + 0.5;
                dist = pow(dist, 4.0);
                float edge = (1.0 - abs(uv.x - 0.5) * 2.0); // centre = 1, edges = 0
                edge = 1.0 - edge;                            // invert: edges = 1
                uv.x += sin(uv.y * 30.0 + t * 5.0) * _EdgeDistortion * 0.05 * edge;
                return uv;
            }

            // ─────────────────────────────────────────────
            v2f vert(appdata v)
            {
                v2f o;
                // Vertex-level glitch: randomly snap rows of verts
                float3 wp = mul(unity_ObjectToWorld, v.vertex).xyz;
                float row = floor(v.uv.y / _BlockSize);
                float t   = floor(_Time.y * _GlitchSpeed);
                float trigger = glitchTrigger(row * 13.7, _GlitchSpeed, _GlitchFrequency);
                float disp = (hash11(row + t) - 0.5) * _BlockIntensity * trigger * _GlitchIntensity;
                v.vertex.x += disp;

                o.pos      = UnityObjectToClipPos(v.vertex);
                o.uv       = TRANSFORM_TEX(v.uv, _MainTex);
                o.color    = v.color;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // ── 1. Block glitch UV displacement ──────
                uv = blockGlitch(uv);

                // ── 2. Edge distortion ───────────────────
                //uv = edgeDistortion(uv);

                // ── 3. RGB chromatic aberration split ────
                float rgbTime  = floor(_Time.y * _RGBSplitSpeed);
                float rgbTrig  = step(0.6, hash11(rgbTime * 3.7)) * _GlitchIntensity;
                float splitAmt = _RGBSplitAmount * rgbTrig;
                float noiseRow = hash11(floor(uv.y * 20.0) + rgbTime);
                float2 splitDir = float2(splitAmt * (noiseRow - 0.5) * 4.0, 0.0);

                float r = tex2D(_MainTex, uv + splitDir).r;
                float g = tex2D(_MainTex, uv).g;
                float b = tex2D(_MainTex, uv - splitDir).b;
                float a = tex2D(_MainTex, uv).a;

                float4 col = float4(r, g, b, a) * _Color * i.color;

                // ── 4. Scanlines ─────────────────────────
                float scanY    = uv.y * _ScanlineDensity + _Time.y * _ScanlineSpeed;
                float scanline = sin(scanY * 3.14159) * 0.5 + 0.5;
                scanline = pow(scanline, 2.0);
                col.rgb -= scanline * _ScanlineIntensity * 0.5;

                // ── 5. Pixel noise / static ───────────────
                float2 noiseUV = floor(uv * 512.0) / 512.0;
                float  noiseSeed = floor(_Time.y * _FlickerSpeed);
                float  noise = hash12(noiseUV + noiseSeed);
                float  noiseTrig = step(0.55, hash11(noiseSeed * 5.1)) * _GlitchIntensity;
                col.rgb += (noise - 0.5) * _NoiseIntensity * noiseTrig;

                // ── 6. Color corruption (channel swap) ───
                float corrTime = floor(_Time.y * _CorruptionSpeed);
                float corrTrig = step(0.85, hash11(corrTime * 9.3)) * _GlitchIntensity;
                float corrRow  = step(0.5, hash11(floor(uv.y * 30.0) + corrTime));
                float3 swapped = col.rgb.brg;
                col.rgb = lerp(col.rgb, swapped, corrTrig * corrRow * _ColorCorruption);

                // ── 7. Horizontal tear lines ──────────────
                float tearTime = floor(_Time.y * _GlitchSpeed * 0.5);
                float tearY    = hash11(tearTime * 7.1);
                float tearWidth = 0.005 + hash11(tearTime) * 0.02;
                float tear = step(abs(uv.y - tearY), tearWidth);
                col.rgb += tear * _GlitchIntensity * float3(0.9, 0.1, 0.1);
                uv.x += tear * (hash11(tearTime * 3.3) - 0.5) * 0.3 * _GlitchIntensity;

                // ── 8. Brightness flicker ─────────────────
                float flickTime = floor(_Time.y * _FlickerSpeed * 2.0);
                float flicker   = lerp(1.0, hash11(flickTime * 4.7), _GlitchIntensity * _NoiseIntensity);
                col.rgb *= flicker;

                // ── 9. Vignette for atmosphere ────────────
                float2 vigUV = uv - 0.5;
                float  vig   = 1.0 - dot(vigUV * 1.5, vigUV * 1.5);
                vig = saturate(vig);
                col.rgb *= vig * 0.4 + 0.6;

                col.rgb = saturate(col.rgb);
                return col;
            }
            ENDCG
        }
    }

    Fallback "Sprites/Default"
}
