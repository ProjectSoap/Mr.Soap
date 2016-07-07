
Shader "Custom/MiniMapShader" {
	Properties{
		 _MainTex("Base", 2D) = "white" {}
		 _MaskTex("Mask", 2D) = "white" {}
		 _SubTex("Sub", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15
	}
		SubShader
		{
			Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

			Stencil
		{
			Ref[_Stencil]
			Comp[_StencilComp]
			Pass[_StencilOp]
			ReadMask[_StencilReadMask]
			WriteMask[_StencilWriteMask]
		}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass
		{
			CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "UnityUI.cginc"

	sampler2D _MainTex;
	sampler2D _SubTex;
	sampler2D _MaskTex;
	float4 _Color;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv1 : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
		float2 uv3 : TEXCOORD2;
		float4 worldPosition : TEXCOORD3;
	};

	float4 _MainTex_ST;
	float4 _SubTex_ST;
	float4 _MaskTex_ST;
	float4 _ClipRect;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.worldPosition = v.vertex;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv1 = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.uv2 = TRANSFORM_TEX(v.texcoord, _SubTex);
		o.uv3 = TRANSFORM_TEX(v.texcoord, _MaskTex);
#ifdef UNITY_HALF_TEXEL_OFFSET
		o.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1, 1);
#endif
		return o;
	}




		fixed4 frag(v2f i) : SV_Target
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
}