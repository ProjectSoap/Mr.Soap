Shader "Custom/NewSurfaceShader 1" {
	Properties{
		_MainTex("Base", 2D) = "white" {}
		_SubTex("Frame", 2D) = "white" {}
	_MaskTex("Mask", 2D) = "white" {}
	_Color("Color", Color) = (0.5, 0.5, 0.5, 0.5)
	}
		SubShader{
		Pass{
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

	sampler2D _MainTex;
	sampler2D _SubTex;
	sampler2D _MaskTex;
	float4 _Color;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv1 : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
		float2 uv3 : TEXCOORD2;
	};

	float4 _MainTex_ST;
	float4 _SubTex_ST;
	float4 _MaskTex_ST;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv1 = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.uv2 = TRANSFORM_TEX(v.texcoord, _SubTex);
		o.uv3 = TRANSFORM_TEX(v.texcoord, _MaskTex);
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		half4 base = tex2D(_MainTex, i.uv1)*0.5;
		half4 frame = tex2D(_SubTex, i.uv1)*0.5;
		half4 mask = tex2D(_MaskTex, i.uv2);
		base.w = mask.w;
		half4 color = saturate(frame + base * (_Color * 2.0f));
		return color;
	}
		ENDCG
	}
	}
		FallBack Off
}