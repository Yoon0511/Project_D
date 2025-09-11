Shader "Custom/RimLight"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimPower("RimPower" , Range(0,10)) = 0 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        fixed4 _Color;
        float _RimPower;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
 
            float rim  = saturate(dot(o.Normal , IN.viewDir));
            rim = pow ( 1-rim , _RimPower);
            //o.Emission = c.rgb + rim * _Color.rgb;
 
            //알베도값에 Rim을 곱해서 어두워지게 만듬
            o.Albedo = lerp(o.Albedo, _Color.rgb, rim);

            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
