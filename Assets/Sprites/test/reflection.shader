Shader "Custom/reflection"
{
    Properties
    {
        _MainTex("RenderTexture",2D)="white"{}
        _Strength("Strength",Range(0,0.1))=0.02
        _WaveScale("Wave Scale",Float)=5
        _WaveSpeed("Wave Speed",Float)=2
        _Tint("Tint",Color)=(0.1,0.3,0.6,0.5)
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "Queue"="Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _Strength;
            float _WaveScale;
            float _WaveSpeed;
            float4 _Tint;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs pos =
                    GetVertexPositionInputs(IN.positionOS.xyz);

                OUT.positionHCS = pos.positionCS;
                OUT.worldPos = pos.positionWS;
                OUT.uv = IN.uv;

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uv;

                float wave =
                    sin(IN.worldPos.x * _WaveScale +
                        _Time.y * _WaveSpeed)
                    * _Strength;

                uv.x += wave;

                // X축 반전
                uv.x = 1.0 - uv.x;

                half4 col =
                    SAMPLE_TEXTURE2D(
                        _MainTex,
                        sampler_MainTex,
                        uv);

                col.rgb =
                    lerp(
                        col.rgb,
                        _Tint.rgb,
                        _Tint.a);

                return col;
            }

            ENDHLSL
        }
    }
}