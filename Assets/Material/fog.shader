Shader "Custom/FlowingFogProcedural"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (1,1,1,1)
        _Scale ("Noise Scale", Float) = 3.0
        _Speed1 ("Layer1 Scroll Speed", Vector) = (0.05, 0.02, 0, 0)
        _Speed2 ("Layer2 Scroll Speed", Vector) = (-0.03, 0.04, 0, 0)
        _Density ("Fog Density", Range(0,1)) = 0.5
        _EdgeSoftness ("Edge Softness", Range(0.01,1)) = 0.3
        _EdgeFeather ("Sprite Edge Feather", Range(0.01, 0.5)) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }
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
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _FogColor;
            float _Scale;
            float4 _Speed1;
            float4 _Speed2;
            float _Density;
            float _EdgeSoftness;
            float _EdgeFeather;

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            float hash(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                float2 u = f * f * (3.0 - 2.0 * f);

                float a = hash(i);
                float b = hash(i + float2(1,0));
                float c = hash(i + float2(0,1));
                float d = hash(i + float2(1,1));

                return lerp(lerp(a, b, u.x), lerp(c, d, u.x), u.y);
            }

            float fbm(float2 p)
            {
                float v = 0.0;
                float amp = 0.5;
                for (int i = 0; i < 3; i++)
                {
                    v += amp * noise(p);
                    p *= 2.0;
                    amp *= 0.5;
                }
                return v;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                float2 uv1 = IN.uv * _Scale + _Speed1.xy * _Time.y;
                float2 uv2 = IN.uv * _Scale * 1.6 + _Speed2.xy * _Time.y;

                float n1 = fbm(uv1);
                float n2 = fbm(uv2);

                float fog = n1 * n2;
                fog = smoothstep(_Density - _EdgeSoftness, _Density + _EdgeSoftness, fog);

                float2 edgeDist = min(IN.uv, 1.0 - IN.uv);
                float edgeMask = smoothstep(0.0, _EdgeFeather, edgeDist.x)
                                * smoothstep(0.0, _EdgeFeather, edgeDist.y);
                fog *= edgeMask;

                float4 col = _FogColor;
                col.a *= fog;
                return col;
            }
            ENDHLSL
        }
    }
}
