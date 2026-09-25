//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Water/Water_jianhuashui" {
Properties {
 _Normal ("Normal", 2D) = "bump" {}
 _MainColor ("Base Ocean Color", Color) = (0.1137,0.4,0.31,1)
 _MainAlpha ("Base Alpha", Range(0,1)) = 0.8
 _ReflectionTex ("Reflection Texture", 2D) = "white" {}
 _ReflectionBias ("Reflection Bias", Float) = 0.01
 _ReflectionAlpha ("Reflection Alpha", Range(0,1)) = 1
 _Shininess ("Specular Shininess", Range(2,500)) = 200
 _FogColor ("Ocean Fog color", Color) = (0.11,0.11,0.32,1)
 _FogStrength ("Fog Strength", Range(0,1)) = 0.3
 _FogShininess ("Fog Shininess", Range(0,10)) = 2
 _InvFadeParemeter ("Edge Blend", Float) = 0.04
 _DirectionUv ("Wet scroll direction (2 samples)", Vector) = (2,2,0,-1)
 _TexAtlasTiling ("Tex atlas tiling", Vector) = (8,8,20,20)
 _SunPos ("Sun light position)", Vector) = (0,0,0,0)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "RenderType"="Transparent" }
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform mediump vec4 _DirectionUv;
uniform mediump vec4 _TexAtlasTiling;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0;
  mediump vec4 tmpvar_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7 = (_Object2World * _glesVertex);
  tmpvar_4 = tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_4.xyz - _WorldSpaceCameraPos);
  tmpvar_4.xyz = tmpvar_8;
  tmpvar_4.w = sqrt(dot (tmpvar_4, tmpvar_4));
  highp vec4 tmpvar_9;
  tmpvar_9 = ((_glesMultiTexCoord0.xyxy * _TexAtlasTiling) + fract((
    (_Time.xxxx * _DirectionUv.xyxy)
   * _DirectionUv.w)));
  tmpvar_2 = tmpvar_9;
  tmpvar_5 = tmpvar_1;
  highp vec4 o_10;
  highp vec4 tmpvar_11;
  tmpvar_11 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_12;
  tmpvar_12.x = tmpvar_11.x;
  tmpvar_12.y = (tmpvar_11.y * _ProjectionParams.x);
  o_10.xy = (tmpvar_12 + tmpvar_11.w);
  o_10.zw = tmpvar_6.zw;
  tmpvar_3 = o_10;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform lowp vec4 _MainColor;
uniform mediump float _MainAlpha;
uniform mediump float _ReflectionBias;
uniform mediump float _ReflectionAlpha;
uniform lowp vec4 _FogColor;
uniform mediump float _FogStrength;
uniform mediump float _Shininess;
uniform mediump float _FogShininess;
uniform mediump vec4 _SunPos;
uniform sampler2D _Normal;
uniform sampler2D _ReflectionTex;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 baseColor_1;
  lowp float t_fogScale_2;
  lowp float t_ShadowAlpha_3;
  lowp vec4 rtRefl_4;
  lowp vec4 rtReflNorm_5;
  lowp float spec_6;
  lowp vec3 h_7;
  lowp vec3 viewVector_8;
  lowp vec4 bump_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_Normal, xlv_TEXCOORD0.zw);
  lowp vec4 tmpvar_11;
  tmpvar_11 = (texture2D (_Normal, xlv_TEXCOORD0.xy) + tmpvar_10);
  bump_9.zw = tmpvar_11.zw;
  bump_9.xy = (tmpvar_11.wy - vec2(1.0, 1.0));
  lowp vec3 tmpvar_12;
  tmpvar_12 = normalize((vec3(0.0, 1.0, 0.0) + (bump_9.xxy * vec3(1.0, 0.0, 1.0))));
  mediump vec3 tmpvar_13;
  tmpvar_13 = normalize(xlv_TEXCOORD2.xyz);
  viewVector_8 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize((_SunPos.xyz + viewVector_8));
  h_7 = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = max (0.0, dot (normalize(
    (viewVector_8 - (2.0 * (dot (tmpvar_12, viewVector_8) * tmpvar_12)))
  ), h_7));
  mediump float tmpvar_16;
  tmpvar_16 = max (0.0, pow (tmpvar_15, _Shininess));
  spec_6 = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = ((tmpvar_10 - 0.5) * _ReflectionBias);
  rtReflNorm_5 = tmpvar_17;
  lowp vec4 tmpvar_18;
  mediump vec2 P_19;
  P_19 = (xlv_TEXCOORD3.xy + rtReflNorm_5.xy);
  tmpvar_18 = texture2D (_ReflectionTex, P_19);
  rtRefl_4.xyz = tmpvar_18.xyz;
  mediump float tmpvar_20;
  tmpvar_20 = (tmpvar_18.w * _ReflectionAlpha);
  t_ShadowAlpha_3 = tmpvar_20;
  rtRefl_4.w = 1.0;
  mediump float tmpvar_21;
  lowp float x_22;
  x_22 = (1.0 + viewVector_8.y);
  tmpvar_21 = pow (x_22, _FogShininess);
  t_fogScale_2 = tmpvar_21;
  lowp float tmpvar_23;
  tmpvar_23 = min (max (-0.2, t_fogScale_2), 1.0);
  mediump vec4 tmpvar_24;
  tmpvar_24 = ((_MainColor + (rtRefl_4 * t_ShadowAlpha_3)) + (tmpvar_23 * (_FogColor + 
    ((spec_6 + tmpvar_15) * _FogStrength)
  )));
  baseColor_1.xyz = tmpvar_24.xyz;
  baseColor_1.w = _MainAlpha;
  gl_FragData[0] = baseColor_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp vec4 _ProjectionParams;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform mediump vec4 _DirectionUv;
uniform mediump vec4 _TexAtlasTiling;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
out mediump vec4 xlv_TEXCOORD2;
out mediump vec4 xlv_TEXCOORD3;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0;
  mediump vec4 tmpvar_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_7;
  tmpvar_7 = (_Object2World * _glesVertex);
  tmpvar_4 = tmpvar_7;
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_4.xyz - _WorldSpaceCameraPos);
  tmpvar_4.xyz = tmpvar_8;
  tmpvar_4.w = sqrt(dot (tmpvar_4, tmpvar_4));
  highp vec4 tmpvar_9;
  tmpvar_9 = ((_glesMultiTexCoord0.xyxy * _TexAtlasTiling) + fract((
    (_Time.xxxx * _DirectionUv.xyxy)
   * _DirectionUv.w)));
  tmpvar_2 = tmpvar_9;
  tmpvar_5 = tmpvar_1;
  highp vec4 o_10;
  highp vec4 tmpvar_11;
  tmpvar_11 = (tmpvar_6 * 0.5);
  highp vec2 tmpvar_12;
  tmpvar_12.x = tmpvar_11.x;
  tmpvar_12.y = (tmpvar_11.y * _ProjectionParams.x);
  o_10.xy = (tmpvar_12 + tmpvar_11.w);
  o_10.zw = tmpvar_6.zw;
  tmpvar_3 = o_10;
  gl_Position = tmpvar_6;
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform lowp vec4 _MainColor;
uniform mediump float _MainAlpha;
uniform mediump float _ReflectionBias;
uniform mediump float _ReflectionAlpha;
uniform lowp vec4 _FogColor;
uniform mediump float _FogStrength;
uniform mediump float _Shininess;
uniform mediump float _FogShininess;
uniform mediump vec4 _SunPos;
uniform sampler2D _Normal;
uniform sampler2D _ReflectionTex;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD2;
in mediump vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 baseColor_1;
  lowp float t_fogScale_2;
  lowp float t_ShadowAlpha_3;
  lowp vec4 rtRefl_4;
  lowp vec4 rtReflNorm_5;
  lowp float spec_6;
  lowp vec3 h_7;
  lowp vec3 viewVector_8;
  lowp vec4 bump_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_Normal, xlv_TEXCOORD0.zw);
  lowp vec4 tmpvar_11;
  tmpvar_11 = (texture (_Normal, xlv_TEXCOORD0.xy) + tmpvar_10);
  bump_9.zw = tmpvar_11.zw;
  bump_9.xy = (tmpvar_11.wy - vec2(1.0, 1.0));
  lowp vec3 tmpvar_12;
  tmpvar_12 = normalize((vec3(0.0, 1.0, 0.0) + (bump_9.xxy * vec3(1.0, 0.0, 1.0))));
  mediump vec3 tmpvar_13;
  tmpvar_13 = normalize(xlv_TEXCOORD2.xyz);
  viewVector_8 = tmpvar_13;
  mediump vec3 tmpvar_14;
  tmpvar_14 = normalize((_SunPos.xyz + viewVector_8));
  h_7 = tmpvar_14;
  lowp float tmpvar_15;
  tmpvar_15 = max (0.0, dot (normalize(
    (viewVector_8 - (2.0 * (dot (tmpvar_12, viewVector_8) * tmpvar_12)))
  ), h_7));
  mediump float tmpvar_16;
  tmpvar_16 = max (0.0, pow (tmpvar_15, _Shininess));
  spec_6 = tmpvar_16;
  mediump vec4 tmpvar_17;
  tmpvar_17 = ((tmpvar_10 - 0.5) * _ReflectionBias);
  rtReflNorm_5 = tmpvar_17;
  lowp vec4 tmpvar_18;
  mediump vec2 P_19;
  P_19 = (xlv_TEXCOORD3.xy + rtReflNorm_5.xy);
  tmpvar_18 = texture (_ReflectionTex, P_19);
  rtRefl_4.xyz = tmpvar_18.xyz;
  mediump float tmpvar_20;
  tmpvar_20 = (tmpvar_18.w * _ReflectionAlpha);
  t_ShadowAlpha_3 = tmpvar_20;
  rtRefl_4.w = 1.0;
  mediump float tmpvar_21;
  lowp float x_22;
  x_22 = (1.0 + viewVector_8.y);
  tmpvar_21 = pow (x_22, _FogShininess);
  t_fogScale_2 = tmpvar_21;
  lowp float tmpvar_23;
  tmpvar_23 = min (max (-0.2, t_fogScale_2), 1.0);
  mediump vec4 tmpvar_24;
  tmpvar_24 = ((_MainColor + (rtRefl_4 * t_ShadowAlpha_3)) + (tmpvar_23 * (_FogColor + 
    ((spec_6 + tmpvar_15) * _FogStrength)
  )));
  baseColor_1.xyz = tmpvar_24.xyz;
  baseColor_1.w = _MainAlpha;
  _glesFragData[0] = baseColor_1;
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
}
Fallback "Diffuse"
}