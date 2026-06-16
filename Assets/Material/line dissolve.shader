Shader "Unlit/line dissolve"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0, 1)) = 0
        _BandCount ("Band Count", Float) = 10
        _NoiseSeed ("Noise Seed", Float) = 7.3
        _SlideAmount ("Slide Amount", Float) = 0.3
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "IgnoreProjector" = "True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _Dissolve;
                float _BandCount;
                float _NoiseSeed;
                float _SlideAmount;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float rand(float n)
            {
                return frac(sin(n * _NoiseSeed) * 43758.5453);
            }

            half4 frag(Varyings i) : SV_Target
            {
                float bandScaled = i.uv.y * _BandCount;
                float bandIndex  = floor(bandScaled);
                float bandLocal  = frac(bandScaled);

                // -1~1 랜덤값으로 방향+거리 동시에
                float randSlide = rand(bandIndex + 99.1) * 2.0 - 1.0;
                float slideX    = randSlide * _Dissolve * _SlideAmount;

                float2 uv = float2(i.uv.x + slideX, i.uv.y);
                half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                if (_Dissolve <= 0.0)
                    return tex * i.color;

                if (_Dissolve >= 1.0)
                    clip(-1);

                float center   = 0.2 + rand(bandIndex + 13.7) * 0.6;
                float halfSize = min(center, 1.0 - center);
                float dist     = abs(bandLocal - center) / halfSize;

                float threshold = _Dissolve * 2.0;
                clip(dist - threshold);

                return tex * i.color;
            }
            ENDHLSL
        }
    }
}