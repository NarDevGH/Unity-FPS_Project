Shader "Custom/SniperOpticGlass_Shader"
{
	Properties
	{
	_MainTex("Base (RGB)", 2D) = "white" {}
	_Mask("Culling Mask", 2D) = "white" {}
	}

	SubShader
	{
		Tags {"Queue" = "Transparent"}
		Lighting Off
		ZWrite Off
		ZTest off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			SetTexture[_Mask] {combine texture}
			SetTexture[_MainTex] {combine texture, previous}
		}
	}
}