Shader "Custom/TetherShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FrameCountX ("Frame Count X", Float) = 5 // Number of columns in the sprite sheet
        _FrameCountY ("Frame Count Y", Float) = 5 // Number of rows in the sprite sheet
        _AnimationSpeed ("Animation Speed", Float) = 1 // Speed of the animation
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
            };

            sampler2D _MainTex;
            float _FrameCountX;
            float _FrameCountY;
            float _AnimationSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate total frame count
                float totalFrames = _FrameCountX * _FrameCountY;

                // Calculate which frame we are on based on time and speed
                float frameIndex = floor(_Time.y * _AnimationSpeed) % totalFrames;

                // Get the frame's X and Y coordinates on the sprite sheet
                float frameX = floor(frameIndex % _FrameCountX);
                float frameY = floor(frameIndex / _FrameCountX);

                // Normalize the UVs by dividing by the number of frames
                float2 uv = i.uv;

                // Adjust UVs to display the correct frame
                uv.x = (uv.x / _FrameCountX) + (frameX / _FrameCountX);
                uv.y = (uv.y / _FrameCountY) + (frameY / _FrameCountY);

                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
