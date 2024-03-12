Shader "Fur/Built In/Fur Strands"
{
    Properties
    {
        [Header(Main Colors)]_MainColorTextureB("Main Color Texture", 2D) = "white" {}
        _MainColorB("Main Color", Color) = (0.7735849,0.4603023,0.259078,0)
        _SpecColor("Specular Color", Color) = (0.8584906,0.4960563,0.360404,0)
        [Header(Marschner Highlight 1)]_SpecIntensity("Specular Inensity", Range( 0 , 1)) = 1
        _Smoothness("Smoothness", Range( 0 , 1)) = 0.5
        [Header(Marschner Highlight 2)]_SecondarySpecIntensity("Secondary Specular Intensity", Range( 0 , 4)) = 1
        _SecondarySmoothness("Secondary Smoothness", Range( 0 , 1)) = 0.5
        [Header(Back Lighting)]_SubsurfaceColor("Back Lighting Color", Color) = (0.972549,0.8509804,0.6666667,1)
        _Scale("Intensity", Range( 0 , 1)) = 1
        [Header(Ambient)] _AmbientIntensity("Ambient Light Intensity", Range( 0 , 1)) = 1
        [Header(Max Painter Color Override)] _maxColorOverride("Max Override", Range( 0 , 1)) = 1

    }

    SubShader 
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        Pass
        {
            Name "FORWARD"
            Tags
            {
                "LightMode" = "ForwardBase"
            }
            Cull Off

            CGPROGRAM
            #pragma vertex vertStrands
            #pragma target 3.0
            #pragma fragment fragStrands
            #pragma multi_compile_fwdbase_fullshadows nolightmap nodynlightmap novertexlight
            #pragma multi_compile_fog
            #include "FurInclude.cginc"
            ENDCG

        }

        Pass
        {
            Name "FORWARD"
            Tags
            {
                "LightMode" = "ForwardAdd"
            }
            Cull Off
            ZWrite Off Blend One One

            CGPROGRAM
            #pragma target 3.0
            #pragma vertex vertStrands
            #pragma fragment fragStrands
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #include "FurInclude.cginc"
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
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing // allow instanced shadow pass for most of the shaders
            #include "UnityCG.cginc"

            struct v2f
            {
                V2F_SHADOW_CASTER; 
                UNITY_VERTEX_OUTPUT_STEREO
            };
 

            v2f vert(appdata_base v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o) 
                return o;
            }
 
            float4 frag(v2f i) : SV_Target
            { 
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
}