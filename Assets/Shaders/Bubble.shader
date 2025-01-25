Shader "Custom/BubbleShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 1, 1, 0.5) // Color translúcido
        _Transparency ("Transparency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Back
        ZWrite Off

        Pass
        {
            Material {
                Diffuse [_MainColor]
                Ambient [_MainColor]
            }
            Lighting On
            SetTexture [_MainTex] {
                Combine Primary * Texture
            }
        }
    }
    FallBack "Transparent/Diffuse"
}
