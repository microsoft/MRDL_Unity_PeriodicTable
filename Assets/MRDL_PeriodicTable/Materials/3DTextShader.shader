Shader "HoloToolkit/3DTextShader"{
	Properties{
		_MainTex("Font Texture", 2D) = "white" {}
		_Color("Text Color", Color) = (1,1,1,1)
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}

	SubShader{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 200
		Lighting Off Cull Off Fog{ Mode Off }

		Pass{
			Alphatest Greater[_Cutoff]
			Color[_Color]
			SetTexture[_MainTex]{
				combine primary, texture * primary
			}
		}
	}
}