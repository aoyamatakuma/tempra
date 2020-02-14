Shader "Unlit/form"
{
	SubShader{
		Tags { "Queue" = "Transparent" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade
		#pragma target 3.0

		struct Input {
			float3 worldNormal;
				float3 viewDir;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = fixed4(0, 0, 0, 1);
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
				o.Alpha = alpha*0.5f;
		}
		ENDCG
	}
		FallBack "Diffuse"
}