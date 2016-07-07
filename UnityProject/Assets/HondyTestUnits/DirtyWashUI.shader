Shader "Custom/DirtyWashUI"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
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
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

			Cull Off
			Lighting Off
			ZWrite Off
			Fog{ Mode Off }
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
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord1 : TEXCOORD0;
				float2 texcoord2 : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord1  : TEXCOORD0;
				half2 texcoord2  : TEXCOORD1;
				float4 worldPosition : TEXCOORD2;
			};
			
			fixed4 _Color;
			half _MaskAlpha;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;

			v2f vert(appdata_t v)
			{
				v2f OUT;

				OUT.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				OUT.texcoord1 = v.texcoord1;
				OUT.texcoord2 = v.texcoord2;
				OUT.worldPosition = v.vertex;

				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
				OUT.color = v.color * _Color;
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _MaskTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 base = (tex2D(_MainTex, IN.texcoord1) + _TextureSampleAdd) * IN.color;

				half alphaRef = (tex2D(_MaskTex, IN.texcoord2).r - (1 - _MaskAlpha * 5)) * (base.w);
				clip(alphaRef - 1.0f);
				half4 color = (base * (_Color));

				clip(alphaRef - 1);

				return color;
			}
		ENDCG
		}
	}
}
