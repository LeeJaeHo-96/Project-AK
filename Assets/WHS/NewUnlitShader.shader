Shader "Custom/OutlineWithBaseTexture"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // �⺻ �ؽ�ó
        _OutLineColor ("Outline Color", Color) = (0,0,0,1) // �׵θ� ����
        _OutLineWidth ("Outline Width", Range(0.001, 0.05)) = 0.01 // �׵θ� �β�
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // �ƿ����� �н�
        Pass
        {
            Cull Front // �ո��� ������ ������Ʈ�� �ܰ����� ǥ��
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha // ������ ����� ����

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _OutLineWidth;
            fixed4 _OutLineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                // ���� �������� ������Ʈ�� Ȯ���Ͽ� �׵θ� ����
                v.vertex.xyz += v.normal * _OutLineWidth;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutLineColor; // �׵θ� ���� ����
            }
            ENDCG
        }

        // �⺻ �ؽ�ó �н�
        Pass
        {
            Cull Back // �޸��� �׸��� ���� Back Cull
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha // ������ ����� ����

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0; // UV ��ǥ
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv); // �ؽ�ó ���ø�
                return col; // ���� ���� ��ȯ
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
