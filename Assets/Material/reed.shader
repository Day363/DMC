Shader "Unlit/reed"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Amplitude ("Amplitude", Float) = 0.08
        _Frequency ("Frequency", Float) = 2.5
        _Speed ("Speed", Float) = 1.5
        _Stiffness ("Stiffness", Float) = 3
        _PlayerPos ("Player Position", Vector) = (0,0,0,0)
        _InfluenceRadius ("Influence Radius", Float) = 2
        _Boost ("Boost Amount", Float) = 2
        _RandomSeed ("Random Seed", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _Amplitude;
            float _Frequency;
            float _Speed;
            float _Stiffness;
            float4 _PlayerPos;
            float _InfluenceRadius;
            float _Boost;
            float _RandomSeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float rand(float2 co)
            {
                return frac(sin(dot(co, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                float height = v.uv.y;
                float weight = pow(height, _Stiffness);

                // 월드 위치
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // 플레이어 거리 기반 영향
                float dist = distance(worldPos.xy, _PlayerPos.xy);
                float influence = saturate(1.0 - dist / _InfluenceRadius);
                float finalAmplitude = _Amplitude * (1.0 + influence * _Boost);

                // 패럴랙스에 영향받지 않는 고정 랜덤 offset
                float randomOffset = rand(float2(_RandomSeed, _RandomSeed * 1.7));

                // 바람 계산
                float wave = sin(_Time.y * _Speed + v.vertex.y * _Frequency + randomOffset * 10.0);
                v.vertex.x += wave * finalAmplitude * weight;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= _Color;
                return col;
            }
            ENDHLSL
        }
    }
}
