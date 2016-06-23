Shader "Custom/Dirty" {
	Properties{
		_Color("_Color", Color) = (1,1,1,1)
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }

		Lighting Off
		LOD 100

		CGPROGRAM
#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed4 _Color;
		float _Cutoff;
		struct Input {
			float2 uv_MainTex;
		};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) ;

			o.Albedo = _Color * c.rgb;
			clip(_Cutoff * c.a-0.1);
	}
	ENDCG
	}

		Fallback "VertexLit"
}