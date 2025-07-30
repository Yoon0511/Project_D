Shader "Unlit/CustomCarToonShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Main Tex Color",Color) = (1,1,1,1)
        _Outline_Bold("Outline Bold", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        cull front
        Pass
        {
            CGPROGRAM
            #pragma vertex _VertexFuc
            #pragma fragment _FragmentFunc
            #include "UnityCG.cginc"

            struct ST_VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct ST_VertexOutput
            {
                float4 vertex : SV_POSITION;
            };

            float _Outline_Bold;

            ST_VertexOutput _VertexFuc(ST_VertexInput stInput)
            {
                ST_VertexOutput stOutput;

                float3 fNomalized_Normal = normalize(stInput.normal);
                float3 fOutline_Position = stInput.vertex + fNomalized_Normal + (_Outline_Bold + 0.1f);
                
                stOutput.vertex = UnityObjectToClipPos(fOutline_Position);
                return stOutput;
            }

            float4 _FragmentFunc(ST_VertexOutput i) : SV_Target
            {
                return 0.0f;
            }

            ENDCG
        }
    }
}

