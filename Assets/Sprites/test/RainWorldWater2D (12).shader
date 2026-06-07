Shader "Custom/RainWorldWater2D"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.03, 0.10, 0.18, 0.5)
        _Distortion ("Distortion", Range(0,0.1)) = 0.03
    }

    SubShader
    {
        Tags
        {
            "RenderType"     = "Transparent"
            "Queue"          = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "RainWorldWater2D"
            Tags { "LightMode" = "Universal2D" }

            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_CameraSortingLayerTexture);
            SAMPLER(sampler_CameraSortingLayerTexture);

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
                float _Distortion;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float2 worldXY : TEXCOORD2;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                float3 worldPos = TransformObjectToWorld(IN.positionOS.xyz);

                OUT.positionHCS = TransformWorldToHClip(worldPos);
                OUT.screenPos = ComputeScreenPos(OUT.positionHCS);
                OUT.worldXY = worldPos.xy;
                OUT.uv = IN.uv;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                #if UNITY_UV_STARTS_AT_TOP
                    screenUV.y = 1.0 - screenUV.y;
                #endif

                float waveStrength = IN.uv.y * _Distortion;

                float distortion =
                (
                    sin(IN.worldXY.x * 3.0 + _Time.y * 2.0) +
                    sin(IN.worldXY.x * 6.0 - _Time.y * 1.3) * 0.5
                )
                * waveStrength;

                // 가로 물결 -> 화면을 위아래로 왜곡
                screenUV.y += distortion;

                half4 bg = SAMPLE_TEXTURE2D(
                    _CameraSortingLayerTexture,
                    sampler_CameraSortingLayerTexture,
                    screenUV
                );

                half3 finalColor = lerp(bg.rgb, _Color.rgb, _Color.a);

                return half4(finalColor, _Color.a);
            }

            ENDHLSL
        }
    }
}