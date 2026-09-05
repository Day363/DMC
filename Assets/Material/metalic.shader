Shader "Custom/SpriteMetalColor"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

        _MetalColor ("Target Color", Color) = (0.5, 0.5, 0.5, 1)
        _ColorTolerance ("Color Tolerance", Range(0, 1)) = 0.1

        _MetalDark ("Metal Dark", Color) = (0.08, 0.08, 0.08, 1)
        _MetalBright ("Metal Bright", Color) = (0.8, 0.8, 0.8, 1)

        _MetalStrength ("Metal Strength", Range(0, 1)) = 1
        _HighlightStrength ("Highlight Strength", Range(0, 3)) = 1.5
        _NoiseScale ("Noise Scale", Float) = 20
        _Roughness ("Roughness", Range(0, 1)) = 0.35

        [PerRendererData] _Color ("Sprite Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
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

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            CBUFFER_START(UnityPerMaterial)

            float4 _MetalColor;
            float4 _MetalDark;
            float4 _MetalBright;
            float4 _Color;

            float _ColorTolerance;
            float _MetalStrength;
            float _HighlightStrength;
            float _NoiseScale;
            float _Roughness;

            CBUFFER_END


            Varyings vert(Attributes input)
            {
                Varyings output;

                output.positionHCS =
                    TransformObjectToHClip(input.positionOS.xyz);

                output.uv = input.uv;
                output.color = input.color * _Color;

                return output;
            }


            // 간단한 2D 노이즈
            float Noise(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);

                return frac(p.x * p.y);
            }


            half4 frag(Varyings input) : SV_Target
            {
                float4 tex = SAMPLE_TEXTURE2D(
                    _MainTex,
                    sampler_MainTex,
                    input.uv
                );

                float4 original = tex * input.color;


                // -----------------------------------------
                // 1. 특정 색상인지 판단
                // -----------------------------------------

                float colorDistance =
                    distance(original.rgb, _MetalColor.rgb);

                float metalMask =
                    1.0 - smoothstep(
                        0.0,
                        _ColorTolerance,
                        colorDistance
                    );

                metalMask *= original.a;


                // -----------------------------------------
                // 2. 금속 표면용 노이즈
                // -----------------------------------------

                float noise =
                    Noise(input.uv * _NoiseScale);

                noise = lerp(
                    1.0 - _Roughness,
                    1.0,
                    noise
                );


                // -----------------------------------------
                // 3. 금속 밝기
                // -----------------------------------------

                // UV 기준으로 대각선 방향의 금속 하이라이트
                float highlight =
                    sin(
                        (input.uv.x + input.uv.y) * 18.0
                    );

                highlight =
                    smoothstep(
                        0.25,
                        0.9,
                        highlight
                    );

                highlight *= _HighlightStrength;

                highlight *= noise;


                // -----------------------------------------
                // 4. 철 색상
                // -----------------------------------------

                float metalValue =
                    saturate(
                        0.35 +
                        highlight * 0.65
                    );

                float3 metalColor =
                    lerp(
                        _MetalDark.rgb,
                        _MetalBright.rgb,
                        metalValue
                    );


                // -----------------------------------------
                // 5. 원본과 금속 효과 혼합
                // -----------------------------------------

                float3 finalColor =
                    lerp(
                        original.rgb,
                        metalColor,
                        metalMask * _MetalStrength
                    );


                return float4(
                    finalColor,
                    original.a
                );
            }

            ENDHLSL
        }
    }
}