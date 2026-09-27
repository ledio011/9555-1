//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Custom/EdgeAnima" {
Properties {
 _Edge ("Edge", Range(0,0.2)) = 0.043
 _EdgeColor ("EdgeColor", Color) = (1,1,1,1)
 _FlowColor ("FlowColor", Color) = (1,1,1,1)
 _FlowLight ("FlowLight", Float) = 2
 _FlowSpeed ("FlowSpeed", Range(0,10)) = 3
 _FlowLength ("FlowLength", Float) = 0.5
 _MainTex ("MainTex", 2D) = "white" {}
 _Ratio ("Ratio", Float) = 1
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
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 1.0;
  tmpvar_3.xyz = _glesVertex.xyz;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * tmpvar_3);
  xlv_TEXCOORD0 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform highp vec4 _Time;
uniform lowp float _Edge;
uniform lowp vec4 _EdgeColor;
uniform lowp vec4 _FlowColor;
uniform highp float _FlowLight;
uniform highp float _FlowLength;
uniform highp float _FlowSpeed;
uniform highp float _Ratio;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp float y_2;
  lowp float x_3;
  mediump float tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD0.x;
  x_3 = tmpvar_4;
  mediump float tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD0.y;
  y_2 = tmpvar_5;
  bool tmpvar_6;
  if ((x_3 < (_Edge / _Ratio))) {
    tmpvar_6 = bool(1);
  } else {
    tmpvar_6 = (abs((1.0 - x_3)) < (_Edge / _Ratio));
  };
  bool tmpvar_7;
  if ((tmpvar_6 || (y_2 < _Edge))) {
    tmpvar_7 = bool(1);
  } else {
    tmpvar_7 = (abs((1.0 - y_2)) < _Edge);
  };
  if (tmpvar_7) {
    highp vec2 rotUV_8;
    highp float tmpvar_9;
    tmpvar_9 = (_Time.y * _FlowSpeed);
    lowp float tmpvar_10;
    tmpvar_10 = (x_3 - 0.5);
    x_3 = tmpvar_10;
    lowp float tmpvar_11;
    tmpvar_11 = (y_2 - 0.5);
    y_2 = tmpvar_11;
    rotUV_8.x = (((tmpvar_10 * 
      cos(tmpvar_9)
    ) - (tmpvar_11 * 
      sin(tmpvar_9)
    )) + 0.5);
    rotUV_8.y = (((tmpvar_10 * 
      sin(tmpvar_9)
    ) + (tmpvar_11 * 
      cos(tmpvar_9)
    )) + 0.5);
    lowp float tmpvar_12;
    highp float tmpvar_13;
    tmpvar_13 = clamp (((rotUV_8.x - _FlowLength) * (_FlowLength - rotUV_8.y)), 0.0, 1.0);
    tmpvar_12 = tmpvar_13;
    tmpvar_1 = ((_EdgeColor * (1.0 - tmpvar_12)) + ((_FlowColor * tmpvar_12) * _FlowLight));
  } else {
    tmpvar_1 = vec4(1.0, 1.0, 1.0, 0.0);
  };
  gl_FragData[0] = tmpvar_1;
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
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3.w = 1.0;
  tmpvar_3.xyz = _glesVertex.xyz;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * tmpvar_3);
  xlv_TEXCOORD0 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform highp vec4 _Time;
uniform lowp float _Edge;
uniform lowp vec4 _EdgeColor;
uniform lowp vec4 _FlowColor;
uniform highp float _FlowLight;
uniform highp float _FlowLength;
uniform highp float _FlowSpeed;
uniform highp float _Ratio;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp float y_2;
  lowp float x_3;
  mediump float tmpvar_4;
  tmpvar_4 = xlv_TEXCOORD0.x;
  x_3 = tmpvar_4;
  mediump float tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD0.y;
  y_2 = tmpvar_5;
  bool tmpvar_6;
  if ((x_3 < (_Edge / _Ratio))) {
    tmpvar_6 = bool(1);
  } else {
    tmpvar_6 = (abs((1.0 - x_3)) < (_Edge / _Ratio));
  };
  bool tmpvar_7;
  if ((tmpvar_6 || (y_2 < _Edge))) {
    tmpvar_7 = bool(1);
  } else {
    tmpvar_7 = (abs((1.0 - y_2)) < _Edge);
  };
  if (tmpvar_7) {
    highp vec2 rotUV_8;
    highp float tmpvar_9;
    tmpvar_9 = (_Time.y * _FlowSpeed);
    lowp float tmpvar_10;
    tmpvar_10 = (x_3 - 0.5);
    x_3 = tmpvar_10;
    lowp float tmpvar_11;
    tmpvar_11 = (y_2 - 0.5);
    y_2 = tmpvar_11;
    rotUV_8.x = (((tmpvar_10 * 
      cos(tmpvar_9)
    ) - (tmpvar_11 * 
      sin(tmpvar_9)
    )) + 0.5);
    rotUV_8.y = (((tmpvar_10 * 
      sin(tmpvar_9)
    ) + (tmpvar_11 * 
      cos(tmpvar_9)
    )) + 0.5);
    lowp float tmpvar_12;
    highp float tmpvar_13;
    tmpvar_13 = clamp (((rotUV_8.x - _FlowLength) * (_FlowLength - rotUV_8.y)), 0.0, 1.0);
    tmpvar_12 = tmpvar_13;
    tmpvar_1 = ((_EdgeColor * (1.0 - tmpvar_12)) + ((_FlowColor * tmpvar_12) * _FlowLight));
  } else {
    tmpvar_1 = vec4(1.0, 1.0, 1.0, 0.0);
  };
  _glesFragData[0] = tmpvar_1;
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
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0.01
  ColorMask RGB
  ColorMaterial AmbientAndDiffuse
  Offset -1, -1
  SetTexture [_MainTex] { combine texture * primary }
 }
}
}