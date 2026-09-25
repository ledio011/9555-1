//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/Custom/EdgeAnima 1" {
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
 LOD 200
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
uniform highp vec4 _ClipRange0;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = 1.0;
  tmpvar_1.xyz = _glesVertex.xyz;
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD1 = ((_glesVertex.xy * _ClipRange0.zw) + _ClipRange0.xy);
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
uniform highp vec2 _ClipArgs0;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp float y_2;
  lowp float x_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = ((vec2(1.0, 1.0) - abs(xlv_TEXCOORD1)) * _ClipArgs0);
  highp float tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD0.x;
  x_3 = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = xlv_TEXCOORD0.y;
  y_2 = tmpvar_6;
  bool tmpvar_7;
  if ((x_3 < (_Edge / _Ratio))) {
    tmpvar_7 = bool(1);
  } else {
    tmpvar_7 = (abs((1.0 - x_3)) < (_Edge / _Ratio));
  };
  bool tmpvar_8;
  if ((tmpvar_7 || (y_2 < _Edge))) {
    tmpvar_8 = bool(1);
  } else {
    tmpvar_8 = (abs((1.0 - y_2)) < _Edge);
  };
  if (tmpvar_8) {
    lowp vec4 retFix_9;
    highp vec2 rotUV_10;
    highp float tmpvar_11;
    tmpvar_11 = (_Time.y * _FlowSpeed);
    lowp float tmpvar_12;
    tmpvar_12 = (x_3 - 0.5);
    x_3 = tmpvar_12;
    lowp float tmpvar_13;
    tmpvar_13 = (y_2 - 0.5);
    y_2 = tmpvar_13;
    rotUV_10.x = (((tmpvar_12 * 
      cos(tmpvar_11)
    ) - (tmpvar_13 * 
      sin(tmpvar_11)
    )) + 0.5);
    rotUV_10.y = (((tmpvar_12 * 
      sin(tmpvar_11)
    ) + (tmpvar_13 * 
      cos(tmpvar_11)
    )) + 0.5);
    lowp float tmpvar_14;
    highp float tmpvar_15;
    tmpvar_15 = clamp (((rotUV_10.x - _FlowLength) * (_FlowLength - rotUV_10.y)), 0.0, 1.0);
    tmpvar_14 = tmpvar_15;
    highp vec4 tmpvar_16;
    tmpvar_16 = ((_EdgeColor * (1.0 - tmpvar_14)) + ((_FlowColor * tmpvar_14) * _FlowLight));
    retFix_9 = tmpvar_16;
    highp float tmpvar_17;
    tmpvar_17 = (retFix_9.w * clamp (min (tmpvar_4.x, tmpvar_4.y), 0.0, 1.0));
    retFix_9.w = tmpvar_17;
    tmpvar_1 = retFix_9;
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
uniform highp vec4 _ClipRange0;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = 1.0;
  tmpvar_1.xyz = _glesVertex.xyz;
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD1 = ((_glesVertex.xy * _ClipRange0.zw) + _ClipRange0.xy);
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
uniform highp vec2 _ClipArgs0;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp float y_2;
  lowp float x_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = ((vec2(1.0, 1.0) - abs(xlv_TEXCOORD1)) * _ClipArgs0);
  highp float tmpvar_5;
  tmpvar_5 = xlv_TEXCOORD0.x;
  x_3 = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = xlv_TEXCOORD0.y;
  y_2 = tmpvar_6;
  bool tmpvar_7;
  if ((x_3 < (_Edge / _Ratio))) {
    tmpvar_7 = bool(1);
  } else {
    tmpvar_7 = (abs((1.0 - x_3)) < (_Edge / _Ratio));
  };
  bool tmpvar_8;
  if ((tmpvar_7 || (y_2 < _Edge))) {
    tmpvar_8 = bool(1);
  } else {
    tmpvar_8 = (abs((1.0 - y_2)) < _Edge);
  };
  if (tmpvar_8) {
    lowp vec4 retFix_9;
    highp vec2 rotUV_10;
    highp float tmpvar_11;
    tmpvar_11 = (_Time.y * _FlowSpeed);
    lowp float tmpvar_12;
    tmpvar_12 = (x_3 - 0.5);
    x_3 = tmpvar_12;
    lowp float tmpvar_13;
    tmpvar_13 = (y_2 - 0.5);
    y_2 = tmpvar_13;
    rotUV_10.x = (((tmpvar_12 * 
      cos(tmpvar_11)
    ) - (tmpvar_13 * 
      sin(tmpvar_11)
    )) + 0.5);
    rotUV_10.y = (((tmpvar_12 * 
      sin(tmpvar_11)
    ) + (tmpvar_13 * 
      cos(tmpvar_11)
    )) + 0.5);
    lowp float tmpvar_14;
    highp float tmpvar_15;
    tmpvar_15 = clamp (((rotUV_10.x - _FlowLength) * (_FlowLength - rotUV_10.y)), 0.0, 1.0);
    tmpvar_14 = tmpvar_15;
    highp vec4 tmpvar_16;
    tmpvar_16 = ((_EdgeColor * (1.0 - tmpvar_14)) + ((_FlowColor * tmpvar_14) * _FlowLight));
    retFix_9 = tmpvar_16;
    highp float tmpvar_17;
    tmpvar_17 = (retFix_9.w * clamp (min (tmpvar_4.x, tmpvar_4.y), 0.0, 1.0));
    retFix_9.w = tmpvar_17;
    tmpvar_1 = retFix_9;
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