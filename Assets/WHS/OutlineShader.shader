
// �׵θ��� ���� �������� ���͸����� ���� ���͸��� �纻�� ���� ���̴� ���� Custom -> OutLine ����
// (�׳� �����ϸ� ���� ���͸����� ���� �ٸ� ������Ʈ�� ���� ����ǹ��� �纻���� ���� �����ؾ���)
// �׵θ��� ���� �����յ��� ������� Outline �纻 ���͸��� ���


Shader "Custom/OutLine"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {} // �˺��� �ؽ����� �⺻�� ���
        _OutLineColor("OutLine Color", Color) = (0,0,0,0) // �׵θ��� ���� ����
        _OutLineWidth("OutLine Width", Range(0.001, 0.05)) = 0.01 // �׵θ��� �β� ����
        _BlinkSpeed("Blink Speed", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"} // ���̴��� ����ġ�� �����ϰ�  // 
        LOD 200 // ���̴��� ���⵵ LOD ���� ( 200 �߰� ���� )

        // �׵θ� �н�
        cull front // �ո��� ������ ������Ʈ�� �ܰ����� ǥ��
        zwrite off // ���� ���۸� ���� �ʾ� �ٸ� ������Ʈ�� ��ġ�� �� ���� �׷�������

        CGPROGRAM
        #pragma surface surf NoLight vertex:vert noshadow noambient // �׵θ��� ����, �׸��� ���� ����

        float4 _OutLineColor; // �׵θ� ����
        float _OutLineWidth; // �׵θ� �β�
        float _BlinkSpeed; // ������ �ӵ�

        void vert(inout appdata_full v) {
            float time = _Time.y;
            // 0���� _OutLineWidth���� �ð������� sin������ �β��� ��ȭ��Ŵ
            float width = lerp(0.0, _OutLineWidth, abs(sin(time * _BlinkSpeed))); // 0���� _OutLineWidth���� �β� ��ȭ
            v.vertex.xyz += v.normal.xyz * width; // ���� �������� �׵θ� �β���ŭ �̵����� �׵θ� ǥ��
        }

        struct Input
        {
            float4 color; 
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            
        }

        float4 LightingNoLight(SurfaceOutput s, float3 lightDir, float atten) {
            return float4(_OutLineColor.rgb, 1); // �׵θ� ���� ��ȯ
        }
        ENDCG

        // ���� �н�
        cull back // �׵θ��� �ƴ� �ٱ��� �������� ���� �޸� ����
        zwrite on // ������ ���� ���۸� �� ���ΰ� �ùٸ��� ���̰�
        CGPROGRAM
        #pragma surface surf Lambert
        #pragma target 3.0 // 

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse" // ���̴��� �������� ������ �⺻ Diffuse ���̴��� ���
}