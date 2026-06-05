Shader "Custom/wireframe"
{
    Properties
    {
        _WireColor ("Wire Color", Color) = (0, 1, 1, 1)
        _FillColor ("Fill Color", Color) = (0, 0, 0, 1)
        _WireThickness ("Wire Thickness", Float) = 1.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _WireColor;
            float4 _FillColor;
            float  _WireThickness;

            struct appdata { float4 vertex : POSITION; };

            struct v2g
            {
                float4 pos : SV_POSITION;
            };

            struct g2f
            {
                float4 pos  : SV_POSITION;
                float3 barycentric : TEXCOORD0; // 삼각형 내 위치
            };

            v2g vert(appdata v)
            {
                v2g o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // Geometry 셰이더: 삼각형 꼭짓점에 무게중심 좌표 할당
            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> stream)
            {
                g2f o;
                o.pos = IN[0].pos; o.barycentric = float3(1,0,0); stream.Append(o);
                o.pos = IN[1].pos; o.barycentric = float3(0,1,0); stream.Append(o);
                o.pos = IN[2].pos; o.barycentric = float3(0,0,1); stream.Append(o);
            }

            fixed4 frag(g2f i) : SV_Target
            {
                // 무게중심 좌표의 최솟값 = 엣지까지의 거리
                float3 b = i.barycentric;
                float  edge = min(min(b.x, b.y), b.z);
                float  wire = 1.0 - smoothstep(0.0, _WireThickness * 0.01, edge);
                return lerp(_FillColor, _WireColor, wire);
            }
            ENDCG
        }
    }
}
