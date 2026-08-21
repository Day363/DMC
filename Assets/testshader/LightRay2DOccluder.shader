Shader "Hidden/LightRay2DOccluder"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float4 _MainTex_ST;
    float  _OccCutoff;

    struct appdata
    {
        float4 vertex : POSITION;
        float2 uv     : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float2 uv  : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    v2f vert(appdata v)
    {
        v2f o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
        o.pos = UnityObjectToClipPos(v.vertex);
        o.uv  = TRANSFORM_TEX(v.uv, _MainTex);
        return o;
    }

    fixed4 fragAlpha(v2f i) : SV_Target
    {
        float a = step(_OccCutoff, tex2D(_MainTex, i.uv).a);
        return fixed4(a, a, a, a);
    }

    fixed4 fragSolid(v2f i) : SV_Target
    {
        return fixed4(1, 1, 1, 1);
    }
    ENDCG

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Pass
        {
            Cull Off ZWrite Off ZTest Always
            BlendOp Max
            Blend One One

            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment fragAlpha
            ENDCG
        }
    }

    SubShader
    {
        Tags { "RenderType" = "TransparentCutout" }
        Pass
        {
            Cull Off ZWrite Off ZTest Always
            BlendOp Max
            Blend One One

            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment fragAlpha
            ENDCG
        }
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Pass
        {
            Cull Off ZWrite Off ZTest Always
            BlendOp Max
            Blend One One

            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment fragSolid
            ENDCG
        }
    }

    Fallback Off
}
