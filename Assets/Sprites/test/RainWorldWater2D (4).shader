Shader "Custom/RainWorldWater2D"
{
    Properties
    {
        _ShallowColor ("Shallow Color", Color) = (0.10, 0.30, 0.38, 0.75)
        _DeepColor    ("Deep Color",    Color) = (0.02, 0.08, 0.14, 0.95)
        _FoamColor    ("Foam Color",    Color) = (0.55, 0.75, 0.80, 1.00)
        _FoamAmount   ("Foam Amount",   Range(0, 1)) = 0.25
        _BlendOffset  ("Blend Offset",  Range(0, 1)) = 0.5
        _BlendRange   ("Blend Range",   Range(0.01, 1)) = 0.4
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

            CBUFFER_START(UnityPerMaterial)
                half4 _ShallowColor;
                half4 _DeepColor;
                half4 _FoamColor;
                float _FoamAmount;
                float _BlendOffset;
                float _BlendRange;
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
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // UV.y 1=위(얕음) 0=아래(깊음)
                // BlendOffset: 경계 위치 (0.5 = 중간)
                // BlendRange: 경계 부드러움
                float t = smoothstep(
                    _BlendOffset - _BlendRange * 0.5,
                    _BlendOffset + _BlendRange * 0.5,
                    IN.uv.y);

                half4 color = lerp(_DeepColor, _ShallowColor, t);

                // 위쪽 가장자리 폼
                float foam = smoothstep(0.7, 1.0, IN.uv.y) * _FoamAmount;
                color.rgb  = lerp(color.rgb, _FoamColor.rgb, foam);

                return color;
            }
            ENDHLSL
        }
    }
}
