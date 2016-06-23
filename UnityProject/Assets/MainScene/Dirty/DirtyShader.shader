Shader "Custom/Dirty" {
	Properties{
		_MainTex("Base", 2D) = "white" {}
		_MaskTex("Mask",2D) = "white"{}
		_MaskAlpha("MaskAlpha", Range(-0.01,1)) = 0.5
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Pass{

				CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _MaskTex;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv1 : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};

			half _MaskAlpha;
			fixed4 _Color;

			float4 _MainTex_ST;
			float4 _MaskTex_ST;
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv1 = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.texcoord, _MaskTex);
				return o;
			}

			half4 frag(v2f i) : COLOR
			{
				half4 base = tex2D(_MainTex, i.uv1);
				half alphaRef = ( (1- _MaskAlpha )) * (base.w );			
				clip(alphaRef -0.1 );
				half4 color = (base * (_Color ));
				if (color.w < 0.1)
				{
					color.w = 0;
				}
				return color;
			}
			ENDCG
		}
		}
			FallBack Off
}