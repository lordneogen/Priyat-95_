Shader "Hidden/PaletteSwapper" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorPalette("Color Palette", 2D) = "white" {}
    }

    SubShader {
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
            sampler2D _ColorPalette;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 srcColor = tex2D(_MainTex, i.uv);
                
                // Calculate the index of the closest color in the palette
                float2 paletteUV = float2(srcColor.r, 0.5); // Assuming the palette is a horizontal strip
                fixed4 paletteColor = tex2D(_ColorPalette, paletteUV);

                return paletteColor;
            }
            ENDCG
        }
    }
}
