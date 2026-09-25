//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Map/Minimap" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _CenterX ("CenterX", Range(0,1)) = 0.5
 _CenterY ("CenterY", Range(0,1)) = 0.5
 _Radius ("Radius", Range(0,1)) = 1
 _WHRatio ("WHRatio", Float) = 1
 _Alph ("Alph", Range(0,1)) = 1
 _OutLine ("OutLine", Float) = 0.003
 _PoliceCenterX ("PoliceCenterX", Range(0,1)) = 0.5
 _PoliceCenterY ("PoliceCenterY", Range(0,1)) = 0.5
 _PoliceColor ("PoliceColor", Color) = (1,0,0,0)
 _PoliceRadius ("PoliceRadius", Range(0,1)) = 0.4
 _LockShowRange ("r value above this value will show", Float) = 1.09
 _LockAreaColor ("LockAreaColor", Color) = (0,0,1,0.5)
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  Offset -1, -1
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  lowp vec4 tmpvar_3;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp float _CenterX;
uniform highp float _CenterY;
uniform highp float _Radius;
uniform highp float _WHRatio;
uniform highp float _OutLine;
uniform highp float _PoliceCenterX;
uniform highp float _PoliceCenterY;
uniform lowp vec4 _PoliceColor;
uniform highp float _PoliceRadius;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 col_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  highp float tmpvar_3;
  tmpvar_3 = abs((xlv_TEXCOORD0.x - _CenterX));
  highp float tmpvar_4;
  tmpvar_4 = (abs((xlv_TEXCOORD0.y - _CenterY)) / _WHRatio);
  highp float tmpvar_5;
  tmpvar_5 = (tmpvar_3 * tmpvar_3);
  highp float tmpvar_6;
  tmpvar_6 = (tmpvar_4 * tmpvar_4);
  highp float tmpvar_7;
  tmpvar_7 = (_Radius * _Radius);
  highp float tmpvar_8;
  tmpvar_8 = abs((xlv_TEXCOORD0.x - _PoliceCenterX));
  highp float tmpvar_9;
  tmpvar_9 = (abs((xlv_TEXCOORD0.y - _PoliceCenterY)) / _WHRatio);
  highp float tmpvar_10;
  tmpvar_10 = (_PoliceColor.w * float((
    (_PoliceRadius * _PoliceRadius)
   >= 
    ((tmpvar_8 * tmpvar_8) + (tmpvar_9 * tmpvar_9))
  )));
  highp vec4 tmpvar_11;
  tmpvar_11 = ((tmpvar_2 * (1.0 - tmpvar_10)) + (_PoliceColor * tmpvar_10));
  col_1 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12 = (col_1 * float((
    (tmpvar_7 - _OutLine)
   >= 
    (tmpvar_5 + tmpvar_6)
  )));
  col_1.xyz = tmpvar_12.xyz;
  highp float tmpvar_13;
  tmpvar_13 = float((tmpvar_7 >= (tmpvar_5 + tmpvar_6)));
  col_1.w = tmpvar_13;
  gl_FragData[0] = col_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec4 xlv_COLOR;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  lowp vec4 tmpvar_3;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp float _CenterX;
uniform highp float _CenterY;
uniform highp float _Radius;
uniform highp float _WHRatio;
uniform highp float _OutLine;
uniform highp float _PoliceCenterX;
uniform highp float _PoliceCenterY;
uniform lowp vec4 _PoliceColor;
uniform highp float _PoliceRadius;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 col_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0);
  highp float tmpvar_3;
  tmpvar_3 = abs((xlv_TEXCOORD0.x - _CenterX));
  highp float tmpvar_4;
  tmpvar_4 = (abs((xlv_TEXCOORD0.y - _CenterY)) / _WHRatio);
  highp float tmpvar_5;
  tmpvar_5 = (tmpvar_3 * tmpvar_3);
  highp float tmpvar_6;
  tmpvar_6 = (tmpvar_4 * tmpvar_4);
  highp float tmpvar_7;
  tmpvar_7 = (_Radius * _Radius);
  highp float tmpvar_8;
  tmpvar_8 = abs((xlv_TEXCOORD0.x - _PoliceCenterX));
  highp float tmpvar_9;
  tmpvar_9 = (abs((xlv_TEXCOORD0.y - _PoliceCenterY)) / _WHRatio);
  highp float tmpvar_10;
  tmpvar_10 = (_PoliceColor.w * float((
    (_PoliceRadius * _PoliceRadius)
   >= 
    ((tmpvar_8 * tmpvar_8) + (tmpvar_9 * tmpvar_9))
  )));
  highp vec4 tmpvar_11;
  tmpvar_11 = ((tmpvar_2 * (1.0 - tmpvar_10)) + (_PoliceColor * tmpvar_10));
  col_1 = tmpvar_11;
  highp vec4 tmpvar_12;
  tmpvar_12 = (col_1 * float((
    (tmpvar_7 - _OutLine)
   >= 
    (tmpvar_5 + tmpvar_6)
  )));
  col_1.xyz = tmpvar_12.xyz;
  highp float tmpvar_13;
  tmpvar_13 = float((tmpvar_7 >= (tmpvar_5 + tmpvar_6)));
  col_1.w = tmpvar_13;
  _glesFragData[0] = col_1;
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
}