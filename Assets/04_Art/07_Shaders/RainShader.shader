Shader "Custom/ChromaKeySimplified"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _KeyColor ("Key Color", Color) = (0, 1, 0, 1) // Grün als Key Color
        _Threshold ("Threshold", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "Queue"="Overlay" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _KeyColor;
            float _Threshold;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                half3 keyColor = _KeyColor.rgb;
                half diff = distance(col.rgb, keyColor);
                if (diff < _Threshold)
                    col.a = 0; // Transparenz für den grünen Bereich
                return col;
            }
            ENDCG
        }
    }
}
