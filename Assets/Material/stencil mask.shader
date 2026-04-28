Shader "Unlit/stencil mask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Stencil ("Stencil ID", Float) = 1
    }

    SubShader
    {
        Tags { "Queue"="Transparent" }

        Pass
        {
            ColorMask 0
            ZWrite Off

            Stencil
            {
                Ref [_Stencil]
                Comp Always
                Pass Replace
            }
        }
    }
}
