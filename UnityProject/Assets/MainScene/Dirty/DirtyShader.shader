Shader "Custom/Dirty" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_NormalTex("Normal map", 2D) = "white" {}
	}
		SubShader{
		Tags{
		"RenderType" = "Opaque"
		"LightMode" = "ForwardBase"
	}
		LOD 200

		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag

		struct vertInput {
		float4 vertex   : SV_POSITION;
		float4 normal   : NORMAL;
		float2 texcoord : TEXCOORD0;
	};

	struct vert2frag {
		float4 position : POSITION;
		float2 uv       : TEXCOORD0;
		float4 color    : COLOR0;
		float3 lightDirection : COLOR1;
	};

	uniform sampler2D _MainTex;
	uniform sampler2D _NormalTex;
	uniform fixed4 _LightColor0;

	vert2frag vert(vertInput v) {
		vert2frag o;

		float4 wpos = mul(UNITY_MATRIX_MVP, v.vertex);
		float4 wnormal = normalize(mul(v.normal, _World2Object));
		float  diffuse = max(0, dot(_WorldSpaceLightPos0, wnormal));

		float3 n = normalize(wnormal).xyz;
		float3 t = normalize(cross(n, float3(0, 1, 0)));
		float3 b = cross(n, t);

		o.position = wpos;
		o.color = _LightColor0 * diffuse;
		o.uv = v.texcoord;

		float4 light = _WorldSpaceLightPos0;
		o.lightDirection.x = dot(t, light);
		o.lightDirection.y = dot(b, light);
		o.lightDirection.z = dot(n, light);
		o.lightDirection = normalize(o.lightDirection);

		return o;
	}

	float4 frag(vert2frag i) : COLOR{
		float4 color = tex2D(_MainTex, i.uv);
		float3 mNormal = (tex2D(_NormalTex, i.uv) * 2.0 - 1.0).rgb;
		float3 light = normalize(i.lightDirection).xyz;
		float  diffuse = max(0, dot(mNormal, light));
		float4 last = i.color * (color * diffuse);
		return last;
	}

		ENDCG
	}
	}

		FallBack "Diffuse"
}