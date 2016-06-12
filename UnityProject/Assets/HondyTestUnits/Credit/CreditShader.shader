Shader "Custom/CreditShader" {
	Properties{
		_MainTex("Base", 2D) = "white" {}
		_MaskTex("Mask",2D) = "white"{}
		_MaskAlpha("MaskAlpha", Range(0,1)) = 0.5
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Pass{
			Blend SrcAlpha OneMinusSrcAlpha

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
				half4 base = tex2D(_MainTex, i.uv1)*0.5;
				half4 mask = tex2D(_MaskTex, i.uv2);
				base.w = -mask.a + _MaskAlpha *2;
				half4 color = saturate(base * (_Color * 2.0f));
				return color;
			}
			ENDCG
		}
		}
			FallBack Off
}