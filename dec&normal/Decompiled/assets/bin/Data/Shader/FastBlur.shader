//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Custom/FastBlur" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _MainTex_TexelSize;
uniform mediump float _Parameter;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  tmpvar_1.zw = vec2(1.0, 1.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_MainTex_TexelSize.xy * vec2(0.0, 1.0)) * _Parameter);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 coords_2;
  coords_2 = (xlv_TEXCOORD0.xy - (xlv_TEXCOORD1 * 2.0));
  mediump vec4 tap_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, coords_2);
  tap_3 = tmpvar_4;
  color_1 = (tap_3 * 0.0938);
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, coords_2);
  tap_5 = tmpvar_6;
  color_1 = (color_1 + (tap_5 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, coords_2);
  tap_7 = tmpvar_8;
  color_1 = (color_1 + (tap_7 * 0.3125));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, coords_2);
  tap_9 = tmpvar_10;
  color_1 = (color_1 + (tap_9 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture2D (_MainTex, coords_2);
  tap_11 = tmpvar_12;
  color_1 = (color_1 + (tap_11 * 0.0938));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _MainTex_TexelSize;
uniform mediump float _Parameter;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  tmpvar_1.zw = vec2(1.0, 1.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_MainTex_TexelSize.xy * vec2(0.0, 1.0)) * _Parameter);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 coords_2;
  coords_2 = (xlv_TEXCOORD0.xy - (xlv_TEXCOORD1 * 2.0));
  mediump vec4 tap_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_MainTex, coords_2);
  tap_3 = tmpvar_4;
  color_1 = (tap_3 * 0.0938);
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture (_MainTex, coords_2);
  tap_5 = tmpvar_6;
  color_1 = (color_1 + (tap_5 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture (_MainTex, coords_2);
  tap_7 = tmpvar_8;
  color_1 = (color_1 + (tap_7 * 0.3125));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_MainTex, coords_2);
  tap_9 = tmpvar_10;
  color_1 = (color_1 + (tap_9 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_MainTex, coords_2);
  tap_11 = tmpvar_12;
  color_1 = (color_1 + (tap_11 * 0.0938));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  _glesFragData[0] = color_1;
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
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _MainTex_TexelSize;
uniform mediump float _Parameter;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  tmpvar_1.zw = vec2(1.0, 1.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_MainTex_TexelSize.xy * vec2(1.0, 0.0)) * _Parameter);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 coords_2;
  coords_2 = (xlv_TEXCOORD0.xy - (xlv_TEXCOORD1 * 2.0));
  mediump vec4 tap_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, coords_2);
  tap_3 = tmpvar_4;
  color_1 = (tap_3 * 0.0938);
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, coords_2);
  tap_5 = tmpvar_6;
  color_1 = (color_1 + (tap_5 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, coords_2);
  tap_7 = tmpvar_8;
  color_1 = (color_1 + (tap_7 * 0.3125));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, coords_2);
  tap_9 = tmpvar_10;
  color_1 = (color_1 + (tap_9 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture2D (_MainTex, coords_2);
  tap_11 = tmpvar_12;
  color_1 = (color_1 + (tap_11 * 0.0938));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _MainTex_TexelSize;
uniform mediump float _Parameter;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  tmpvar_1.zw = vec2(1.0, 1.0);
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = ((_MainTex_TexelSize.xy * vec2(1.0, 0.0)) * _Parameter);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 color_1;
  mediump vec2 coords_2;
  coords_2 = (xlv_TEXCOORD0.xy - (xlv_TEXCOORD1 * 2.0));
  mediump vec4 tap_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_MainTex, coords_2);
  tap_3 = tmpvar_4;
  color_1 = (tap_3 * 0.0938);
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_5;
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture (_MainTex, coords_2);
  tap_5 = tmpvar_6;
  color_1 = (color_1 + (tap_5 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture (_MainTex, coords_2);
  tap_7 = tmpvar_8;
  color_1 = (color_1 + (tap_7 * 0.3125));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_MainTex, coords_2);
  tap_9 = tmpvar_10;
  color_1 = (color_1 + (tap_9 * 0.2344));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  mediump vec4 tap_11;
  lowp vec4 tmpvar_12;
  tmpvar_12 = texture (_MainTex, coords_2);
  tap_11 = tmpvar_12;
  color_1 = (color_1 + (tap_11 * 0.0938));
  coords_2 = (coords_2 + xlv_TEXCOORD1);
  _glesFragData[0] = color_1;
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
Fallback Off
}