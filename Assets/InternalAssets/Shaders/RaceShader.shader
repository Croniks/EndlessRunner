Shader "Custom/RaceShader"
{
    Properties
    {
        _MaskTex ("Mask", 2D) = "white" {}
        _FirstColor("FirstColor", Color) = (1, 1, 1, 1)
        _SecondColor("SecondColor", Color) = (1, 1, 1, 1)
        _ScrollSpeeds ("Scroll Speeds", vector) = (0, 0, 0, 0)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MaskTex;

        struct Input
        {
            float2 uv_MaskTex;
        };

        fixed4 _FirstColor;
        fixed4 _SecondColor;
        float4 _ScrollSpeeds;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            IN.uv_MaskTex += _ScrollSpeeds * _Time.x;

            fixed3 color;

            color = tex2D(_MaskTex, IN.uv_MaskTex);

            if(color.g < 0.2)
            {
                color.g = 0;
            }

            color = color.r * _SecondColor + color.g * _FirstColor;

            o.Albedo = color;
            
            //IN.uv_MaskTex = _Time.x * _Offset;
        }
        ENDCG
    }
    FallBack "Diffuse"
}