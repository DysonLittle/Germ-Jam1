Shader "Custom/WorldSpaceTexture" {
    Properties{
        _MainTex("Albedo", 2D) = "white" {}
        _Metallic("Metallic", 2D) = "white" {}
        _MetallicScale("Metallic Scale", Range(0, 1)) = 0
        //_Normal("Normal Map", 2D) = "white" {}
        _Smoothness("Smoothness", 2D) = "white" {}
        _SmoothnessScale("Smoothness Scale", Range(0, 1)) = 0
        _Occlusion("Occlusion Map", 2D) = "white" {}
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200
            
            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0
                  
            sampler2D _MainTex;
            sampler2D _Metallic;
            float _MetallicScale;
            //sampler2D _Normal;
            sampler2D _Smoothness;
            float _SmoothnessScale;
            sampler2D _Occlusion;
            uniform float4 _MainTex_ST;

            struct Input {
                float3 worldNormal;
                float3 worldPos;
            };

            float4 axisAngleQuat(float3 axis, float angle)
            {
                float4 quat;
                float sinAngle = sin(angle * 0.5f);

                quat.w = cos(angle * 0.5f);
                quat.x = axis.x * sinAngle;
                quat.y = axis.y * sinAngle;
                quat.z = axis.z * sinAngle;
                return quat;
            }

            bool isCloseTo(float3 firstVec, float3 secondVec)
            {
                return length(firstVec - secondVec) < 0.00001f;
            }

            float4 get2VecQuat(float3 vecFrom)
            {
                float3 vecCross = cross(vecFrom, float3(0, 0, -1));
                bool zeroCheck = all(vecCross == float3(0.0f, 0.0f, 0.0f)) || isCloseTo(vecFrom, float3(0.0f, 0.0f, -1.0f));

                return zeroCheck ? float4(0, 0, 0, 1) : axisAngleQuat(normalize(vecCross), acos(dot(vecFrom, float3(0, 0, -1))));
            }

            float3 rotateByQuat(float4 quat, float3 vec)
            {
                return 2.0f * dot(quat.xyz, vec) * quat.xyz
                    + (quat.w * quat.w - dot(quat.xyz, quat.xyz)) * vec
                    + 2.0f * quat.w * cross(quat.xyz, vec);
            }

            void surf(Input IN, inout SurfaceOutputStandard o) {
                
                float3 norm = IN.worldNormal;
                float4 transformQuat = get2VecQuat(normalize(norm));
                float3 transformedVec = rotateByQuat(transformQuat, IN.worldPos);

                float4 sampleColor = tex2D(_MainTex, transformedVec.xy * _MainTex_ST);

                o.Albedo = sampleColor;
                o.Metallic = tex2D(_Metallic, transformedVec.xy * _MainTex_ST) * _MetallicScale;
                WorldNormalVector(IN, o.Normal);
                o.Smoothness = (float4(1, 1, 1, 1) - tex2D(_Smoothness, transformedVec.xy * _MainTex_ST)) * _SmoothnessScale;
                o.Occlusion = tex2D(_Occlusion, transformedVec.xy * _MainTex_ST);
            }

            ENDCG
    }
        FallBack "Diffuse"
}