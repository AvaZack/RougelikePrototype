Shader "Custom/SectorEffect"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,0,0,0)
        _Radius ("Radius", float) = 5
        _Angle ("Angle", float) = 60
        _Direction ("Direction", Vector) = (1,0,0,0)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            float4 _MainColor;
            float _Radius;
            float _Angle;
            float2 _Direction;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算位置相对中心的方向
                float2 center = unity_ObjectToWorld._m03_m13;
                float2 dir = normalize(i.worldPos.xy - center);
                
                // 计算与中轴的角度差
                float angle = degrees(acos(dot(dir, normalize(_Direction))));
                float distance = length(i.worldPos.xy - center);

                // 判断是否在扇形内
                if(angle <= _Angle/2 && distance <= _Radius)
                {
                    return _MainColor;
                }
                else
                {
                    discard;
                    return float4(0,0,0,0);
                }
            }
            ENDCG
        }
    }
}