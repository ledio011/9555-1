//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "FlagWind" {
Properties {
 _Color ("Mine Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
 _Wind ("Wind params", Vector) = (1,1,1,1)
 _WindEdgeFlutter ("Wind edge fultter factor", Float) = 0.5
 _WindEdgeFlutterFreqScale ("Wind edge fultter freq scale", Float) = 0.5
 _AlphaCutOut ("Alpha Cut Out ", Range(0,1)) = 0.5
}
SubShader { 
 LOD 100
 Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent+450" "RenderType"="Transparent" }
 Pass {
  Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent+450" "RenderType"="Transparent" }
  Cull Off
  Blend SrcAlpha OneMinusSrcAlpha
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 _Wind;
uniform mediump vec4 _MainTex_ST;
uniform mediump float _WindEdgeFlutter;
uniform mediump float _WindEdgeFlutterFreqScale;
varying mediump vec2 xlv_TEXCOORD0;
varying lowp vec3 xlv_TEXCOORD2;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = _glesVertex;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  lowp vec4 tmpvar_3;
  tmpvar_3 = _glesColor;
  mediump vec4 mdlPos_4;
  mediump float bendingFact_5;
  mediump vec4 wind_6;
  mediump vec2 tmpvar_7;
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_3.w;
  bendingFact_5 = tmpvar_8;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _World2Object[0].xyz;
  tmpvar_9[1] = _World2Object[1].xyz;
  tmpvar_9[2] = _World2Object[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * _Wind.xyz);
  wind_6.xyz = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = (_Wind.w * bendingFact_5);
  wind_6.w = tmpvar_11;
  highp vec4 tmpvar_12;
  mediump vec4 pos_13;
  pos_13 = tmpvar_1;
  mediump vec3 normal_14;
  normal_14 = tmpvar_2;
  mediump vec3 bend_15;
  mediump vec4 vWaves_16;
  highp vec4 v_17;
  v_17.x = _Object2World[0].w;
  v_17.y = _Object2World[1].w;
  v_17.z = _Object2World[2].w;
  v_17.w = _Object2World[3].w;
  mediump float tmpvar_18;
  tmpvar_18 = dot (v_17.xyz, vec3(1.0, 1.0, 1.0));
  mediump float tmpvar_19;
  tmpvar_19 = dot (pos_13.xyz, vec3((_WindEdgeFlutter + tmpvar_18)));
  mediump vec4 tmpvar_20;
  tmpvar_20.zw = vec2(1.0, 1.0);
  tmpvar_20.x = _WindEdgeFlutterFreqScale;
  tmpvar_20.y = _WindEdgeFlutterFreqScale;
  mediump vec4 tmpvar_21;
  tmpvar_21.x = tmpvar_19;
  tmpvar_21.y = tmpvar_19;
  tmpvar_21.z = tmpvar_18;
  tmpvar_21.w = tmpvar_18;
  highp vec4 tmpvar_22;
  tmpvar_22 = ((fract(
    (((_Time.yyyy * tmpvar_20) + tmpvar_21) * vec4(1.975, 0.793, 0.375, 0.193))
  ) * 2.0) - 1.0);
  vWaves_16 = tmpvar_22;
  highp vec4 x_23;
  x_23 = vWaves_16;
  highp vec4 tmpvar_24;
  tmpvar_24 = abs(((
    fract((x_23 + 0.5))
   * 2.0) - 1.0));
  highp vec4 tmpvar_25;
  tmpvar_25 = ((tmpvar_24 * tmpvar_24) * (3.0 - (2.0 * tmpvar_24)));
  vWaves_16 = tmpvar_25;
  mediump vec2 tmpvar_26;
  tmpvar_26 = (vWaves_16.xz + vWaves_16.yw);
  bend_15.xz = ((_WindEdgeFlutter * 0.1) * normal_14).xz;
  bend_15.y = (bendingFact_5 * 0.3);
  pos_13.xyz = (pos_13.xyz + ((
    (tmpvar_26.xyx * bend_15)
   + 
    ((wind_6.xyz * tmpvar_26.y) * bendingFact_5)
  ) * wind_6.w));
  pos_13.xyz = (pos_13.xyz + (bendingFact_5 * wind_6.xyz));
  tmpvar_12 = pos_13;
  mdlPos_4 = tmpvar_12;
  highp vec2 tmpvar_27;
  tmpvar_27 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_7 = tmpvar_27;
  gl_Position = (glstate_matrix_mvp * mdlPos_4);
  xlv_TEXCOORD0 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3.xyz;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2.x = (xlv_TEXCOORD0.x * 2.0);
  tmpvar_2.y = xlv_TEXCOORD0.y;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, tmpvar_2);
  c_1.xyz = (tmpvar_3.xyz * _Color.xyz);
  c_1.w = tmpvar_3.w;
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 _Wind;
uniform mediump vec4 _MainTex_ST;
uniform mediump float _WindEdgeFlutter;
uniform mediump float _WindEdgeFlutterFreqScale;
out mediump vec2 xlv_TEXCOORD0;
out lowp vec3 xlv_TEXCOORD2;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1 = _glesVertex;
  highp vec3 tmpvar_2;
  tmpvar_2 = normalize(_glesNormal);
  lowp vec4 tmpvar_3;
  tmpvar_3 = _glesColor;
  mediump vec4 mdlPos_4;
  mediump float bendingFact_5;
  mediump vec4 wind_6;
  mediump vec2 tmpvar_7;
  lowp float tmpvar_8;
  tmpvar_8 = tmpvar_3.w;
  bendingFact_5 = tmpvar_8;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _World2Object[0].xyz;
  tmpvar_9[1] = _World2Object[1].xyz;
  tmpvar_9[2] = _World2Object[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (tmpvar_9 * _Wind.xyz);
  wind_6.xyz = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = (_Wind.w * bendingFact_5);
  wind_6.w = tmpvar_11;
  highp vec4 tmpvar_12;
  mediump vec4 pos_13;
  pos_13 = tmpvar_1;
  mediump vec3 normal_14;
  normal_14 = tmpvar_2;
  mediump vec3 bend_15;
  mediump vec4 vWaves_16;
  highp vec4 v_17;
  v_17.x = _Object2World[0].w;
  v_17.y = _Object2World[1].w;
  v_17.z = _Object2World[2].w;
  v_17.w = _Object2World[3].w;
  mediump float tmpvar_18;
  tmpvar_18 = dot (v_17.xyz, vec3(1.0, 1.0, 1.0));
  mediump float tmpvar_19;
  tmpvar_19 = dot (pos_13.xyz, vec3((_WindEdgeFlutter + tmpvar_18)));
  mediump vec4 tmpvar_20;
  tmpvar_20.zw = vec2(1.0, 1.0);
  tmpvar_20.x = _WindEdgeFlutterFreqScale;
  tmpvar_20.y = _WindEdgeFlutterFreqScale;
  mediump vec4 tmpvar_21;
  tmpvar_21.x = tmpvar_19;
  tmpvar_21.y = tmpvar_19;
  tmpvar_21.z = tmpvar_18;
  tmpvar_21.w = tmpvar_18;
  highp vec4 tmpvar_22;
  tmpvar_22 = ((fract(
    (((_Time.yyyy * tmpvar_20) + tmpvar_21) * vec4(1.975, 0.793, 0.375, 0.193))
  ) * 2.0) - 1.0);
  vWaves_16 = tmpvar_22;
  highp vec4 x_23;
  x_23 = vWaves_16;
  highp vec4 tmpvar_24;
  tmpvar_24 = abs(((
    fract((x_23 + 0.5))
   * 2.0) - 1.0));
  highp vec4 tmpvar_25;
  tmpvar_25 = ((tmpvar_24 * tmpvar_24) * (3.0 - (2.0 * tmpvar_24)));
  vWaves_16 = tmpvar_25;
  mediump vec2 tmpvar_26;
  tmpvar_26 = (vWaves_16.xz + vWaves_16.yw);
  bend_15.xz = ((_WindEdgeFlutter * 0.1) * normal_14).xz;
  bend_15.y = (bendingFact_5 * 0.3);
  pos_13.xyz = (pos_13.xyz + ((
    (tmpvar_26.xyx * bend_15)
   + 
    ((wind_6.xyz * tmpvar_26.y) * bendingFact_5)
  ) * wind_6.w));
  pos_13.xyz = (pos_13.xyz + (bendingFact_5 * wind_6.xyz));
  tmpvar_12 = pos_13;
  mdlPos_4 = tmpvar_12;
  highp vec2 tmpvar_27;
  tmpvar_27 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_7 = tmpvar_27;
  gl_Position = (glstate_matrix_mvp * mdlPos_4);
  xlv_TEXCOORD0 = tmpvar_7;
  xlv_TEXCOORD2 = tmpvar_3.xyz;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2.x = (xlv_TEXCOORD0.x * 2.0);
  tmpvar_2.y = xlv_TEXCOORD0.y;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, tmpvar_2);
  c_1.xyz = (tmpvar_3.xyz * _Color.xyz);
  c_1.w = tmpvar_3.w;
  _glesFragData[0] = c_1;
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