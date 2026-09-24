//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Outline_/OutLineHighLightSelfIllumVirtualLightDir" {
Properties {
 _MainTex ("贴图(RGB)", 2D) = "white" {}
 _Color ("整体颜色", Color) = (1,1,1,1)
 _DefaultColor ("与整体颜色设置相同", Color) = (1,1,1,1)
 _HighLightColor ("高光颜色", Color) = (0.6,0.6,0.6,1)
 _PowValue ("高光强度", Float) = 0
 _Illum ("自发光贴图(R)", 2D) = "white" {}
 _LightColor ("光照颜色", Color) = (1,1,1,1)
 _LightDir ("光照方向", Vector) = (0,0,0,0)
 _RimColor ("边缘光颜色", Color) = (1,1,1,1)
 _RimPower ("边缘光强度", Range(0.1,10)) = 2
}
SubShader { 
 Tags { "QUEUE"="Transparent-10" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "QUEUE"="Transparent-10" }
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out lowp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in lowp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1 = tmpvar_3;
  highp mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * normalize(_glesNormal));
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD3 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
uniform sampler2D unity_Lightmap;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 + (_RimColor.xyz * pow (tmpvar_8, _RimPower)));
  tmpvar_3 = tmpvar_9;
  c_1.xyz = ((cse_5.xyz * _Color.xyz) * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD3).xyz));
  c_1.w = tmpvar_4.w;
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out highp vec2 xlv_TEXCOORD3;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1 = tmpvar_3;
  highp mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * normalize(_glesNormal));
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD3 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
uniform sampler2D unity_Lightmap;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 + (_RimColor.xyz * pow (tmpvar_8, _RimPower)));
  tmpvar_3 = tmpvar_9;
  c_1.xyz = ((cse_5.xyz * _Color.xyz) * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD3).xyz));
  c_1.w = tmpvar_4.w;
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1 = tmpvar_3;
  highp mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * normalize(_glesNormal));
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_6.xyz);
  xlv_TEXCOORD3 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD4 = (unity_World2Shadow[0] * cse_6);
}



#endif
#ifdef FRAGMENT

uniform highp vec4 _LightShadowData;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
uniform sampler2D unity_Lightmap;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 + (_RimColor.xyz * pow (tmpvar_8, _RimPower)));
  tmpvar_3 = tmpvar_9;
  lowp float tmpvar_10;
  mediump float lightShadowDataX_11;
  highp float dist_12;
  lowp float tmpvar_13;
  tmpvar_13 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD4).x;
  dist_12 = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = _LightShadowData.x;
  lightShadowDataX_11 = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = max (float((dist_12 > 
    (xlv_TEXCOORD4.z / xlv_TEXCOORD4.w)
  )), lightShadowDataX_11);
  tmpvar_10 = tmpvar_15;
  c_1.xyz = ((cse_5.xyz * _Color.xyz) * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD3).xyz), vec3((tmpvar_10 * 2.0))));
  c_1.w = tmpvar_4.w;
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosX0 - cse_28.x);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosY0 - cse_28.y);
  highp vec4 tmpvar_31;
  tmpvar_31 = (unity_4LightPosZ0 - cse_28.z);
  highp vec4 tmpvar_32;
  tmpvar_32 = (((tmpvar_29 * tmpvar_29) + (tmpvar_30 * tmpvar_30)) + (tmpvar_31 * tmpvar_31));
  highp vec4 tmpvar_33;
  tmpvar_33 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_29 * tmpvar_11.x) + (tmpvar_30 * tmpvar_11.y)) + (tmpvar_31 * tmpvar_11.z))
   * 
    inversesqrt(tmpvar_32)
  )) * (1.0/((1.0 + 
    (tmpvar_32 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_34;
  tmpvar_34 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_33.x) + (unity_LightColor[1].xyz * tmpvar_33.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_33.z)
  ) + (unity_LightColor[3].xyz * tmpvar_33.w)));
  tmpvar_6 = tmpvar_34;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out lowp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosX0 - cse_28.x);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosY0 - cse_28.y);
  highp vec4 tmpvar_31;
  tmpvar_31 = (unity_4LightPosZ0 - cse_28.z);
  highp vec4 tmpvar_32;
  tmpvar_32 = (((tmpvar_29 * tmpvar_29) + (tmpvar_30 * tmpvar_30)) + (tmpvar_31 * tmpvar_31));
  highp vec4 tmpvar_33;
  tmpvar_33 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_29 * tmpvar_11.x) + (tmpvar_30 * tmpvar_11.y)) + (tmpvar_31 * tmpvar_11.z))
   * 
    inversesqrt(tmpvar_32)
  )) * (1.0/((1.0 + 
    (tmpvar_32 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_34;
  tmpvar_34 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_33.x) + (unity_LightColor[1].xyz * tmpvar_33.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_33.z)
  ) + (unity_LightColor[3].xyz * tmpvar_33.w)));
  tmpvar_6 = tmpvar_34;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in lowp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosX0 - cse_28.x);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosY0 - cse_28.y);
  highp vec4 tmpvar_31;
  tmpvar_31 = (unity_4LightPosZ0 - cse_28.z);
  highp vec4 tmpvar_32;
  tmpvar_32 = (((tmpvar_29 * tmpvar_29) + (tmpvar_30 * tmpvar_30)) + (tmpvar_31 * tmpvar_31));
  highp vec4 tmpvar_33;
  tmpvar_33 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_29 * tmpvar_11.x) + (tmpvar_30 * tmpvar_11.y)) + (tmpvar_31 * tmpvar_11.z))
   * 
    inversesqrt(tmpvar_32)
  )) * (1.0/((1.0 + 
    (tmpvar_32 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_34;
  tmpvar_34 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_33.x) + (unity_LightColor[1].xyz * tmpvar_33.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_33.z)
  ) + (unity_LightColor[3].xyz * tmpvar_33.w)));
  tmpvar_6 = tmpvar_34;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out lowp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in lowp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1 = tmpvar_3;
  highp mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * normalize(_glesNormal));
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_6.xyz);
  xlv_TEXCOORD3 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD4 = (unity_World2Shadow[0] * cse_6);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
uniform sampler2D unity_Lightmap;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 + (_RimColor.xyz * pow (tmpvar_8, _RimPower)));
  tmpvar_3 = tmpvar_9;
  lowp float shadow_10;
  lowp float tmpvar_11;
  tmpvar_11 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD4.xyz);
  highp float tmpvar_12;
  tmpvar_12 = (_LightShadowData.x + (tmpvar_11 * (1.0 - _LightShadowData.x)));
  shadow_10 = tmpvar_12;
  c_1.xyz = ((cse_5.xyz * _Color.xyz) * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD3).xyz), vec3((shadow_10 * 2.0))));
  c_1.w = tmpvar_4.w;
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out highp vec2 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec2 tmpvar_1;
  lowp vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1 = tmpvar_3;
  highp mat3 tmpvar_4;
  tmpvar_4[0] = _Object2World[0].xyz;
  tmpvar_4[1] = _Object2World[1].xyz;
  tmpvar_4[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4 * normalize(_glesNormal));
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_6.xyz);
  xlv_TEXCOORD3 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD4 = (unity_World2Shadow[0] * cse_6);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
uniform sampler2D unity_Lightmap;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in highp vec2 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_6 + (_RimColor.xyz * pow (tmpvar_8, _RimPower)));
  tmpvar_3 = tmpvar_9;
  lowp float shadow_10;
  mediump float tmpvar_11;
  tmpvar_11 = texture (_ShadowMapTexture, xlv_TEXCOORD4.xyz);
  lowp float tmpvar_12;
  tmpvar_12 = tmpvar_11;
  highp float tmpvar_13;
  tmpvar_13 = (_LightShadowData.x + (tmpvar_12 * (1.0 - _LightShadowData.x)));
  shadow_10 = tmpvar_13;
  c_1.xyz = ((cse_5.xyz * _Color.xyz) * min ((2.0 * texture (unity_Lightmap, xlv_TEXCOORD3).xyz), vec3((shadow_10 * 2.0))));
  c_1.w = tmpvar_4.w;
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES


#ifdef VERTEX

#extension GL_EXT_shadow_samplers : enable
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosX0 - cse_28.x);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosY0 - cse_28.y);
  highp vec4 tmpvar_31;
  tmpvar_31 = (unity_4LightPosZ0 - cse_28.z);
  highp vec4 tmpvar_32;
  tmpvar_32 = (((tmpvar_29 * tmpvar_29) + (tmpvar_30 * tmpvar_30)) + (tmpvar_31 * tmpvar_31));
  highp vec4 tmpvar_33;
  tmpvar_33 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_29 * tmpvar_11.x) + (tmpvar_30 * tmpvar_11.y)) + (tmpvar_31 * tmpvar_11.z))
   * 
    inversesqrt(tmpvar_32)
  )) * (1.0/((1.0 + 
    (tmpvar_32 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_34;
  tmpvar_34 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_33.x) + (unity_LightColor[1].xyz * tmpvar_33.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_33.z)
  ) + (unity_LightColor[3].xyz * tmpvar_33.w)));
  tmpvar_6 = tmpvar_34;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying highp vec3 xlv_TEXCOORD2;
varying lowp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture2D (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform highp vec4 unity_4LightAtten0;
uniform highp vec4 unity_LightColor[8];
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out highp vec3 xlv_TEXCOORD2;
out lowp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec2 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec2 tmpvar_7;
  tmpvar_7 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_3 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_4 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_11;
  mediump vec3 tmpvar_13;
  mediump vec4 normal_14;
  normal_14 = tmpvar_12;
  highp float vC_15;
  mediump vec3 x3_16;
  mediump vec3 x2_17;
  mediump vec3 x1_18;
  highp float tmpvar_19;
  tmpvar_19 = dot (unity_SHAr, normal_14);
  x1_18.x = tmpvar_19;
  highp float tmpvar_20;
  tmpvar_20 = dot (unity_SHAg, normal_14);
  x1_18.y = tmpvar_20;
  highp float tmpvar_21;
  tmpvar_21 = dot (unity_SHAb, normal_14);
  x1_18.z = tmpvar_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_14.xyzz * normal_14.yzzx);
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHBr, tmpvar_22);
  x2_17.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHBg, tmpvar_22);
  x2_17.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHBb, tmpvar_22);
  x2_17.z = tmpvar_25;
  mediump float tmpvar_26;
  tmpvar_26 = ((normal_14.x * normal_14.x) - (normal_14.y * normal_14.y));
  vC_15 = tmpvar_26;
  highp vec3 tmpvar_27;
  tmpvar_27 = (unity_SHC.xyz * vC_15);
  x3_16 = tmpvar_27;
  tmpvar_13 = ((x1_18 + x2_17) + x3_16);
  shlight_2 = tmpvar_13;
  tmpvar_6 = shlight_2;
  highp vec4 cse_28;
  cse_28 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosX0 - cse_28.x);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosY0 - cse_28.y);
  highp vec4 tmpvar_31;
  tmpvar_31 = (unity_4LightPosZ0 - cse_28.z);
  highp vec4 tmpvar_32;
  tmpvar_32 = (((tmpvar_29 * tmpvar_29) + (tmpvar_30 * tmpvar_30)) + (tmpvar_31 * tmpvar_31));
  highp vec4 tmpvar_33;
  tmpvar_33 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_29 * tmpvar_11.x) + (tmpvar_30 * tmpvar_11.y)) + (tmpvar_31 * tmpvar_11.z))
   * 
    inversesqrt(tmpvar_32)
  )) * (1.0/((1.0 + 
    (tmpvar_32 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_34;
  tmpvar_34 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_33.x) + (unity_LightColor[1].xyz * tmpvar_33.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_33.z)
  ) + (unity_LightColor[3].xyz * tmpvar_33.w)));
  tmpvar_6 = tmpvar_34;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
  xlv_TEXCOORD1 = tmpvar_4;
  xlv_TEXCOORD2 = (_WorldSpaceCameraPos - cse_28.xyz);
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_28);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp vec4 _HighLightColor;
uniform mediump float _PowValue;
uniform lowp vec4 _LightColor;
uniform mediump vec4 _LightDir;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform sampler2D _Illum;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in highp vec3 xlv_TEXCOORD2;
in lowp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  tmpvar_2 = xlv_TEXCOORD1;
  lowp vec3 tmpvar_3;
  lowp vec4 tmpvar_4;
  lowp vec4 cse_5;
  cse_5 = texture (_MainTex, xlv_TEXCOORD0);
  tmpvar_4 = (cse_5 * _Color);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (cse_5.xyz * _Color.xyz);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_4.xyz * texture (_Illum, xlv_TEXCOORD0).x);
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_4.w;
  lowp vec3 tmpvar_9;
  tmpvar_9 = normalize(xlv_TEXCOORD3);
  mediump float tmpvar_10;
  tmpvar_10 = max (dot (tmpvar_9, normalize(_LightDir.xyz)), 0.001);
  highp float tmpvar_11;
  tmpvar_11 = clamp (dot (normalize(xlv_TEXCOORD2), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_12;
  tmpvar_12 = (1.0 - tmpvar_11);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_7 + (_RimColor.xyz * pow (tmpvar_12, _RimPower)));
  tmpvar_3 = tmpvar_13;
  mediump vec4 c_14;
  c_14.xyz = (((tmpvar_6 * 
    (_HighLightColor.xyz + _LightColor.xyz)
  ) * (
    (tmpvar_10 * pow (tmpvar_10, _PowValue))
   * 2.0)) + (tmpvar_6 * min (
    (_PowValue / 10.0)
  , 0.3)));
  c_14.w = tmpvar_8;
  c_1 = c_14;
  c_1.xyz = (c_1.xyz + (tmpvar_6 * xlv_TEXCOORD4));
  c_1.xyz = (c_1.xyz + tmpvar_3);
  _glesFragData[0] = c_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" }
"!!GLES3"
}
}
 }
}
Fallback "Diffuse"
}