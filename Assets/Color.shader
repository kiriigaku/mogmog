Shader "Custom/Color" {
Properties {
_Color("Main Color", Color) = (1, 1, 1, 1)
_Float("Float", Float) = 20.0
_Vector("Vector", Vector) = (0.5, 0.7, 0.2, 0.4)
_Range("Range", Range (0.0, 1.0)) = 0.5
}
SubShader {
Pass{
Tags { "RenderType"="Opaque"}
LOD 200
// Blend SrcAlpha OneMinusSrcAlpha // Alpha blending
// Blend One One // Additive
// Blend OneMinusDstColor One // Soft Additive
// Blend DstColor Zero // Multiplicative
// Blend DstColor SrcColor // 2x Multiplicative
Cull Off
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 3.0
#include "UnityCG.cginc"
#include "Random.cginc"
#include "PhotoshopMath.cginc"
#include "Easing.cginc"
#include "CoonsCurve.cginc"
#include "Noise.cginc"
#include "Transform.cginc"
uniform float4 _Color;
uniform float _Float;
uniform float4 _Vector;
uniform float _Range;
//uniform _LightColor0;
struct v2f {
float4 position : SV_POSITION;
float3 normal : NORMAL;
//float4 texcoord : TEXCOORD0;
float4 col : COLOR0;
};
v2f vert(appdata_base v){
v2f o;
o.position = mul(UNITY_MATRIX_MVP, v.vertex);
o.normal = v.normal;
//o.texcoord = v.texcoord;
return o;
}
float4 frag(v2f i) : COLOR{
return _Color;
}
ENDCG
}
}
}