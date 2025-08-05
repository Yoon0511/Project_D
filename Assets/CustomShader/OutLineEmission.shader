Shader "Custom/OutLineEmission"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Main Tex Color", Color) = (1,1,1,1)
 
        _OutLine_Bold("Outline Bold", Range(0, 1)) = 0.1
        _OutLine_Color("Outline Color", Color) = (0,0,0,1)

        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
 
        Cull front    
        Pass
        {
            CGPROGRAM
            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFuc
            #include "UnityCG.cginc"
 
                struct ST_VertexInput    //! 버텍스 쉐이더 Input
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };
 
                struct ST_VertexOutput    //! 버텍스 쉐이더 Output
                {
                    float4 vertex : SV_POSITION;
                };
 
                float _OutLine_Bold;
                fixed4 _OutLine_Color;
 
                ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
                {
                    ST_VertexOutput stOutput;
 
                    float3 fNormalized_Normal = normalize(stInput.normal);        //! 로컬 노말 벡터를 정규화 시킴
                    float3 fOutline_Position = stInput.vertex + fNormalized_Normal * (_OutLine_Bold * 0.1f); //! 버텍스 좌표에 노말 방향으로 더한다.
 
                    stOutput.vertex = UnityObjectToClipPos(fOutline_Position);    //! 노말 방향으로 더해진 버텍스 좌표를 카메라 클립 공간으로 변환 
                    return stOutput;
                }
 
 
                float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
                {
                    return _OutLine_Color;
                }
 
            ENDCG
        }

        Pass
        {
            Cull back
            CGPROGRAM

            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFuc

            #include "UnityCG.cginc"

            struct ST_VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct ST_VertexOutput
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _EmissionColor;

            ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
            {
                ST_VertexOutput stOutput;
                stOutput.vertex = UnityObjectToClipPos(stInput.vertex);
                stOutput.uv = stInput.uv;
                return stOutput;
            }

            float4 _FragmentFuc(ST_VertexOutput i) : SV_Target
            {
                float4 mainTexColor = tex2D(_MainTex, i.uv);
                mainTexColor.rgb += _EmissionColor.rgb;
                return mainTexColor;
            }
            ENDCG
        }
    }
}
