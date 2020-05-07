Shader "Unit/Dissolve"
{
	Properties{
		_Color("Tint", Color) = (0, 0, 0, 1)
		_MainTex("Texture", 2D) = "white" {}

		[Header(Dissolve)]
		_DissolveTex("Dissolve Texture", 2D) = "black" {}
		_DissolveAmount("Dissolve Amount", Range(0, 1)) = 0.5

	}
		SubShader{
			Tags{ "RenderType" = "Opaque" "Queue" = "Geometry"}

			CGPROGRAM

			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MainTex;
			fixed4 _Color;
			sampler2D _DissolveTex;
			float _DissolveAmount;


			struct Input {
				float2 uv_MainTex;
				float2 uv_DissolveTex;
			};

			void surf(Input i, inout SurfaceOutputStandard o) {

				float dissolve = tex2D(_DissolveTex, i.uv_DissolveTex).r;
				dissolve = dissolve * 0.999;
				float isVisible = dissolve - _DissolveAmount;
				clip(isVisible);


				fixed4 col = tex2D(_MainTex, i.uv_MainTex);
				col *= _Color;

				o.Albedo = col;
			}
			ENDCG
		}
			FallBack "Standard"
}