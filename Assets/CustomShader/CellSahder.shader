Shader "Unlit/CellShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Main Tex Color", Color) = (1,1,1,1)
 
        _OutLine_Bold("Outline Bold", Range(0, 1)) = 0.1
        _OutLine_Color("Outline Color", Color) = (0,0,0,1)

        _Brightness("Brightness", Range(0, 10)) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
 
        Cull front    //! 1Pass�� �ո��� �׸��� �ʴ´�.
        Pass
        {
            CGPROGRAM
            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFuc
            #include "UnityCG.cginc"
 
                struct ST_VertexInput    //! ���ؽ� ���̴� Input
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };
 
                struct ST_VertexOutput    //! ���ؽ� ���̴� Output
                {
                    float4 vertex : SV_POSITION;
                };
 
                float _OutLine_Bold;
                fixed4 _OutLine_Color;
 
                ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
                {
                    ST_VertexOutput stOutput;
 
                    float3 fNormalized_Normal = normalize(stInput.normal);        //! ���� �븻 ���͸� ����ȭ ��Ŵ
                    float3 fOutline_Position = stInput.vertex + fNormalized_Normal * (_OutLine_Bold * 0.1f); //! ���ؽ� ��ǥ�� �븻 �������� ���Ѵ�.
 
                    stOutput.vertex = UnityObjectToClipPos(fOutline_Position);    //! �븻 �������� ������ ���ؽ� ��ǥ�� ī�޶� Ŭ�� �������� ��ȯ 
                    return stOutput;
                }
 
 
                float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
                {
                    return _OutLine_Color;
                }
 
            ENDCG
        }
 
        Cull back    //! 2Pass�� �޸��� �׸��� �ʴ´�.
        ZWrite On
        Blend Off
        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 worldPos;
        };

        float _Brightness;

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
            float3 normal = normalize(IN.worldNormal);

            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

            float ndotl = dot(normal,lightDir);

            float shade;
            if(ndotl > 0.6) shade = 1.0;
            else if(ndotl > 0.2) shade = 0.6;
            else shade = 0.3;

            o.Albedo = tex.rgb * _Color.rgb * shade * _Brightness;
            o.Alpha = 1.0; // ������ ó��
        }
        ENDCG
    }
}
