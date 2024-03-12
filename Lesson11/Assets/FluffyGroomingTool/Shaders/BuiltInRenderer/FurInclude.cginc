// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "NormalHelperInclude.cginc"
#include "MarshnerLightingInclude.cginc"

static float DISTORTION = 5;
static float POWER = 0.75;
static float MAX_SCALE = 6;
static float4 white = float4(1, 1, 1, 1);

struct vertexInput
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float2 uv: TEXCOORD0;
    float2 uv2: TEXCOORD1;
    float4 color:COLOR;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct vertexOutput
{
    float4 pos : SV_POSITION;
    float3 worldPos : TEXCOORD0;
    UNITY_SHADOW_COORDS(1)
    float3 viewDir : TEXCOORD2;
    float3 normalDir : TEXCOORD3;
    float3 tspace0 : TEXCOORD4;
    float3 tspace1 : TEXCOORD5;
    float3 tspace2 : TEXCOORD6;
    float2 uv : TEXCOORD7;
    float2 uv2 : TEXCOORD8;
    float4 overrideColor : TEXCOORD9;
    float4 diff : TEXCOORD10;
    float3 ambient : TEXCOORD11;
    float3 marschnerLighting : TEXCOORD12;
    UNITY_FOG_COORDS(13)
    UNITY_VERTEX_OUTPUT_STEREO
};

uniform float _AmbientIntensity;
uniform float _Smoothness;
uniform float _SecondarySpecIntensity;
uniform float _SecondarySmoothness;
uniform float _ReflectionArea;
float4 _SpecularColor;
uniform float _SpecIntensity;

uniform float4 _SubsurfaceColor;

uniform sampler2D FurMotionTexture;
uniform float _Distortion;
uniform float _Power;
uniform float _Scale;

uniform float4 _MainColorB;
uniform float _AlphaX;
uniform float _AlphaClip;

uniform float _HighlightSize;
uniform float _SecondaryHighlightIntensity;
uniform float _HighlightIntensity;
uniform sampler2D _NormalMap;
uniform float4 _NormalMap_ST;
uniform sampler2D _MainColorTextureB;

uniform sampler2D _Detail;
uniform float4 _Detail_ST;
uniform sampler2D _Alpha;
uniform float4 _MainColorTextureB_ST;
uniform float _NormalStrength;
uniform float _Reflectiveness;
uniform float _maxColorOverride = 1;
uniform float4x4 objectRotationMatrix;

vertexOutput vertStrands(vertexInput v)
{
    vertexOutput o;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_OUTPUT(vertexOutput, o);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos.xyz);
    o.normalDir = mul(unity_ObjectToWorld, normalize(v.normal)).xyz;
    o.pos = mul(UNITY_MATRIX_VP, float4(o.worldPos, 1));
    UNITY_TRANSFER_FOG(o, o.pos);
    float3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
    float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
    float3 wBitangent = cross(o.normalDir, wTangent) * tangentSign;
    o.tspace0 = float3(wTangent.x, wBitangent.x, o.normalDir.x);
    o.tspace1 = float3(wTangent.y, wBitangent.y, o.normalDir.y);
    o.tspace2 = float3(wTangent.z, wBitangent.z, o.normalDir.z);
    o.uv = v.uv2;
    o.uv2 = v.uv;
    o.overrideColor = float4(v.color.xyz, lerp(0, 1 - v.color.a, _maxColorOverride));

    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
    #if defined (POINT) || defined (SPOT)
    float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - o.worldPos.xyz;
    lightDirection = normalize(vertexToLightSource);
    #endif

    float nl = max(0, dot(o.normalDir, lightDirection));
    o.diff = nl * _LightColor0;
    o.ambient = lerp(white, ShadeSH9(float4(o.normalDir, 1)), _AmbientIntensity);

    float4 lightColor = saturate(_LightColor0);

    float3 marshnerLighting = _SpecColor * MAX_REFLECTION * _SpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            o.normalDir,
            o.viewDir,
            -lightDirection,
            1,
            _ReflectionArea,
            _Smoothness
        );
    float3 marshnerLighting2 = _SpecColor * MAX_REFLECTION * _SecondarySpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            o.normalDir,
            o.viewDir,
            -lightDirection,
            1,
            _ReflectionArea,
            _SecondarySmoothness
        );
    o.marschnerLighting = marshnerLighting + marshnerLighting2;
    #ifndef SHADER_API_VULKAN
    TRANSFER_SHADOW(o)
    #endif
    return o;
}

vertexOutput vertCards(vertexInput v)
{
    vertexOutput o;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_OUTPUT(vertexOutput, o);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
    o.viewDir = normalize(_WorldSpaceCameraPos - o.worldPos.xyz);
    o.normalDir = mul(unity_ObjectToWorld, normalize(v.normal)).xyz;
    o.pos = mul(UNITY_MATRIX_VP, float4(o.worldPos, 1));
    UNITY_TRANSFER_FOG(o, o.pos);
    float3 wTangent = UnityObjectToWorldDir(v.tangent.xyz);
    float tangentSign = v.tangent.w * unity_WorldTransformParams.w;
    float3 wBitangent = cross(o.normalDir, wTangent) * tangentSign;
    o.tspace0 = float3(wTangent.x, wBitangent.x, o.normalDir.x);
    o.tspace1 = float3(wTangent.y, wBitangent.y, o.normalDir.y);
    o.tspace2 = float3(wTangent.z, wBitangent.z, o.normalDir.z);
    o.uv = v.uv2;
    o.uv2 = v.uv;
    o.overrideColor = float4(v.color.xyz, lerp(0, 1 - v.color.a, _maxColorOverride));
    o.marschnerLighting = 1;
    float nl = max(0, dot(o.normalDir, _WorldSpaceLightPos0.xyz));
    o.diff = nl * _LightColor0;
    o.ambient = lerp(white, ShadeSH9(float4(o.normalDir, 1)), _AmbientIntensity);

    #ifndef SHADER_API_VULKAN
    TRANSFER_SHADOW(o)
    #endif
    return o;
}


float3 getWorldNormal(vertexOutput input)
{
    float4 normal = lerp(float4(0.5, 0.5, 1, 1), tex2D(_NormalMap, input.uv * _NormalMap_ST.xy + _NormalMap_ST.zw),
                         _NormalStrength);

    float3 tnormal = UnpackNormal(normal);
    float3 worldNormal;
    worldNormal.x = dot(input.tspace0, tnormal);
    worldNormal.y = dot(input.tspace1, tnormal);
    worldNormal.z = dot(input.tspace2, tnormal);
    return worldNormal;
}

uniform float _AmbientLightContribution;

float3 getBaseColor(vertexOutput input, float4 overrideColor)
{
    float3 diffuse = tex2D(_MainColorTextureB, input.uv2 * _MainColorTextureB_ST.xy + _MainColorTextureB_ST.zw).rgb *
        _MainColorB.rgb;
    return lerp(diffuse, overrideColor.rgb, overrideColor.a);
}


float4 fragCards(vertexOutput input) : COLOR
{
    clip(tex2D(_Alpha, input.uv).r - _AlphaClip);
    float3 worldNormal = getWorldNormal(input);
    float3 color = getBaseColor(input, input.overrideColor);

    UNITY_LIGHT_ATTENUATION(atten, input, input.worldPos)
    float attenuation = atten;

    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

    #if defined (POINT) || defined (SPOT)
    float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.worldPos.xyz;
    lightDirection = normalize(vertexToLightSource);
    #endif

    float4 lightColor = saturate(_LightColor0);

    float4 finalColor = float4(1, 1, 1, 1);
    float3 marshnerLighting = _SpecColor * MAX_REFLECTION * _SpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            worldNormal,
            input.viewDir,
            -lightDirection,
            attenuation,
            _ReflectionArea,
            _Smoothness
        );
    float3 marshnerLighting2 = _SpecColor * MAX_REFLECTION * _SecondarySpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            worldNormal,
            input.viewDir,
            -lightDirection,
            attenuation,
            _ReflectionArea,
            _SecondarySmoothness
        );
    float3 lighting = input.diff * atten + input.ambient;
    finalColor.rgb = color * lighting + marshnerLighting + marshnerLighting2;
    finalColor.rgb += BackLighting(lightDirection, input.viewDir, worldNormal, attenuation, DISTORTION, POWER,
                                   _Scale * MAX_SCALE, _SubsurfaceColor);
    UNITY_APPLY_FOG(input.fogCoord, finalColor);
    return finalColor;
}

float4 fragCardsAlpha(vertexOutput input) : COLOR
{
    float3 worldNormal = getWorldNormal(input);
    float3 color = getBaseColor(input, input.overrideColor);

    UNITY_LIGHT_ATTENUATION(atten, input, input.worldPos)
    float attenuation = atten;

    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);

    #if defined (POINT) || defined (SPOT)
    float3 vertexToLightSource = _WorldSpaceLightPos0.xyz - input.worldPos.xyz;
    lightDirection = normalize(vertexToLightSource);
    #endif

    float4 lightColor = saturate(_LightColor0);

    float4 finalColor = float4(1, 1, 1, 1);
    float3 marshnerLighting = _SpecColor * MAX_REFLECTION * _SpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            worldNormal,
            input.viewDir,
            -lightDirection,
            attenuation,
            _ReflectionArea,
            _Smoothness
        );
    float3 marshnerLighting2 = _SpecColor * MAX_REFLECTION * _SecondarySpecIntensity * lightColor.w *
        HairBxDF(
            float3(0, 0, 0),
            worldNormal,
            input.viewDir,
            -lightDirection,
            attenuation,
            _ReflectionArea,
            _SecondarySmoothness
        );
    float3 lighting = input.diff * atten + input.ambient;
    finalColor.rgb = color * lighting + marshnerLighting + marshnerLighting2;
    finalColor.rgb += BackLighting(lightDirection, input.viewDir, worldNormal, attenuation, DISTORTION, POWER,
                                   _Scale * MAX_SCALE, _SubsurfaceColor);
    finalColor.a = tex2D(_Alpha, input.uv).r;
    UNITY_APPLY_FOG(input.fogCoord, finalColor);
    return finalColor;
}

float4 fragStrands(vertexOutput input) : COLOR
{
    float3 color = getBaseColor(input, input.overrideColor);

    UNITY_LIGHT_ATTENUATION(atten, input, input.worldPos)
    float attenuation = atten;
    float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
    float4 finalColor = float4(1, 1, 1, 1);
    float3 lighting = input.diff * atten + input.ambient;
    finalColor.rgb = color * lighting + input.marschnerLighting * atten;
    finalColor.rgb += BackLighting(lightDirection, input.viewDir, input.normalDir, attenuation, DISTORTION, POWER,
                                   _Scale * MAX_SCALE, _SubsurfaceColor);
    UNITY_APPLY_FOG(input.fogCoord, finalColor);
    return finalColor;
}

float4 fragWindContribution(vertexOutput input) : COLOR
{
    return float4(input.overrideColor.rgb, 1);
}
