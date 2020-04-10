Shader "Custom/testSurf" {
    Properties {
          _Color ("Color", Color) = (1,1,1,1)
        }
    SubShader {
      Tags { "RenderType" = "Opaque" }

      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float4 color : COLOR;
      };
      void surf (Input IN, inout SurfaceOutput o) {
      }
      ENDCG

      ZTest Greater

      CGPROGRAM

      #pragma target 3.0
      #pragma surface surf Lambert //lambertian reflectance lighting version
      //#pragma surface surf NoLighting //no lighting version

      struct Input {
        float4 color : COLOR;
      };

      float4 _Color;

      void surf (Input IN, inout SurfaceOutput o) {
         o.Albedo = _Color;
      }

      ENDCG

    }
    Fallback "Diffuse"
  }
