Shader "Custom/RainWorldWater2D"
{
    Properties
    {
        _Color      ("Water Color", Color)        = (0.03, 0.10, 0.18, 0.5)
        _Distortion ("Distortion",  Range(0,0.1)) = 0.03
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
            #pragma vertex   vert
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
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv          : TEXCOORD0;
                float4 screenPos   : TEXCOORD1;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv          = IN.uv;
                OUT.screenPos   = ComputeScreenPos(OUT.positionHCS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

                // URP는 플랫폼에 따라 Y 반전 필요
                #if UNITY_UV_STARTS_AT_TOP
                    screenUV.y = 1.0 - screenUV.y;
                #endif

                float wave = IN.uv.y * _Distortion;
                screenUV.x += sin(IN.uv.x * 18.0 + _Time.y * 2.0) * wave;
                screenUV.y += cos(IN.uv.x * 13.0 + _Time.y * 1.5) * wave * 0.4;

                half4 bg = SAMPLE_TEXTURE2D(_CameraSortingLayerTexture, sampler_CameraSortingLayerTexture, screenUV);

                return half4(lerp(bg.rgb, _Color.rgb, _Color.a), 1.0);
            }
            ENDHLSL
        }
    }
}
