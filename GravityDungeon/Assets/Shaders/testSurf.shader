Shader "Custom/testSurf" {
    Properties {
          _Color ("Color", Color) = (1,1,1,1)
        }
    SubShader {
      Tags { "RenderType" = "Opaque" }
        // Write the value 1 to the stencil buffer
        Stencil {
          Ref 0
          Comp Less
          Fail IncrSat
        }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float4 color : COLOR;
      };
      float4 _Color;
      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = _Color; // 1 = (1,1,1,1) = white
      }
      ENDCG
    }
    Fallback "Diffuse"
  }
