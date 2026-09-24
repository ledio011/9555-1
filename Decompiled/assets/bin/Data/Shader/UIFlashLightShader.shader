//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "UI/UIFlashLightShader" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _WidthRate ("Sprite.width/Atlas.width", Float) = 1
 _HeightRate ("Sprite.height/Atlas.height", Float) = 1
 _XOffset ("offsetX/Atlas.width", Float) = 0
 _YOffset ("offsetY/Atlas.height", Float) = 0
 _FlowLightTex ("FlowLight Texture", 2D) = "white" {}
 _FlowLightPower ("FlowLight Power", Float) = 1
 _IsOpenFlowLight ("IsOpenFlowLight", Float) = 0
 _FlowLightOffset ("FlowLight Offset", Float) = 0
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
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_COLOR = _glesColor;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp float _WidthRate;
uniform highp float _HeightRate;
uniform highp float _XOffset;
uniform highp float _YOffset;
uniform sampler2D _FlowLightTex;
uniform highp float _FlowLightPower;
uniform highp float _IsOpenFlowLight;
uniform highp float _FlowLightOffset;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec4 colorFlowLight_1;
  highp vec2 flow_uv_2;
  lowp vec4 col_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = (texture2D (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
  col_3.w = tmpvar_4.w;
  highp vec2 tmpvar_5;
  tmpvar_5.x = ((xlv_TEXCOORD0.x - _XOffset) / _WidthRate);
  tmpvar_5.y = ((xlv_TEXCOORD0.y - _YOffset) / _HeightRate);
  flow_uv_2.y = tmpvar_5.y;
  flow_uv_2.x = (tmpvar_5.x - _FlowLightOffset);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_FlowLightTex, flow_uv_2);
  highp vec4 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * _FlowLightPower);
  colorFlowLight_1 = tmpvar_7;
  colorFlowLight_1.xyz = (colorFlowLight_1.xyz * tmpvar_4.xyz);
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_4.xyz + (colorFlowLight_1.xyz * float(
    (1.0 >= _IsOpenFlowLight)
  )));
  col_3.xyz = tmpvar_8;
  gl_FragData[0] = col_3;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec4 xlv_COLOR;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_COLOR = _glesColor;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp float _WidthRate;
uniform highp float _HeightRate;
uniform highp float _XOffset;
uniform highp float _YOffset;
uniform sampler2D _FlowLightTex;
uniform highp float _FlowLightPower;
uniform highp float _IsOpenFlowLight;
uniform highp float _FlowLightOffset;
in mediump vec2 xlv_TEXCOORD0;
in lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec4 colorFlowLight_1;
  highp vec2 flow_uv_2;
  lowp vec4 col_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = (texture (_MainTex, xlv_TEXCOORD0) * xlv_COLOR);
  col_3.w = tmpvar_4.w;
  highp vec2 tmpvar_5;
  tmpvar_5.x = ((xlv_TEXCOORD0.x - _XOffset) / _WidthRate);
  tmpvar_5.y = ((xlv_TEXCOORD0.y - _YOffset) / _HeightRate);
  flow_uv_2.y = tmpvar_5.y;
  flow_uv_2.x = (tmpvar_5.x - _FlowLightOffset);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture (_FlowLightTex, flow_uv_2);
  highp vec4 tmpvar_7;
  tmpvar_7 = (tmpvar_6 * _FlowLightPower);
  colorFlowLight_1 = tmpvar_7;
  colorFlowLight_1.xyz = (colorFlowLight_1.xyz * tmpvar_4.xyz);
  highp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_4.xyz + (colorFlowLight_1.xyz * float(
    (1.0 >= _IsOpenFlowLight)
  )));
  col_3.xyz = tmpvar_8;
  _glesFragData[0] = col_3;
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