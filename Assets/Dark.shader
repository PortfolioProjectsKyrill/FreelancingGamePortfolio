Shader "Custom/Dark"
{
         Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.8
        _Metallic ("Metallic", Range(0,1)) = 0.6
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float4 _Color;
            float _Glossiness;
            float _Metallic;
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Set base color to black
                fixed4 baseColor = float4(0, 0, 0, 1);
                
                // Sample texture and multiply by color property
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                
                // Combine base color and texture color
                fixed4 finalColor = lerp(baseColor, texColor, texColor.a);
                
                // Set smoothness and metallic values from properties
                finalColor.a = _Glossiness;
                finalColor.rgba += _Metallic;
                
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
