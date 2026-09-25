//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Outline_/PlayerXRay" {
Properties {
 _Color ("Color (RGB)", Color) = (0.745,0.631,0.529,1)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent-20" "RenderType"="RealTimeShadow" }
 Pass {
  Tags { "QUEUE"="Transparent-20" "RenderType"="RealTimeShadow" }
  ZTest Greater
  ZWrite Off
  Blend One One
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _Color;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_COLOR;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xyz;
  highp float N_2;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  tmpvar_3 = _Color;
  highp float tmpvar_6;
  tmpvar_6 = dot (normalize((
    ((_World2Object * tmpvar_5).xyz * unity_Scale.w)
   - _glesVertex.xyz)), normalize(_glesNormal));
  N_2 = tmpvar_6;
  if ((tmpvar_6 < 0.0)) {
    N_2 = 0.0;
  };
  tmpvar_3.xyz = (_Color * N_2).xyz;
  tmpvar_3.w = 0.0;
  xlv_TEXCOORD0 = tmpvar_1.xy;
  gl_Position = tmpvar_4;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT

varying highp vec4 xlv_COLOR;
void main ()
{
  gl_FragData[0] = xlv_COLOR;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_Scale;
uniform highp vec4 _Color;
out highp vec2 xlv_TEXCOORD0;
out highp vec4 xlv_COLOR;
void main ()
{
  highp vec3 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xyz;
  highp float N_2;
  highp vec4 tmpvar_3;
  highp vec4 tmpvar_4;
  tmpvar_4 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 tmpvar_5;
  tmpvar_5.w = 1.0;
  tmpvar_5.xyz = _WorldSpaceCameraPos;
  tmpvar_3 = _Color;
  highp float tmpvar_6;
  tmpvar_6 = dot (normalize((
    ((_World2Object * tmpvar_5).xyz * unity_Scale.w)
   - _glesVertex.xyz)), normalize(_glesNormal));
  N_2 = tmpvar_6;
  if ((tmpvar_6 < 0.0)) {
    N_2 = 0.0;
  };
  tmpvar_3.xyz = (_Color * N_2).xyz;
  tmpvar_3.w = 0.0;
  xlv_TEXCOORD0 = tmpvar_1.xy;
  gl_Position = tmpvar_4;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
in highp vec4 xlv_COLOR;
void main ()
{
  _glesFragData[0] = xlv_COLOR;
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