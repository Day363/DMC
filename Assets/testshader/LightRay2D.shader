Shader "Hidden/LightRay2D"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    sampler2D _MainTex;
    sampler2D _RayTex;
    sampler2D _OccTex;

    float4 _SunUV;
    float4 _ProxUV;
    float  _Threshold;
    float  _Softness;
    float  _SunRadius;
    float  _Aspect;
    float  _Density;
    float  _Decay;
    float  _Exposure;
    float4 _RayTint;
    float  _RayStrength;
    float  _SceneColor;
    float  _UseOcc;
    float  _OccShadow;
    float4 _DarkData[8];   // per light: xy centre uv, z darkness, w range (8 = MaxLights)
    float4 _DarkTints[8];
    float  _Illum;
    ENDCG

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert_img
            #pragma fragment frag

            float4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                float3 sceneColor = tex2D(_MainTex, uv).rgb;
                float  luminance  = dot(sceneColor, float3(0.2126, 0.7152, 0.0722));

                float fromMask       = saturate(1.0 - tex2D(_OccTex, uv).r);
                float fromBrightness = smoothstep(_Threshold, _Threshold + max(_Softness, 1e-4), luminance);
                float transmit       = lerp(fromBrightness, fromMask, _UseOcc);

                float2 toCenter = (uv - _ProxUV.xy) * float2(_Aspect, 1.0);
                float  falloff  = exp(-dot(toCenter, toCenter) / max(_SunRadius * _SunRadius, 1e-4));

                float3 rayColor = lerp(float3(1.0, 1.0, 1.0), sceneColor, _SceneColor);
                return float4(rayColor * falloff * transmit, 1.0);
            }
            ENDCG
        }

        Pass
        {
            Blend One One

            CGPROGRAM
            #pragma vertex   vert_img
            #pragma fragment frag
            #pragma target   3.0
            #pragma multi_compile RAYS_LOW RAYS_MED RAYS_HIGH

            #if defined(RAYS_HIGH)
                #define SAMPLES 64
            #elif defined(RAYS_MED)
                #define SAMPLES 40
            #else
                #define SAMPLES 24
            #endif

            float4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                float2 toSun   = _SunUV.xy - uv;
                float  distance = length(toSun);
                float2 direction = toSun / max(distance, 1e-5);

                float2 toEdge   = lerp(uv, 1.0 - uv, step(0.0, direction)) / max(abs(direction), 1e-5);
                float  maxMarch = min(toEdge.x, toEdge.y);

                float  march  = min(distance * _Density, maxMarch);
                float2 stride = direction * (march / SAMPLES);

                float2 coord = uv;
                float  weight = 1.0;
                float3 accumulated = 0;
                [unroll(SAMPLES)]
                for (int s = 0; s < SAMPLES; ++s) {
                    coord += stride;
                    accumulated += tex2Dlod(_MainTex, float4(coord, 0, 0)).rgb * weight;
                    weight *= _Decay;
                }

                float weightSum = (1.0 - pow(_Decay, SAMPLES)) / max(1.0 - _Decay, 1e-5);
                float3 rays = accumulated * (_Exposure / max(weightSum, 1e-5)) * _RayStrength;

                // Alpha carries how much light reached this pixel, before tinting, so a deep
                // red lamp still clears the Darkness. Blend One One sums it across lights.
                float illumination = dot(rays, float3(0.2126, 0.7152, 0.0722)) * _Illum;
                return float4(rays * _RayTint.rgb, illumination);
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert_img
            #pragma fragment frag
            #pragma target   3.0

            float4 frag(v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                float3 sceneColor = tex2D(_MainTex, uv).rgb;

                float occluded = tex2D(_OccTex, uv).r * _UseOcc;
                float lit      = saturate(1.0 - occluded * _OccShadow);

                float4 rays = tex2D(_RayTex, uv);

                // Overlapping darkness does not stack: the strongest light wins the
                // amount, and tints mix in proportion to how much each contributes.
                float  demand = 0.0;
                float3 tintSum = 0.0;
                float  tintWeight = 0.0;
                [unroll(8)]
                for (int k = 0; k < 8; ++k)
                {
                    float4 dark = _DarkData[k];
                    float2 offset = (uv - dark.xy) * float2(_Aspect, 1.0);
                    float  range = max(dark.w, 1e-4);
                    float  amount = dark.z * (1.0 - smoothstep(range * 0.6, range, length(offset)));
                    demand = max(demand, amount);
                    tintSum += _DarkTints[k].rgb * amount;
                    tintWeight += amount;
                }
                float3 darkTint = tintWeight > 1e-5 ? tintSum / tintWeight : float3(1.0, 1.0, 1.0);

                float3 ambient = lerp(float3(1.0, 1.0, 1.0), darkTint, demand);
                sceneColor *= lerp(ambient, float3(1.0, 1.0, 1.0), saturate(rays.a));

                sceneColor += rays.rgb * _RayTint.rgb * (_RayStrength * lit);
                return float4(sceneColor, 1.0);
            }
            ENDCG
        }
    }

    Fallback Off
}
