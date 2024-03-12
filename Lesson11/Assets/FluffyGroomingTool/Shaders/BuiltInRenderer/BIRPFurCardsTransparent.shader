Shader "Fur/Built In/Fur Cards Transparent"
{
    Properties
    {
        [Header(Main Colors)]_MainColorTextureB("Main Color Texture", 2D) = "white" {}
        _MainColorB("Main Color", Color) = (0.7735849,0.4603023,0.259078,0)
        _SpecColor("Specular Color", Color) = (0.8584906,0.4960563,0.360404,0)
        _NormalMap("Normal Map", 2D) = "bump" {}
        _NormalStrength ("Normal Map Strength", Range(0.0,2.0)) = 1.0
        [Header(Alpha)]_Alpha ("Alpha Texture", 2D) = "white" {}
        _AlphaClip ("Alpha Clip Shadows", Range(0.0,1.0)) = 1.0
        [Header(Marschner Highlight 1)]_SpecIntensity("Specular Inensity", Range( 0 , 1)) = 1
        _Smoothness("Smoothness", Range( 0 , 1)) = 0.5
        [Header(Marschner Highlight 2)]_SecondarySpecIntensity("Secondary Specular Intensity", Range( 0 , 4)) = 1
        _SecondarySmoothness("Secondary Smoothness", Range( 0 , 1)) = 0.5
        [Header(Back Lighting)]_SubsurfaceColor("Back Lighting Color", Color) = (0.972549,0.8509804,0.6666667,1)
        _Scale("Intensity", Range( 0 , 1)) = 1
        [Header(Ambient)]_AmbientIntensity("Ambient Light Intensity", Range( 0 , 1)) = 1
        [Header(Max Painter Color Override)] _maxColorOverride("Max Override", Range( 0 , 1)) = 1
    }
    SubShader 
    {
        Tags
        {
            "Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="Transparent"
        }


        Pass
        {

            Name "FORWARD"
            Tags
            {
                "LightMode" = "ForwardBase"
                "Queue"="AlphaTest"
            }
            Cull Off
            ZWrite Off

            Blend SrcAlpha OneMinusSrcAlpha
            LOD 100

            CGPROGRAM
            #include "FurInclude.cginc"
            #pragma vertex vertCards
            #pragma fragment fragCardsAlpha
            #pragma multi_compile_fwdbase nolightmap nodynlightmap novertexlight
            #pragma multi_compile_fog
            ENDCG

        }

        Pass
        {
            Name "FORWARD"
            Tags
            {
                "LightMode" = "ForwardAdd"
                "Queue" = "Transparent"
            }
            Cull Off
            ZWrite Off
            ZWrite Off Blend One One

            CGPROGRAM
            #include "FurInclude.cginc"
            #pragma vertex vertCards
            #pragma fragment fragCardsAlpha
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            ENDCG
        }
        Pass
        {
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
            Cull Off
            CGPROGRAM
            #include "FurInclude.cginc"
            #pragma vertex vertCards
            #pragma fragment shadowFrag

            fixed4 shadowFrag(vertexOutput input) : SV_Target
            {
                clip(tex2D(_Alpha, input.uv).r - _AlphaClip);
                return 0;
            }
            ENDCG
        }
    }
}