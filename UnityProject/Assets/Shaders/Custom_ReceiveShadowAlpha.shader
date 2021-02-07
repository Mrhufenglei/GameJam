// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Receive Shadow Alpha" 
{
    Properties
    {
        _Color("Color", COLOR) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white" {}
    }

    Subshader 
    {
        //Tags
        //{ 
        //    "RenderType" = "Transparent" 
        //    "Queue" = "Geometry"
        //    //"PerformanceChecks" = "False" 
        //}

        Pass 
        {
            //Name "Diffuse"
            Tags{ 
			"LightMode" = "ForwardBase"
			"RenderType" = "AlphaTest" 
            "Queue" = "Geometry" }
            Lighting Off
			 //目标Alpha通道混合
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
				//#ifndef SHADOWS_SCREEN
				//#define SHADOWS_SCREEN
				//#endif
				//#ifndef UNITY_NO_SCREENSPACE_SHADOWS
				//#define UNITY_NO_SCREENSPACE_SHADOWS
				//#endif
                #pragma multi_compile_fwdbase
                #pragma vertex Vert
                #pragma fragment Frag
                #include "UnityCG.cginc"
				#include "Lighting.cginc"
                #include "AutoLight.cginc"

                uniform half4 _Color;
                uniform sampler2D _MainTex;
                uniform half4 _MainTex_ST;//必须声明

                struct V2f {
                    half4 pos : SV_POSITION;
                    half2 uv : TEXCOORD0;
					float4 vertex:COLOR;
                    SHADOW_COORDS(1)
                };

                V2f Vert(appdata_base v)
                {
                    V2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					o.vertex=v.vertex;
                    TRANSFER_SHADOW(o);

                    return o;
                }

                half4 Frag(V2f i) : SV_Target
                {
                    half4 tex = tex2D(_MainTex, i.uv) * _Color;
					float atten = SHADOW_ATTENUATION(i);
					//if (atten != 1.0)
					//{
					//	atten = 0.83;
					//}
                    return tex * atten;
                }
            ENDCG
        }
    }
	FallBack "Diffuse"
}