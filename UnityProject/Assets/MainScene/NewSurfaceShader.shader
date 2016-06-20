﻿Shader "Projector/Light" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_ShadowTex("Cookie", 2D) = "" {}
	_FalloffTex("FallOff", 2D) = "" {}
	}

		Subshader{
		Tags{ "Queue" = "Transparent" }
		Pass{
		ZWrite Off
		ColorMask RGB
		Blend SrcAlpha OneMinusSrcAlpha	 
		Offset 0, -1

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile_fog
#include "UnityCG.cginc"

		struct v2f {
		float4 uvShadow : TEXCOORD0;
		UNITY_FOG_COORDS(2)
			float4 pos : SV_POSITION;
	};

	float4x4 _Projector;
	float4x4 _ProjectorClip;

	v2f vert(float4 vertex : POSITION)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, vertex);
		o.uvShadow = mul(_Projector, vertex);
		UNITY_TRANSFER_FOG(o,o.pos);
		return o;
	}

	fixed4 _Color;
	sampler2D _ShadowTex;

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
	texS.rgb *= _Color.rgb;

	fixed4 res = texS;

	UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
	return res;
	}
		ENDCG
	}
	}
}
