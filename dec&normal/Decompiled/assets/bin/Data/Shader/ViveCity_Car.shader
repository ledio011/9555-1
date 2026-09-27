//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "ViveCity/Car" {
Properties {
 _MainTex ("贴图(RGB)变色通道(A)", 2D) = "white" {}
 _Color ("变色", Color) = (1,1,1,1)
 _Cubemap ("反射贴图", CUBE) = "" {}
 _ReflAmount ("反射强度", Range(0.01,1)) = 0.5
 _RimColor ("边缘光颜色", Color) = (1,1,1,1)
 _RimPower ("边缘光强度", Range(0.1,10)) = 2
}
SubShader { 
 Tags { "RenderType"="Opaque" }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" }
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp vec4 c_13;
  c_13.xyz = ((tmpvar_7 * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz)
  ) * 2.0));
  c_13.w = 0.0;
  c_1.w = c_13.w;
  c_1.xyz = (c_13.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp vec4 c_13;
  c_13.xyz = ((tmpvar_7 * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz)
  ) * 2.0));
  c_13.w = 0.0;
  c_1.w = c_13.w;
  c_1.xyz = (c_13.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (_RimColor.xyz * pow (tmpvar_8, _RimPower));
  tmpvar_4 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_4 + ((tmpvar_10.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_11;
  c_1.xyz = ((tmpvar_5.xyz * tmpvar_6.xyz) * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz));
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec2 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec2 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (_RimColor.xyz * pow (tmpvar_8, _RimPower));
  tmpvar_4 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_4 + ((tmpvar_10.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_11;
  c_1.xyz = ((tmpvar_5.xyz * tmpvar_6.xyz) * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD4).xyz));
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  mediump vec3 lm_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz);
  lm_13 = tmpvar_14;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_7 * lm_13);
  c_1.xyz = tmpvar_15;
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec2 xlv_TEXCOORD4;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - (_Object2World * _glesVertex).xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec2 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  mediump vec3 lm_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD4).xyz);
  lm_13 = tmpvar_14;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_7 * lm_13);
  c_1.xyz = tmpvar_15;
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float tmpvar_13;
  mediump float lightShadowDataX_14;
  highp float dist_15;
  lowp float tmpvar_16;
  tmpvar_16 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6).x;
  dist_15 = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = _LightShadowData.x;
  lightShadowDataX_14 = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = max (float((dist_15 > 
    (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w)
  )), lightShadowDataX_14);
  tmpvar_13 = tmpvar_18;
  lowp vec4 c_19;
  c_19.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * tmpvar_13) * 2.0));
  c_19.w = 0.0;
  c_1.w = c_19.w;
  c_1.xyz = (c_19.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT

uniform highp vec4 _LightShadowData;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (_RimColor.xyz * pow (tmpvar_8, _RimPower));
  tmpvar_4 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_4 + ((tmpvar_10.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_11;
  lowp float tmpvar_12;
  mediump float lightShadowDataX_13;
  highp float dist_14;
  lowp float tmpvar_15;
  tmpvar_15 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD5).x;
  dist_14 = tmpvar_15;
  highp float tmpvar_16;
  tmpvar_16 = _LightShadowData.x;
  lightShadowDataX_13 = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = max (float((dist_14 > 
    (xlv_TEXCOORD5.z / xlv_TEXCOORD5.w)
  )), lightShadowDataX_13);
  tmpvar_12 = tmpvar_17;
  c_1.xyz = ((tmpvar_5.xyz * tmpvar_6.xyz) * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz), vec3((tmpvar_12 * 2.0))));
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT

uniform highp vec4 _LightShadowData;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float tmpvar_13;
  mediump float lightShadowDataX_14;
  highp float dist_15;
  lowp float tmpvar_16;
  tmpvar_16 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD5).x;
  dist_15 = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = _LightShadowData.x;
  lightShadowDataX_14 = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = max (float((dist_15 > 
    (xlv_TEXCOORD5.z / xlv_TEXCOORD5.w)
  )), lightShadowDataX_14);
  tmpvar_13 = tmpvar_18;
  mediump vec3 lm_19;
  lowp vec3 tmpvar_20;
  tmpvar_20 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz);
  lm_19 = tmpvar_20;
  lowp vec3 tmpvar_21;
  tmpvar_21 = vec3((tmpvar_13 * 2.0));
  mediump vec3 tmpvar_22;
  tmpvar_22 = (tmpvar_7 * min (lm_19, tmpvar_21));
  c_1.xyz = tmpvar_22;
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_32;
  tmpvar_32 = (unity_4LightPosX0 - cse_31.x);
  highp vec4 tmpvar_33;
  tmpvar_33 = (unity_4LightPosY0 - cse_31.y);
  highp vec4 tmpvar_34;
  tmpvar_34 = (unity_4LightPosZ0 - cse_31.z);
  highp vec4 tmpvar_35;
  tmpvar_35 = (((tmpvar_32 * tmpvar_32) + (tmpvar_33 * tmpvar_33)) + (tmpvar_34 * tmpvar_34));
  highp vec4 tmpvar_36;
  tmpvar_36 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_32 * tmpvar_14.x) + (tmpvar_33 * tmpvar_14.y)) + (tmpvar_34 * tmpvar_14.z))
   * 
    inversesqrt(tmpvar_35)
  )) * (1.0/((1.0 + 
    (tmpvar_35 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_37;
  tmpvar_37 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_36.x) + (unity_LightColor[1].xyz * tmpvar_36.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_36.z)
  ) + (unity_LightColor[3].xyz * tmpvar_36.w)));
  tmpvar_6 = tmpvar_37;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp vec4 c_13;
  c_13.xyz = ((tmpvar_7 * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz)
  ) * 2.0));
  c_13.w = 0.0;
  c_1.w = c_13.w;
  c_1.xyz = (c_13.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_32;
  tmpvar_32 = (unity_4LightPosX0 - cse_31.x);
  highp vec4 tmpvar_33;
  tmpvar_33 = (unity_4LightPosY0 - cse_31.y);
  highp vec4 tmpvar_34;
  tmpvar_34 = (unity_4LightPosZ0 - cse_31.z);
  highp vec4 tmpvar_35;
  tmpvar_35 = (((tmpvar_32 * tmpvar_32) + (tmpvar_33 * tmpvar_33)) + (tmpvar_34 * tmpvar_34));
  highp vec4 tmpvar_36;
  tmpvar_36 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_32 * tmpvar_14.x) + (tmpvar_33 * tmpvar_14.y)) + (tmpvar_34 * tmpvar_14.z))
   * 
    inversesqrt(tmpvar_35)
  )) * (1.0/((1.0 + 
    (tmpvar_35 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_37;
  tmpvar_37 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_36.x) + (unity_LightColor[1].xyz * tmpvar_36.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_36.z)
  ) + (unity_LightColor[3].xyz * tmpvar_36.w)));
  tmpvar_6 = tmpvar_37;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp vec4 c_13;
  c_13.xyz = ((tmpvar_7 * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz)
  ) * 2.0));
  c_13.w = 0.0;
  c_1.w = c_13.w;
  c_1.xyz = (c_13.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_32;
  tmpvar_32 = (unity_4LightPosX0 - cse_31.x);
  highp vec4 tmpvar_33;
  tmpvar_33 = (unity_4LightPosY0 - cse_31.y);
  highp vec4 tmpvar_34;
  tmpvar_34 = (unity_4LightPosZ0 - cse_31.z);
  highp vec4 tmpvar_35;
  tmpvar_35 = (((tmpvar_32 * tmpvar_32) + (tmpvar_33 * tmpvar_33)) + (tmpvar_34 * tmpvar_34));
  highp vec4 tmpvar_36;
  tmpvar_36 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_32 * tmpvar_14.x) + (tmpvar_33 * tmpvar_14.y)) + (tmpvar_34 * tmpvar_14.z))
   * 
    inversesqrt(tmpvar_35)
  )) * (1.0/((1.0 + 
    (tmpvar_35 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_37;
  tmpvar_37 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_36.x) + (unity_LightColor[1].xyz * tmpvar_36.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_36.z)
  ) + (unity_LightColor[3].xyz * tmpvar_36.w)));
  tmpvar_6 = tmpvar_37;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float tmpvar_13;
  mediump float lightShadowDataX_14;
  highp float dist_15;
  lowp float tmpvar_16;
  tmpvar_16 = texture2DProj (_ShadowMapTexture, xlv_TEXCOORD6).x;
  dist_15 = tmpvar_16;
  highp float tmpvar_17;
  tmpvar_17 = _LightShadowData.x;
  lightShadowDataX_14 = tmpvar_17;
  highp float tmpvar_18;
  tmpvar_18 = max (float((dist_15 > 
    (xlv_TEXCOORD6.z / xlv_TEXCOORD6.w)
  )), lightShadowDataX_14);
  tmpvar_13 = tmpvar_18;
  lowp vec4 c_19;
  c_19.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * tmpvar_13) * 2.0));
  c_19.w = 0.0;
  c_1.w = c_19.w;
  c_1.xyz = (c_19.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  lowp float tmpvar_14;
  tmpvar_14 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD6.xyz);
  highp float tmpvar_15;
  tmpvar_15 = (_LightShadowData.x + (tmpvar_14 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_15;
  lowp vec4 c_16;
  c_16.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * shadow_13) * 2.0));
  c_16.w = 0.0;
  c_1.w = c_16.w;
  c_1.xyz = (c_16.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  mediump float tmpvar_14;
  tmpvar_14 = texture (_ShadowMapTexture, xlv_TEXCOORD6.xyz);
  lowp float tmpvar_15;
  tmpvar_15 = tmpvar_14;
  highp float tmpvar_16;
  tmpvar_16 = (_LightShadowData.x + (tmpvar_15 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_16;
  lowp vec4 c_17;
  c_17.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * shadow_13) * 2.0));
  c_17.w = 0.0;
  c_1.w = c_17.w;
  c_1.xyz = (c_17.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (_RimColor.xyz * pow (tmpvar_8, _RimPower));
  tmpvar_4 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_4 + ((tmpvar_10.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_11;
  lowp float shadow_12;
  lowp float tmpvar_13;
  tmpvar_13 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD5.xyz);
  highp float tmpvar_14;
  tmpvar_14 = (_LightShadowData.x + (tmpvar_13 * (1.0 - _LightShadowData.x)));
  shadow_12 = tmpvar_14;
  c_1.xyz = ((tmpvar_5.xyz * tmpvar_6.xyz) * min ((2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz), vec3((shadow_12 * 2.0))));
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec2 xlv_TEXCOORD4;
out highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec2 xlv_TEXCOORD4;
in highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  highp float tmpvar_7;
  tmpvar_7 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_8;
  tmpvar_8 = (1.0 - tmpvar_7);
  highp vec3 tmpvar_9;
  tmpvar_9 = (_RimColor.xyz * pow (tmpvar_8, _RimPower));
  tmpvar_4 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_4 + ((tmpvar_10.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_11;
  lowp float shadow_12;
  mediump float tmpvar_13;
  tmpvar_13 = texture (_ShadowMapTexture, xlv_TEXCOORD5.xyz);
  lowp float tmpvar_14;
  tmpvar_14 = tmpvar_13;
  highp float tmpvar_15;
  tmpvar_15 = (_LightShadowData.x + (tmpvar_14 * (1.0 - _LightShadowData.x)));
  shadow_12 = tmpvar_15;
  c_1.xyz = ((tmpvar_5.xyz * tmpvar_6.xyz) * min ((2.0 * texture (unity_Lightmap, xlv_TEXCOORD4).xyz), vec3((shadow_12 * 2.0))));
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec2 xlv_TEXCOORD4;
varying highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  lowp float tmpvar_14;
  tmpvar_14 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD5.xyz);
  highp float tmpvar_15;
  tmpvar_15 = (_LightShadowData.x + (tmpvar_14 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_15;
  mediump vec3 lm_16;
  lowp vec3 tmpvar_17;
  tmpvar_17 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD4).xyz);
  lm_16 = tmpvar_17;
  lowp vec3 tmpvar_18;
  tmpvar_18 = vec3((shadow_13 * 2.0));
  mediump vec3 tmpvar_19;
  tmpvar_19 = (tmpvar_7 * min (lm_16, tmpvar_18));
  c_1.xyz = tmpvar_19;
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec2 xlv_TEXCOORD4;
out highp vec4 xlv_TEXCOORD5;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  mediump vec3 tmpvar_2;
  lowp vec3 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 1.0;
  tmpvar_4.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_glesVertex.xyz - ((_World2Object * tmpvar_4).xyz * unity_Scale.w));
  highp mat3 tmpvar_6;
  tmpvar_6[0] = _Object2World[0].xyz;
  tmpvar_6[1] = _Object2World[1].xyz;
  tmpvar_6[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * (tmpvar_5 - (2.0 * 
    (dot (tmpvar_1, tmpvar_5) * tmpvar_1)
  )));
  tmpvar_2 = tmpvar_7;
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * tmpvar_1);
  tmpvar_3 = tmpvar_9;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
  highp vec4 cse_10;
  cse_10 = (_Object2World * _glesVertex);
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_10.xyz);
  xlv_TEXCOORD4 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD5 = (unity_World2Shadow[0] * cse_10);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform highp vec4 _LightShadowData;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec2 xlv_TEXCOORD4;
in highp vec4 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  mediump float tmpvar_14;
  tmpvar_14 = texture (_ShadowMapTexture, xlv_TEXCOORD5.xyz);
  lowp float tmpvar_15;
  tmpvar_15 = tmpvar_14;
  highp float tmpvar_16;
  tmpvar_16 = (_LightShadowData.x + (tmpvar_15 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_16;
  mediump vec3 lm_17;
  lowp vec3 tmpvar_18;
  tmpvar_18 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD4).xyz);
  lm_17 = tmpvar_18;
  lowp vec3 tmpvar_19;
  tmpvar_19 = vec3((shadow_13 * 2.0));
  mediump vec3 tmpvar_20;
  tmpvar_20 = (tmpvar_7 * min (lm_17, tmpvar_19));
  c_1.xyz = tmpvar_20;
  c_1.w = 0.0;
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_32;
  tmpvar_32 = (unity_4LightPosX0 - cse_31.x);
  highp vec4 tmpvar_33;
  tmpvar_33 = (unity_4LightPosY0 - cse_31.y);
  highp vec4 tmpvar_34;
  tmpvar_34 = (unity_4LightPosZ0 - cse_31.z);
  highp vec4 tmpvar_35;
  tmpvar_35 = (((tmpvar_32 * tmpvar_32) + (tmpvar_33 * tmpvar_33)) + (tmpvar_34 * tmpvar_34));
  highp vec4 tmpvar_36;
  tmpvar_36 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_32 * tmpvar_14.x) + (tmpvar_33 * tmpvar_14.y)) + (tmpvar_34 * tmpvar_14.z))
   * 
    inversesqrt(tmpvar_35)
  )) * (1.0/((1.0 + 
    (tmpvar_35 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_37;
  tmpvar_37 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_36.x) + (unity_LightColor[1].xyz * tmpvar_36.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_36.z)
  ) + (unity_LightColor[3].xyz * tmpvar_36.w)));
  tmpvar_6 = tmpvar_37;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT

#extension GL_EXT_shadow_samplers : enable
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying lowp vec3 xlv_TEXCOORD4;
varying lowp vec3 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  lowp float tmpvar_14;
  tmpvar_14 = shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD6.xyz);
  highp float tmpvar_15;
  tmpvar_15 = (_LightShadowData.x + (tmpvar_14 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_15;
  lowp vec4 c_16;
  c_16.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * shadow_13) * 2.0));
  c_16.w = 0.0;
  c_1.w = c_16.w;
  c_1.xyz = (c_16.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out lowp vec3 xlv_TEXCOORD4;
out lowp vec3 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = normalize(_glesNormal);
  highp vec3 shlight_2;
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  lowp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_1, tmpvar_8) * tmpvar_1)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_1);
  tmpvar_4 = tmpvar_12;
  highp mat3 tmpvar_13;
  tmpvar_13[0] = _Object2World[0].xyz;
  tmpvar_13[1] = _Object2World[1].xyz;
  tmpvar_13[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * (tmpvar_1 * unity_Scale.w));
  tmpvar_5 = tmpvar_14;
  highp vec4 tmpvar_15;
  tmpvar_15.w = 1.0;
  tmpvar_15.xyz = tmpvar_14;
  mediump vec3 tmpvar_16;
  mediump vec4 normal_17;
  normal_17 = tmpvar_15;
  highp float vC_18;
  mediump vec3 x3_19;
  mediump vec3 x2_20;
  mediump vec3 x1_21;
  highp float tmpvar_22;
  tmpvar_22 = dot (unity_SHAr, normal_17);
  x1_21.x = tmpvar_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAg, normal_17);
  x1_21.y = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAb, normal_17);
  x1_21.z = tmpvar_24;
  mediump vec4 tmpvar_25;
  tmpvar_25 = (normal_17.xyzz * normal_17.yzzx);
  highp float tmpvar_26;
  tmpvar_26 = dot (unity_SHBr, tmpvar_25);
  x2_20.x = tmpvar_26;
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBg, tmpvar_25);
  x2_20.y = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBb, tmpvar_25);
  x2_20.z = tmpvar_28;
  mediump float tmpvar_29;
  tmpvar_29 = ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y));
  vC_18 = tmpvar_29;
  highp vec3 tmpvar_30;
  tmpvar_30 = (unity_SHC.xyz * vC_18);
  x3_19 = tmpvar_30;
  tmpvar_16 = ((x1_21 + x2_20) + x3_19);
  shlight_2 = tmpvar_16;
  tmpvar_6 = shlight_2;
  highp vec4 cse_31;
  cse_31 = (_Object2World * _glesVertex);
  highp vec4 tmpvar_32;
  tmpvar_32 = (unity_4LightPosX0 - cse_31.x);
  highp vec4 tmpvar_33;
  tmpvar_33 = (unity_4LightPosY0 - cse_31.y);
  highp vec4 tmpvar_34;
  tmpvar_34 = (unity_4LightPosZ0 - cse_31.z);
  highp vec4 tmpvar_35;
  tmpvar_35 = (((tmpvar_32 * tmpvar_32) + (tmpvar_33 * tmpvar_33)) + (tmpvar_34 * tmpvar_34));
  highp vec4 tmpvar_36;
  tmpvar_36 = (max (vec4(0.0, 0.0, 0.0, 0.0), (
    (((tmpvar_32 * tmpvar_14.x) + (tmpvar_33 * tmpvar_14.y)) + (tmpvar_34 * tmpvar_14.z))
   * 
    inversesqrt(tmpvar_35)
  )) * (1.0/((1.0 + 
    (tmpvar_35 * unity_4LightAtten0)
  ))));
  highp vec3 tmpvar_37;
  tmpvar_37 = (tmpvar_6 + ((
    ((unity_LightColor[0].xyz * tmpvar_36.x) + (unity_LightColor[1].xyz * tmpvar_36.y))
   + 
    (unity_LightColor[2].xyz * tmpvar_36.z)
  ) + (unity_LightColor[3].xyz * tmpvar_36.w)));
  tmpvar_6 = tmpvar_37;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (_WorldSpaceCameraPos - cse_31.xyz);
  xlv_TEXCOORD4 = tmpvar_5;
  xlv_TEXCOORD5 = tmpvar_6;
  xlv_TEXCOORD6 = (unity_World2Shadow[0] * cse_31);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in lowp vec3 xlv_TEXCOORD4;
in lowp vec3 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 c_1;
  highp vec3 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_3 = xlv_TEXCOORD1;
  tmpvar_2 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_4;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_6;
  tmpvar_6 = clamp (((_Color + 1.0) - tmpvar_5.w), 0.0, 1.0);
  lowp vec3 tmpvar_7;
  tmpvar_7 = (tmpvar_5.xyz * tmpvar_6.xyz);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_2), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_4 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_3);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_4 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_5.w));
  tmpvar_4 = tmpvar_12;
  lowp float shadow_13;
  mediump float tmpvar_14;
  tmpvar_14 = texture (_ShadowMapTexture, xlv_TEXCOORD6.xyz);
  lowp float tmpvar_15;
  tmpvar_15 = tmpvar_14;
  highp float tmpvar_16;
  tmpvar_16 = (_LightShadowData.x + (tmpvar_15 * (1.0 - _LightShadowData.x)));
  shadow_13 = tmpvar_16;
  lowp vec4 c_17;
  c_17.xyz = ((tmpvar_7 * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD4, _WorldSpaceLightPos0.xyz))
   * shadow_13) * 2.0));
  c_17.w = 0.0;
  c_1.w = c_17.w;
  c_1.xyz = (c_17.xyz + (tmpvar_7 * xlv_TEXCOORD5));
  c_1.xyz = (c_1.xyz + tmpvar_4);
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
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
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
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
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
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_ON" "DIRLIGHTMAP_ON" }
"!!GLES3"
}
}
 }
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardAdd" "RenderType"="Opaque" }
  ZWrite Off
  Fog {
   Color (0,0,0,0)
  }
  Blend One One
Program "vp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6).xyz;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (xlv_TEXCOORD3, xlv_TEXCOORD3);
  lowp vec4 c_16;
  c_16.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * texture2D (_LightTexture0, vec2(tmpvar_15)).w) * 2.0));
  c_16.w = 0.0;
  c_1.xyz = c_16.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "POINT" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6).xyz;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (xlv_TEXCOORD3, xlv_TEXCOORD3);
  lowp vec4 c_16;
  c_16.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * texture (_LightTexture0, vec2(tmpvar_15)).w) * 2.0));
  c_16.w = 0.0;
  c_1.xyz = c_16.xyz;
  c_1.w = 0.0;
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = _WorldSpaceLightPos0.xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  lightDir_2 = xlv_TEXCOORD2;
  lowp vec4 c_14;
  c_14.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD1, lightDir_2)
  ) * 2.0));
  c_14.w = 0.0;
  c_1.xyz = c_14.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = _WorldSpaceLightPos0.xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  lightDir_2 = xlv_TEXCOORD2;
  lowp vec4 c_14;
  c_14.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * (max (0.0, 
    dot (xlv_TEXCOORD1, lightDir_2)
  ) * 2.0));
  c_14.w = 0.0;
  c_1.xyz = c_14.xyz;
  c_1.w = 0.0;
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6);
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp vec2 P_15;
  P_15 = ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5);
  highp float tmpvar_16;
  tmpvar_16 = dot (xlv_TEXCOORD3.xyz, xlv_TEXCOORD3.xyz);
  lowp float atten_17;
  atten_17 = ((float(
    (xlv_TEXCOORD3.z > 0.0)
  ) * texture2D (_LightTexture0, P_15).w) * texture2D (_LightTextureB0, vec2(tmpvar_16)).w);
  lowp vec4 c_18;
  c_18.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * atten_17) * 2.0));
  c_18.w = 0.0;
  c_1.xyz = c_18.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "SPOT" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp vec2 P_15;
  P_15 = ((xlv_TEXCOORD3.xy / xlv_TEXCOORD3.w) + 0.5);
  highp float tmpvar_16;
  tmpvar_16 = dot (xlv_TEXCOORD3.xyz, xlv_TEXCOORD3.xyz);
  lowp float atten_17;
  atten_17 = ((float(
    (xlv_TEXCOORD3.z > 0.0)
  ) * texture (_LightTexture0, P_15).w) * texture (_LightTextureB0, vec2(tmpvar_16)).w);
  lowp vec4 c_18;
  c_18.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * atten_17) * 2.0));
  c_18.w = 0.0;
  c_1.xyz = c_18.xyz;
  c_1.w = 0.0;
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6).xyz;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _LightColor0;
uniform lowp samplerCube _LightTexture0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (xlv_TEXCOORD3, xlv_TEXCOORD3);
  lowp vec4 c_16;
  c_16.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * 
    (texture2D (_LightTextureB0, vec2(tmpvar_15)).w * textureCube (_LightTexture0, xlv_TEXCOORD3).w)
  ) * 2.0));
  c_16.w = 0.0;
  c_1.xyz = c_16.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "POINT_COOKIE" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 cse_6;
  cse_6 = (_Object2World * _glesVertex);
  tmpvar_5 = (_WorldSpaceLightPos0.xyz - cse_6.xyz);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * cse_6).xyz;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _LightColor0;
uniform lowp samplerCube _LightTexture0;
uniform sampler2D _LightTextureB0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize(xlv_TEXCOORD2);
  lightDir_2 = tmpvar_14;
  highp float tmpvar_15;
  tmpvar_15 = dot (xlv_TEXCOORD3, xlv_TEXCOORD3);
  lowp vec4 c_16;
  c_16.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * 
    (texture (_LightTextureB0, vec2(tmpvar_15)).w * texture (_LightTexture0, xlv_TEXCOORD3).w)
  ) * 2.0));
  c_16.w = 0.0;
  c_1.xyz = c_16.xyz;
  c_1.w = 0.0;
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = _WorldSpaceLightPos0.xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying highp vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  lightDir_2 = xlv_TEXCOORD2;
  lowp vec4 c_14;
  c_14.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * texture2D (_LightTexture0, xlv_TEXCOORD3).w) * 2.0));
  c_14.w = 0.0;
  c_1.xyz = c_14.xyz;
  c_1.w = 0.0;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform lowp vec4 _WorldSpaceLightPos0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
uniform highp mat4 _LightMatrix0;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp mat3 tmpvar_3;
  tmpvar_3[0] = _Object2World[0].xyz;
  tmpvar_3[1] = _Object2World[1].xyz;
  tmpvar_3[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = _WorldSpaceLightPos0.xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_1;
  xlv_TEXCOORD2 = tmpvar_2;
  xlv_TEXCOORD3 = (_LightMatrix0 * (_Object2World * _glesVertex)).xy;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _LightColor0;
uniform sampler2D _LightTexture0;
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in highp vec2 xlv_TEXCOORD0;
in lowp vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in highp vec2 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 c_1;
  lowp vec3 lightDir_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  highp float tmpvar_9;
  tmpvar_9 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_10;
  tmpvar_10 = (1.0 - tmpvar_9);
  highp vec3 tmpvar_11;
  tmpvar_11 = (_RimColor.xyz * pow (tmpvar_10, _RimPower));
  tmpvar_6 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_13;
  tmpvar_13 = (tmpvar_6 + ((tmpvar_12.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_13;
  lightDir_2 = xlv_TEXCOORD2;
  lowp vec4 c_14;
  c_14.xyz = (((tmpvar_7.xyz * tmpvar_8.xyz) * _LightColor0.xyz) * ((
    max (0.0, dot (xlv_TEXCOORD1, lightDir_2))
   * texture (_LightTexture0, xlv_TEXCOORD3).w) * 2.0));
  c_14.w = 0.0;
  c_1.xyz = c_14.xyz;
  c_1.w = 0.0;
  _glesFragData[0] = c_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "POINT" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "POINT" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "SPOT" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "SPOT" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "POINT_COOKIE" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "POINT_COOKIE" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL_COOKIE" }
"!!GLES3"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" "RenderType"="Opaque" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
varying lowp vec3 xlv_TEXCOORD0;
void main ()
{
  lowp vec3 tmpvar_1;
  highp mat3 tmpvar_2;
  tmpvar_2[0] = _Object2World[0].xyz;
  tmpvar_2[1] = _Object2World[1].xyz;
  tmpvar_2[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
varying lowp vec3 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 res_1;
  highp vec2 tmpvar_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, tmpvar_2);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_6 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_6 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_12;
  res_1.xyz = ((xlv_TEXCOORD0 * 0.5) + 0.5);
  res_1.w = 0.0;
  gl_FragData[0] = res_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
out lowp vec3 xlv_TEXCOORD0;
void main ()
{
  lowp vec3 tmpvar_1;
  highp mat3 tmpvar_2;
  tmpvar_2[0] = _Object2World[0].xyz;
  tmpvar_2[1] = _Object2World[1].xyz;
  tmpvar_2[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (normalize(_glesNormal) * unity_Scale.w));
  tmpvar_1 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
in lowp vec3 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 res_1;
  highp vec2 tmpvar_2;
  highp vec3 tmpvar_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, tmpvar_2);
  highp float tmpvar_8;
  tmpvar_8 = clamp (dot (normalize(tmpvar_3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_9;
  tmpvar_9 = (1.0 - tmpvar_8);
  highp vec3 tmpvar_10;
  tmpvar_10 = (_RimColor.xyz * pow (tmpvar_9, _RimPower));
  tmpvar_6 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_6 + ((tmpvar_11.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_12;
  res_1.xyz = ((xlv_TEXCOORD0 * 0.5) + 0.5);
  res_1.w = 0.0;
  _glesFragData[0] = res_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
"!!GLES"
}
SubProgram "gles3 " {
"!!GLES3"
}
}
 }
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" "RenderType"="Opaque" }
  ZWrite Off
Program "vp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.xyz = (tmpvar_4 * unity_Scale.w);
  mediump vec3 tmpvar_17;
  mediump vec4 normal_18;
  normal_18 = tmpvar_16;
  highp float vC_19;
  mediump vec3 x3_20;
  mediump vec3 x2_21;
  mediump vec3 x1_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAr, normal_18);
  x1_22.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAg, normal_18);
  x1_22.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHAb, normal_18);
  x1_22.z = tmpvar_25;
  mediump vec4 tmpvar_26;
  tmpvar_26 = (normal_18.xyzz * normal_18.yzzx);
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBr, tmpvar_26);
  x2_21.x = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBg, tmpvar_26);
  x2_21.y = tmpvar_28;
  highp float tmpvar_29;
  tmpvar_29 = dot (unity_SHBb, tmpvar_26);
  x2_21.z = tmpvar_29;
  mediump float tmpvar_30;
  tmpvar_30 = ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y));
  vC_19 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (unity_SHC.xyz * vC_19);
  x3_20 = tmpvar_31;
  tmpvar_17 = ((x1_22 + x2_21) + x3_20);
  tmpvar_5 = tmpvar_17;
  highp vec3 tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_32 = tmpvar_1.xyz;
  tmpvar_33 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_34;
  tmpvar_34[0].x = tmpvar_32.x;
  tmpvar_34[0].y = tmpvar_33.x;
  tmpvar_34[0].z = tmpvar_2.x;
  tmpvar_34[1].x = tmpvar_32.y;
  tmpvar_34[1].y = tmpvar_33.y;
  tmpvar_34[1].z = tmpvar_2.y;
  tmpvar_34[2].x = tmpvar_32.z;
  tmpvar_34[2].y = tmpvar_33.z;
  tmpvar_34[2].z = tmpvar_2.z;
  highp vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_34 * ((
    (_World2Object * tmpvar_35)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
  light_3.w = tmpvar_16.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz + xlv_TEXCOORD5);
  light_3.xyz = tmpvar_17;
  lowp vec4 c_18;
  mediump vec3 tmpvar_19;
  tmpvar_19 = (tmpvar_9 * light_3.xyz);
  c_18.xyz = tmpvar_19;
  c_18.w = 0.0;
  c_2 = c_18;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.xyz = (tmpvar_4 * unity_Scale.w);
  mediump vec3 tmpvar_17;
  mediump vec4 normal_18;
  normal_18 = tmpvar_16;
  highp float vC_19;
  mediump vec3 x3_20;
  mediump vec3 x2_21;
  mediump vec3 x1_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAr, normal_18);
  x1_22.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAg, normal_18);
  x1_22.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHAb, normal_18);
  x1_22.z = tmpvar_25;
  mediump vec4 tmpvar_26;
  tmpvar_26 = (normal_18.xyzz * normal_18.yzzx);
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBr, tmpvar_26);
  x2_21.x = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBg, tmpvar_26);
  x2_21.y = tmpvar_28;
  highp float tmpvar_29;
  tmpvar_29 = dot (unity_SHBb, tmpvar_26);
  x2_21.z = tmpvar_29;
  mediump float tmpvar_30;
  tmpvar_30 = ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y));
  vC_19 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (unity_SHC.xyz * vC_19);
  x3_20 = tmpvar_31;
  tmpvar_17 = ((x1_22 + x2_21) + x3_20);
  tmpvar_5 = tmpvar_17;
  highp vec3 tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_32 = tmpvar_1.xyz;
  tmpvar_33 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_34;
  tmpvar_34[0].x = tmpvar_32.x;
  tmpvar_34[0].y = tmpvar_33.x;
  tmpvar_34[0].z = tmpvar_2.x;
  tmpvar_34[1].x = tmpvar_32.y;
  tmpvar_34[1].y = tmpvar_33.y;
  tmpvar_34[1].z = tmpvar_2.y;
  tmpvar_34[2].x = tmpvar_32.z;
  tmpvar_34[2].y = tmpvar_33.z;
  tmpvar_34[2].z = tmpvar_2.z;
  highp vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_34 * ((
    (_World2Object * tmpvar_35)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
  light_3.w = tmpvar_16.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz + xlv_TEXCOORD5);
  light_3.xyz = tmpvar_17;
  lowp vec4 c_18;
  mediump vec3 tmpvar_19;
  tmpvar_19 = (tmpvar_9 * light_3.xyz);
  c_18.xyz = tmpvar_19;
  c_18.w = 0.0;
  c_2 = c_18;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  tmpvar_5.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_5.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec3 tmpvar_16;
  highp vec3 tmpvar_17;
  tmpvar_16 = tmpvar_1.xyz;
  tmpvar_17 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_18;
  tmpvar_18[0].x = tmpvar_16.x;
  tmpvar_18[0].y = tmpvar_17.x;
  tmpvar_18[0].z = tmpvar_2.x;
  tmpvar_18[1].x = tmpvar_16.y;
  tmpvar_18[1].y = tmpvar_17.y;
  tmpvar_18[1].z = tmpvar_2.y;
  tmpvar_18[2].x = tmpvar_16.z;
  tmpvar_18[2].y = tmpvar_17.z;
  tmpvar_18[2].z = tmpvar_2.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_18 * ((
    (_World2Object * tmpvar_19)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD1;
  tmpvar_7 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_11;
  tmpvar_11 = clamp (((_Color + 1.0) - tmpvar_10.w), 0.0, 1.0);
  lowp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_10.xyz * tmpvar_11.xyz);
  highp float tmpvar_13;
  tmpvar_13 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_7), 0.0, 1.0);
  mediump float tmpvar_14;
  tmpvar_14 = (1.0 - tmpvar_13);
  highp vec3 tmpvar_15;
  tmpvar_15 = (_RimColor.xyz * pow (tmpvar_14, _RimPower));
  tmpvar_9 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = textureCube (_Cubemap, tmpvar_8);
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_9 + ((tmpvar_16.xyz * _ReflAmount) * tmpvar_10.w));
  tmpvar_9 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_6 = tmpvar_18;
  mediump vec4 tmpvar_19;
  tmpvar_19 = -(log2(max (light_6, vec4(0.001, 0.001, 0.001, 0.001))));
  light_6.w = tmpvar_19.w;
  highp float tmpvar_20;
  tmpvar_20 = ((sqrt(
    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
  ) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_20;
  lowp vec3 tmpvar_21;
  tmpvar_21 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lmFull_4 = tmpvar_21;
  lowp vec3 tmpvar_22;
  tmpvar_22 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD5).xyz);
  lmIndirect_3 = tmpvar_22;
  light_6.xyz = (tmpvar_19.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  lowp vec4 c_23;
  mediump vec3 tmpvar_24;
  tmpvar_24 = (tmpvar_12 * light_6.xyz);
  c_23.xyz = tmpvar_24;
  c_23.w = 0.0;
  c_2 = c_23;
  c_2.xyz = (c_2.xyz + tmpvar_9);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  tmpvar_5.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_5.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec3 tmpvar_16;
  highp vec3 tmpvar_17;
  tmpvar_16 = tmpvar_1.xyz;
  tmpvar_17 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_18;
  tmpvar_18[0].x = tmpvar_16.x;
  tmpvar_18[0].y = tmpvar_17.x;
  tmpvar_18[0].z = tmpvar_2.x;
  tmpvar_18[1].x = tmpvar_16.y;
  tmpvar_18[1].y = tmpvar_17.y;
  tmpvar_18[1].z = tmpvar_2.y;
  tmpvar_18[2].x = tmpvar_16.z;
  tmpvar_18[2].y = tmpvar_17.z;
  tmpvar_18[2].z = tmpvar_2.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_18 * ((
    (_World2Object * tmpvar_19)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD1;
  tmpvar_7 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_11;
  tmpvar_11 = clamp (((_Color + 1.0) - tmpvar_10.w), 0.0, 1.0);
  lowp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_10.xyz * tmpvar_11.xyz);
  highp float tmpvar_13;
  tmpvar_13 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_7), 0.0, 1.0);
  mediump float tmpvar_14;
  tmpvar_14 = (1.0 - tmpvar_13);
  highp vec3 tmpvar_15;
  tmpvar_15 = (_RimColor.xyz * pow (tmpvar_14, _RimPower));
  tmpvar_9 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture (_Cubemap, tmpvar_8);
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_9 + ((tmpvar_16.xyz * _ReflAmount) * tmpvar_10.w));
  tmpvar_9 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_6 = tmpvar_18;
  mediump vec4 tmpvar_19;
  tmpvar_19 = -(log2(max (light_6, vec4(0.001, 0.001, 0.001, 0.001))));
  light_6.w = tmpvar_19.w;
  highp float tmpvar_20;
  tmpvar_20 = ((sqrt(
    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
  ) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_20;
  lowp vec3 tmpvar_21;
  tmpvar_21 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lmFull_4 = tmpvar_21;
  lowp vec3 tmpvar_22;
  tmpvar_22 = (2.0 * texture (unity_LightmapInd, xlv_TEXCOORD5).xyz);
  lmIndirect_3 = tmpvar_22;
  light_6.xyz = (tmpvar_19.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  lowp vec4 c_23;
  mediump vec3 tmpvar_24;
  tmpvar_24 = (tmpvar_12 * light_6.xyz);
  c_23.xyz = tmpvar_24;
  c_23.w = 0.0;
  c_2 = c_23;
  c_2.xyz = (c_2.xyz + tmpvar_9);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_7;
  tmpvar_7 = (_glesVertex.xyz - ((_World2Object * tmpvar_6).xyz * unity_Scale.w));
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_7 - (2.0 * 
    (dot (tmpvar_2, tmpvar_7) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * tmpvar_2);
  tmpvar_4 = tmpvar_11;
  highp vec4 o_12;
  highp vec4 tmpvar_13;
  tmpvar_13 = (tmpvar_5 * 0.5);
  highp vec2 tmpvar_14;
  tmpvar_14.x = tmpvar_13.x;
  tmpvar_14.y = (tmpvar_13.y * _ProjectionParams.x);
  o_12.xy = (tmpvar_14 + tmpvar_13.w);
  o_12.zw = tmpvar_5.zw;
  highp vec3 tmpvar_15;
  highp vec3 tmpvar_16;
  tmpvar_15 = tmpvar_1.xyz;
  tmpvar_16 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_17;
  tmpvar_17[0].x = tmpvar_15.x;
  tmpvar_17[0].y = tmpvar_16.x;
  tmpvar_17[0].z = tmpvar_2.x;
  tmpvar_17[1].x = tmpvar_15.y;
  tmpvar_17[1].y = tmpvar_16.y;
  tmpvar_17[1].z = tmpvar_2.y;
  tmpvar_17[2].x = tmpvar_15.z;
  tmpvar_17[2].y = tmpvar_16.z;
  tmpvar_17[2].z = tmpvar_2.z;
  highp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_17 * ((
    (_World2Object * tmpvar_18)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_12;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec3 lm_16;
  lowp vec3 tmpvar_17;
  tmpvar_17 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lm_16 = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 0.0;
  tmpvar_18.xyz = lm_16;
  mediump vec4 tmpvar_19;
  tmpvar_19 = (-(log2(
    max (light_3, vec4(0.001, 0.001, 0.001, 0.001))
  )) + tmpvar_18);
  light_3 = tmpvar_19;
  lowp vec4 c_20;
  mediump vec3 tmpvar_21;
  tmpvar_21 = (tmpvar_9 * tmpvar_19.xyz);
  c_20.xyz = tmpvar_21;
  c_20.w = 0.0;
  c_2 = c_20;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_7;
  tmpvar_7 = (_glesVertex.xyz - ((_World2Object * tmpvar_6).xyz * unity_Scale.w));
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_7 - (2.0 * 
    (dot (tmpvar_2, tmpvar_7) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * tmpvar_2);
  tmpvar_4 = tmpvar_11;
  highp vec4 o_12;
  highp vec4 tmpvar_13;
  tmpvar_13 = (tmpvar_5 * 0.5);
  highp vec2 tmpvar_14;
  tmpvar_14.x = tmpvar_13.x;
  tmpvar_14.y = (tmpvar_13.y * _ProjectionParams.x);
  o_12.xy = (tmpvar_14 + tmpvar_13.w);
  o_12.zw = tmpvar_5.zw;
  highp vec3 tmpvar_15;
  highp vec3 tmpvar_16;
  tmpvar_15 = tmpvar_1.xyz;
  tmpvar_16 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_17;
  tmpvar_17[0].x = tmpvar_15.x;
  tmpvar_17[0].y = tmpvar_16.x;
  tmpvar_17[0].z = tmpvar_2.x;
  tmpvar_17[1].x = tmpvar_15.y;
  tmpvar_17[1].y = tmpvar_16.y;
  tmpvar_17[1].z = tmpvar_2.y;
  tmpvar_17[2].x = tmpvar_15.z;
  tmpvar_17[2].y = tmpvar_16.z;
  tmpvar_17[2].z = tmpvar_2.z;
  highp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_17 * ((
    (_World2Object * tmpvar_18)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_12;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec3 lm_16;
  lowp vec3 tmpvar_17;
  tmpvar_17 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lm_16 = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 0.0;
  tmpvar_18.xyz = lm_16;
  mediump vec4 tmpvar_19;
  tmpvar_19 = (-(log2(
    max (light_3, vec4(0.001, 0.001, 0.001, 0.001))
  )) + tmpvar_18);
  light_3 = tmpvar_19;
  lowp vec4 c_20;
  mediump vec3 tmpvar_21;
  tmpvar_21 = (tmpvar_9 * tmpvar_19.xyz);
  c_20.xyz = tmpvar_21;
  c_20.w = 0.0;
  c_2 = c_20;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.xyz = (tmpvar_4 * unity_Scale.w);
  mediump vec3 tmpvar_17;
  mediump vec4 normal_18;
  normal_18 = tmpvar_16;
  highp float vC_19;
  mediump vec3 x3_20;
  mediump vec3 x2_21;
  mediump vec3 x1_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAr, normal_18);
  x1_22.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAg, normal_18);
  x1_22.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHAb, normal_18);
  x1_22.z = tmpvar_25;
  mediump vec4 tmpvar_26;
  tmpvar_26 = (normal_18.xyzz * normal_18.yzzx);
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBr, tmpvar_26);
  x2_21.x = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBg, tmpvar_26);
  x2_21.y = tmpvar_28;
  highp float tmpvar_29;
  tmpvar_29 = dot (unity_SHBb, tmpvar_26);
  x2_21.z = tmpvar_29;
  mediump float tmpvar_30;
  tmpvar_30 = ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y));
  vC_19 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (unity_SHC.xyz * vC_19);
  x3_20 = tmpvar_31;
  tmpvar_17 = ((x1_22 + x2_21) + x3_20);
  tmpvar_5 = tmpvar_17;
  highp vec3 tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_32 = tmpvar_1.xyz;
  tmpvar_33 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_34;
  tmpvar_34[0].x = tmpvar_32.x;
  tmpvar_34[0].y = tmpvar_33.x;
  tmpvar_34[0].z = tmpvar_2.x;
  tmpvar_34[1].x = tmpvar_32.y;
  tmpvar_34[1].y = tmpvar_33.y;
  tmpvar_34[1].z = tmpvar_2.y;
  tmpvar_34[2].x = tmpvar_32.z;
  tmpvar_34[2].y = tmpvar_33.z;
  tmpvar_34[2].z = tmpvar_2.z;
  highp vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_34 * ((
    (_World2Object * tmpvar_35)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
  light_3.w = tmpvar_16.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz + xlv_TEXCOORD5);
  light_3.xyz = tmpvar_17;
  lowp vec4 c_18;
  mediump vec3 tmpvar_19;
  tmpvar_19 = (tmpvar_9 * light_3.xyz);
  c_18.xyz = tmpvar_19;
  c_18.w = 0.0;
  c_2 = c_18;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_SHAr;
uniform highp vec4 unity_SHAg;
uniform highp vec4 unity_SHAb;
uniform highp vec4 unity_SHBr;
uniform highp vec4 unity_SHBg;
uniform highp vec4 unity_SHBb;
uniform highp vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec3 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  highp vec4 tmpvar_16;
  tmpvar_16.w = 1.0;
  tmpvar_16.xyz = (tmpvar_4 * unity_Scale.w);
  mediump vec3 tmpvar_17;
  mediump vec4 normal_18;
  normal_18 = tmpvar_16;
  highp float vC_19;
  mediump vec3 x3_20;
  mediump vec3 x2_21;
  mediump vec3 x1_22;
  highp float tmpvar_23;
  tmpvar_23 = dot (unity_SHAr, normal_18);
  x1_22.x = tmpvar_23;
  highp float tmpvar_24;
  tmpvar_24 = dot (unity_SHAg, normal_18);
  x1_22.y = tmpvar_24;
  highp float tmpvar_25;
  tmpvar_25 = dot (unity_SHAb, normal_18);
  x1_22.z = tmpvar_25;
  mediump vec4 tmpvar_26;
  tmpvar_26 = (normal_18.xyzz * normal_18.yzzx);
  highp float tmpvar_27;
  tmpvar_27 = dot (unity_SHBr, tmpvar_26);
  x2_21.x = tmpvar_27;
  highp float tmpvar_28;
  tmpvar_28 = dot (unity_SHBg, tmpvar_26);
  x2_21.y = tmpvar_28;
  highp float tmpvar_29;
  tmpvar_29 = dot (unity_SHBb, tmpvar_26);
  x2_21.z = tmpvar_29;
  mediump float tmpvar_30;
  tmpvar_30 = ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y));
  vC_19 = tmpvar_30;
  highp vec3 tmpvar_31;
  tmpvar_31 = (unity_SHC.xyz * vC_19);
  x3_20 = tmpvar_31;
  tmpvar_17 = ((x1_22 + x2_21) + x3_20);
  tmpvar_5 = tmpvar_17;
  highp vec3 tmpvar_32;
  highp vec3 tmpvar_33;
  tmpvar_32 = tmpvar_1.xyz;
  tmpvar_33 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_34;
  tmpvar_34[0].x = tmpvar_32.x;
  tmpvar_34[0].y = tmpvar_33.x;
  tmpvar_34[0].z = tmpvar_2.x;
  tmpvar_34[1].x = tmpvar_32.y;
  tmpvar_34[1].y = tmpvar_33.y;
  tmpvar_34[1].z = tmpvar_2.y;
  tmpvar_34[2].x = tmpvar_32.z;
  tmpvar_34[2].y = tmpvar_33.z;
  tmpvar_34[2].z = tmpvar_2.z;
  highp vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_34 * ((
    (_World2Object * tmpvar_35)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec3 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec4 tmpvar_16;
  tmpvar_16 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
  light_3.w = tmpvar_16.w;
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_16.xyz + xlv_TEXCOORD5);
  light_3.xyz = tmpvar_17;
  lowp vec4 c_18;
  mediump vec3 tmpvar_19;
  tmpvar_19 = (tmpvar_9 * light_3.xyz);
  c_18.xyz = tmpvar_19;
  c_18.w = 0.0;
  c_2 = c_18;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  tmpvar_5.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_5.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec3 tmpvar_16;
  highp vec3 tmpvar_17;
  tmpvar_16 = tmpvar_1.xyz;
  tmpvar_17 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_18;
  tmpvar_18[0].x = tmpvar_16.x;
  tmpvar_18[0].y = tmpvar_17.x;
  tmpvar_18[0].z = tmpvar_2.x;
  tmpvar_18[1].x = tmpvar_16.y;
  tmpvar_18[1].y = tmpvar_17.y;
  tmpvar_18[1].z = tmpvar_2.y;
  tmpvar_18[2].x = tmpvar_16.z;
  tmpvar_18[2].y = tmpvar_17.z;
  tmpvar_18[2].z = tmpvar_2.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_18 * ((
    (_World2Object * tmpvar_19)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
varying highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD1;
  tmpvar_7 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_11;
  tmpvar_11 = clamp (((_Color + 1.0) - tmpvar_10.w), 0.0, 1.0);
  lowp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_10.xyz * tmpvar_11.xyz);
  highp float tmpvar_13;
  tmpvar_13 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_7), 0.0, 1.0);
  mediump float tmpvar_14;
  tmpvar_14 = (1.0 - tmpvar_13);
  highp vec3 tmpvar_15;
  tmpvar_15 = (_RimColor.xyz * pow (tmpvar_14, _RimPower));
  tmpvar_9 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = textureCube (_Cubemap, tmpvar_8);
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_9 + ((tmpvar_16.xyz * _ReflAmount) * tmpvar_10.w));
  tmpvar_9 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_6 = tmpvar_18;
  mediump vec4 tmpvar_19;
  tmpvar_19 = max (light_6, vec4(0.001, 0.001, 0.001, 0.001));
  light_6.w = tmpvar_19.w;
  highp float tmpvar_20;
  tmpvar_20 = ((sqrt(
    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
  ) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_20;
  lowp vec3 tmpvar_21;
  tmpvar_21 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lmFull_4 = tmpvar_21;
  lowp vec3 tmpvar_22;
  tmpvar_22 = (2.0 * texture2D (unity_LightmapInd, xlv_TEXCOORD5).xyz);
  lmIndirect_3 = tmpvar_22;
  light_6.xyz = (tmpvar_19.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  lowp vec4 c_23;
  mediump vec3 tmpvar_24;
  tmpvar_24 = (tmpvar_12 * light_6.xyz);
  c_23.xyz = tmpvar_24;
  c_23.w = 0.0;
  c_2 = c_23;
  c_2.xyz = (c_2.xyz + tmpvar_9);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp vec4 unity_ShadowFadeCenterAndType;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
out highp vec4 xlv_TEXCOORD6;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7.w = 1.0;
  tmpvar_7.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_8;
  tmpvar_8 = (_glesVertex.xyz - ((_World2Object * tmpvar_7).xyz * unity_Scale.w));
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * (tmpvar_8 - (2.0 * 
    (dot (tmpvar_2, tmpvar_8) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * tmpvar_2);
  tmpvar_4 = tmpvar_12;
  highp vec4 o_13;
  highp vec4 tmpvar_14;
  tmpvar_14 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_15;
  tmpvar_15.x = tmpvar_14.x;
  tmpvar_15.y = (tmpvar_14.y * _ProjectionParams.x);
  o_13.xy = (tmpvar_15 + tmpvar_14.w);
  o_13.zw = tmpvar_6.zw;
  tmpvar_5.xyz = (((_Object2World * _glesVertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w);
  tmpvar_5.w = (-((glstate_matrix_modelview0 * _glesVertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w));
  highp vec3 tmpvar_16;
  highp vec3 tmpvar_17;
  tmpvar_16 = tmpvar_1.xyz;
  tmpvar_17 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_18;
  tmpvar_18[0].x = tmpvar_16.x;
  tmpvar_18[0].y = tmpvar_17.x;
  tmpvar_18[0].z = tmpvar_2.x;
  tmpvar_18[1].x = tmpvar_16.y;
  tmpvar_18[1].y = tmpvar_17.y;
  tmpvar_18[1].z = tmpvar_2.y;
  tmpvar_18[2].x = tmpvar_16.z;
  tmpvar_18[2].y = tmpvar_17.z;
  tmpvar_18[2].z = tmpvar_2.z;
  highp vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_18 * ((
    (_World2Object * tmpvar_19)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_13;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD6 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
uniform sampler2D unity_LightmapInd;
uniform highp vec4 unity_LightmapFade;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
in highp vec4 xlv_TEXCOORD6;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec3 lmIndirect_3;
  mediump vec3 lmFull_4;
  mediump float lmFade_5;
  mediump vec4 light_6;
  highp vec3 tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = xlv_TEXCOORD1;
  tmpvar_7 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_11;
  tmpvar_11 = clamp (((_Color + 1.0) - tmpvar_10.w), 0.0, 1.0);
  lowp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_10.xyz * tmpvar_11.xyz);
  highp float tmpvar_13;
  tmpvar_13 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_7), 0.0, 1.0);
  mediump float tmpvar_14;
  tmpvar_14 = (1.0 - tmpvar_13);
  highp vec3 tmpvar_15;
  tmpvar_15 = (_RimColor.xyz * pow (tmpvar_14, _RimPower));
  tmpvar_9 = tmpvar_15;
  lowp vec4 tmpvar_16;
  tmpvar_16 = texture (_Cubemap, tmpvar_8);
  highp vec3 tmpvar_17;
  tmpvar_17 = (tmpvar_9 + ((tmpvar_16.xyz * _ReflAmount) * tmpvar_10.w));
  tmpvar_9 = tmpvar_17;
  lowp vec4 tmpvar_18;
  tmpvar_18 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_6 = tmpvar_18;
  mediump vec4 tmpvar_19;
  tmpvar_19 = max (light_6, vec4(0.001, 0.001, 0.001, 0.001));
  light_6.w = tmpvar_19.w;
  highp float tmpvar_20;
  tmpvar_20 = ((sqrt(
    dot (xlv_TEXCOORD6, xlv_TEXCOORD6)
  ) * unity_LightmapFade.z) + unity_LightmapFade.w);
  lmFade_5 = tmpvar_20;
  lowp vec3 tmpvar_21;
  tmpvar_21 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lmFull_4 = tmpvar_21;
  lowp vec3 tmpvar_22;
  tmpvar_22 = (2.0 * texture (unity_LightmapInd, xlv_TEXCOORD5).xyz);
  lmIndirect_3 = tmpvar_22;
  light_6.xyz = (tmpvar_19.xyz + mix (lmIndirect_3, lmFull_4, vec3(clamp (lmFade_5, 0.0, 1.0))));
  lowp vec4 c_23;
  mediump vec3 tmpvar_24;
  tmpvar_24 = (tmpvar_12 * light_6.xyz);
  c_23.xyz = tmpvar_24;
  c_23.w = 0.0;
  c_2 = c_23;
  c_2.xyz = (c_2.xyz + tmpvar_9);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
attribute vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_7;
  tmpvar_7 = (_glesVertex.xyz - ((_World2Object * tmpvar_6).xyz * unity_Scale.w));
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_7 - (2.0 * 
    (dot (tmpvar_2, tmpvar_7) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * tmpvar_2);
  tmpvar_4 = tmpvar_11;
  highp vec4 o_12;
  highp vec4 tmpvar_13;
  tmpvar_13 = (tmpvar_5 * 0.5);
  highp vec2 tmpvar_14;
  tmpvar_14.x = tmpvar_13.x;
  tmpvar_14.y = (tmpvar_13.y * _ProjectionParams.x);
  o_12.xy = (tmpvar_14 + tmpvar_13.w);
  o_12.zw = tmpvar_5.zw;
  highp vec3 tmpvar_15;
  highp vec3 tmpvar_16;
  tmpvar_15 = tmpvar_1.xyz;
  tmpvar_16 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_17;
  tmpvar_17[0].x = tmpvar_15.x;
  tmpvar_17[0].y = tmpvar_16.x;
  tmpvar_17[0].z = tmpvar_2.x;
  tmpvar_17[1].x = tmpvar_15.y;
  tmpvar_17[1].y = tmpvar_16.y;
  tmpvar_17[1].z = tmpvar_2.y;
  tmpvar_17[2].x = tmpvar_15.z;
  tmpvar_17[2].y = tmpvar_16.z;
  tmpvar_17[2].z = tmpvar_2.z;
  highp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_17 * ((
    (_World2Object * tmpvar_18)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_12;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying lowp vec3 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying highp vec2 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = textureCube (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = texture2DProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec3 lm_16;
  lowp vec3 tmpvar_17;
  tmpvar_17 = (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lm_16 = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 0.0;
  tmpvar_18.xyz = lm_16;
  mediump vec4 tmpvar_19;
  tmpvar_19 = (max (light_3, vec4(0.001, 0.001, 0.001, 0.001)) + tmpvar_18);
  light_3 = tmpvar_19;
  lowp vec4 c_20;
  mediump vec3 tmpvar_21;
  tmpvar_21 = (tmpvar_9 * tmpvar_19.xyz);
  c_20.xyz = tmpvar_21;
  c_20.w = 0.0;
  c_2 = c_20;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
in vec4 _glesTANGENT;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 unity_LightmapST;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out lowp vec3 xlv_TEXCOORD2;
out highp vec3 xlv_TEXCOORD3;
out highp vec4 xlv_TEXCOORD4;
out highp vec2 xlv_TEXCOORD5;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  mediump vec3 tmpvar_3;
  lowp vec3 tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_6;
  tmpvar_6.w = 1.0;
  tmpvar_6.xyz = _WorldSpaceCameraPos;
  highp vec3 tmpvar_7;
  tmpvar_7 = (_glesVertex.xyz - ((_World2Object * tmpvar_6).xyz * unity_Scale.w));
  highp mat3 tmpvar_8;
  tmpvar_8[0] = _Object2World[0].xyz;
  tmpvar_8[1] = _Object2World[1].xyz;
  tmpvar_8[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8 * (tmpvar_7 - (2.0 * 
    (dot (tmpvar_2, tmpvar_7) * tmpvar_2)
  )));
  tmpvar_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10 * tmpvar_2);
  tmpvar_4 = tmpvar_11;
  highp vec4 o_12;
  highp vec4 tmpvar_13;
  tmpvar_13 = (tmpvar_5 * 0.5);
  highp vec2 tmpvar_14;
  tmpvar_14.x = tmpvar_13.x;
  tmpvar_14.y = (tmpvar_13.y * _ProjectionParams.x);
  o_12.xy = (tmpvar_14 + tmpvar_13.w);
  o_12.zw = tmpvar_5.zw;
  highp vec3 tmpvar_15;
  highp vec3 tmpvar_16;
  tmpvar_15 = tmpvar_1.xyz;
  tmpvar_16 = (((tmpvar_2.yzx * tmpvar_1.zxy) - (tmpvar_2.zxy * tmpvar_1.yzx)) * _glesTANGENT.w);
  highp mat3 tmpvar_17;
  tmpvar_17[0].x = tmpvar_15.x;
  tmpvar_17[0].y = tmpvar_16.x;
  tmpvar_17[0].z = tmpvar_2.x;
  tmpvar_17[1].x = tmpvar_15.y;
  tmpvar_17[1].y = tmpvar_16.y;
  tmpvar_17[1].z = tmpvar_2.y;
  tmpvar_17[2].x = tmpvar_15.z;
  tmpvar_17[2].y = tmpvar_16.z;
  tmpvar_17[2].z = tmpvar_2.z;
  highp vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = _WorldSpaceCameraPos;
  gl_Position = tmpvar_5;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = (tmpvar_17 * ((
    (_World2Object * tmpvar_18)
  .xyz * unity_Scale.w) - _glesVertex.xyz));
  xlv_TEXCOORD4 = o_12;
  xlv_TEXCOORD5 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp vec4 _Color;
uniform lowp samplerCube _Cubemap;
uniform highp vec4 _RimColor;
uniform highp float _RimPower;
uniform highp float _ReflAmount;
uniform sampler2D _LightBuffer;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in lowp vec3 xlv_TEXCOORD2;
in highp vec3 xlv_TEXCOORD3;
in highp vec4 xlv_TEXCOORD4;
in highp vec2 xlv_TEXCOORD5;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  highp vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD1;
  tmpvar_4 = xlv_TEXCOORD2;
  lowp vec3 tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0);
  highp vec4 tmpvar_8;
  tmpvar_8 = clamp (((_Color + 1.0) - tmpvar_7.w), 0.0, 1.0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_7.xyz * tmpvar_8.xyz);
  highp float tmpvar_10;
  tmpvar_10 = clamp (dot (normalize(xlv_TEXCOORD3), tmpvar_4), 0.0, 1.0);
  mediump float tmpvar_11;
  tmpvar_11 = (1.0 - tmpvar_10);
  highp vec3 tmpvar_12;
  tmpvar_12 = (_RimColor.xyz * pow (tmpvar_11, _RimPower));
  tmpvar_6 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture (_Cubemap, tmpvar_5);
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_6 + ((tmpvar_13.xyz * _ReflAmount) * tmpvar_7.w));
  tmpvar_6 = tmpvar_14;
  lowp vec4 tmpvar_15;
  tmpvar_15 = textureProj (_LightBuffer, xlv_TEXCOORD4);
  light_3 = tmpvar_15;
  mediump vec3 lm_16;
  lowp vec3 tmpvar_17;
  tmpvar_17 = (2.0 * texture (unity_Lightmap, xlv_TEXCOORD5).xyz);
  lm_16 = tmpvar_17;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 0.0;
  tmpvar_18.xyz = lm_16;
  mediump vec4 tmpvar_19;
  tmpvar_19 = (max (light_3, vec4(0.001, 0.001, 0.001, 0.001)) + tmpvar_18);
  light_3 = tmpvar_19;
  lowp vec4 c_20;
  mediump vec3 tmpvar_21;
  tmpvar_21 = (tmpvar_9 * tmpvar_19.xyz);
  c_20.xyz = tmpvar_21;
  c_20.w = 0.0;
  c_2 = c_20;
  c_2.xyz = (c_2.xyz + tmpvar_6);
  tmpvar_1 = c_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_ON" "DIRLIGHTMAP_ON" "HDR_LIGHT_PREPASS_ON" }
"!!GLES3"
}
}
 }
}
Fallback "Diffuse"
}