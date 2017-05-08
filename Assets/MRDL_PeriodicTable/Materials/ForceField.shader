Shader "HUX/ForceField"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Rim("Rim", Range(0,5)) = 1.5
		_ScrollSpeed("Scroll Speed", Range(0, 5)) = 2
		_Tiling("Tiling", Range(0.1, 10)) = 1
		_TexStrength("Texture Strength", Range(0.1, 1)) = 0.5
		_NearPlaneFadeDistance("Near Fade Distance", Range(0, 1)) = 0.1
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "RenderQueue" = "Transparent"}
			Cull Back
			ZTest Always
			ZWrite Off
			Blend One One
			LOD 300

				CGPROGRAM
				#pragma surface surf BlinnPhong vertex:vert

				fixed4 _Color;
				sampler2D _MainTex;
				float _Rim;
				float _ScrollSpeed;
				float _Tiling;
				float _TexStrength;
				float4 _NearPlaneFadeDistance;

				inline float ComputeNearPlaneFadeLinear(float4 vertex)
				{
					float distToCamera = -(mul(UNITY_MATRIX_MV, vertex).z);
					return saturate(mad(distToCamera, _NearPlaneFadeDistance.y, _NearPlaneFadeDistance.x));
				}

				struct Input {
					half2 uv_MainTex;
					half4 screenPos;
					half3 viewDir;
					float fade;
				};

				void surf(Input IN, inout SurfaceOutput o) {
					fixed stepFresnel = step(fixed(1.0) - dot(normalize(IN.viewDir), fixed3(0, 0, 1)), fixed(1.0));
					fixed rim = pow(fixed(1.0) - dot(normalize(IN.viewDir), fixed3(0, 0, 1)), _Rim) * 5;

					half2 texCoords = half2(IN.uv_MainTex.x + half (_Time.x * _ScrollSpeed), IN.uv_MainTex.y + half (_Time.y * _ScrollSpeed)) * _Tiling;
					fixed tex = tex2D(_MainTex, texCoords).x * _TexStrength;

					o.Albedo = fixed3(0, 0, 0);
					o.Alpha = 1;
					o.Emission = _Color.rgb * tex * rim * _Color.a;// *IN.fade;
					o.Normal = fixed3(0, 0, 1);
				}

				void vert(inout appdata_full v, out Input o)
				{
					UNITY_INITIALIZE_OUTPUT(Input, o);
					o.fade = ComputeNearPlaneFadeLinear(v.vertex);
				}
				ENDCG
		}
}
