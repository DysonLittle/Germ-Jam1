Shader "Custom/WorldSpaceTexture" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
    }

        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 200
            
            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows
            #pragma target 3.0
                  
            sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct Input {
                float3 worldNormal;
                float3 worldPos;
            };

            /*float3 project(float3 vec, float3 onto)
            {
                float ontoLength = length(onto);
                return onto * (dot(vec, onto) / (ontoLength * ontoLength));
            }

            float3 projectOntoPlane(float3 vec, float3 ontoNormal)
            {
                return vec - project(vec, ontoNormal);
            }*/

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

            float4 get2VecQuat(float3 vecFrom, float3 vecTo)
            {
                float3 vecCross = cross(vecFrom, vecTo);
                if (all(vecCross == float3(0, 0, 0)))
                {
                    return float4(0, 0, 0, 1);
                }
                return axisAngleQuat(normalize(vecCross), acos(dot(vecFrom, vecTo)));
            }

            float3 rotateByQuat(float4 quat, float3 vec)
            {
                return 2.0f * dot(quat.xyz, vec) * quat.xyz
                    + (quat.w * quat.w - dot(quat.xyz, quat.xyz)) * vec
                    + 2.0f * quat.w * cross(quat.xyz, vec);
            }

            void surf(Input IN, inout SurfaceOutputStandard o) {

                float4 transformQuat = get2VecQuat(IN.worldNormal, float3(0, 0, -1));
                float3 transformedVec = rotateByQuat(transformQuat, IN.worldPos);

                float4 sampleColor = tex2D(_MainTex, transformedVec.xy * _MainTex_ST);

                o.Albedo = sampleColor;
            }

            ENDCG
    }
        FallBack "Diffuse"
}