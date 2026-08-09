Shader "Custom/MeshDissolveURP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _NoiseScale ("Noise Scale", Float) = 20
        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0

        _EdgeWidth ("Edge Width", Range(0.001, 0.5)) = 0.05
        _EdgeColor ("Edge Color", Color) = (1, 0.5, 0, 1)
        _EdgeIntensity ("Edge Intensity", Range(0, 10)) = 3
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
            };

            TEXTURE2D(_MainTex);       SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _Color;
                float _NoiseScale;
                float _DissolveAmount;
                float _EdgeWidth;
                float4 _EdgeColor;
                float _EdgeIntensity;
            CBUFFER_END

            // 2D 해시 기반 밸류 노이즈
            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            float valueNoise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float a = hash21(i);
                float b = hash21(i + float2(1, 0));
                float c = hash21(i + float2(0, 1));
                float d = hash21(i + float2(1, 1));
                float2 u = f * f * (3.0 - 2.0 * f);
                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv) * _Color;

                float noise = valueNoise(IN.uv * _NoiseScale);

                // 디졸브 클리핑
                clip(noise - _DissolveAmount);

                // 엣지 글로우
                float edge = smoothstep(0, _EdgeWidth, noise - _DissolveAmount);
                half3 edgeGlow = _EdgeColor.rgb * _EdgeIntensity * (1 - edge);

                half3 finalColor = texColor.rgb + edgeGlow;
                return half4(finalColor, texColor.a);
            }
            ENDHLSL
        }
    }
}
