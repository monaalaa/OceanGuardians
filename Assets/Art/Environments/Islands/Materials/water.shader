// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New AmplifyShader"
{
	Properties
	{
		_water("water", 2D) = "white" {}
		_water_2("water_2", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform sampler2D _water;
		uniform sampler2D _water_2;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float2 temp_cast_0 = (-0.01).xx;
			float2 uv_TexCoord10 = i.uv_texcoord * float2( 140,140 ) + float2( 0.2,0.5 );
			float2 panner8 = ( 1.0 * _Time.y * temp_cast_0 + uv_TexCoord10);
			float4 tex2DNode1 = tex2D( _water, panner8 );
			float2 temp_cast_1 = (0.03).xx;
			float2 uv_TexCoord7 = i.uv_texcoord * float2( 180,180 ) + float2( -0.5,0 );
			float2 panner5 = ( 1.0 * _Time.y * temp_cast_1 + uv_TexCoord7);
			float4 tex2DNode2 = tex2D( _water_2, panner5 );
			float4 lerpResult3 = lerp( ( float4(0,0.4424198,0.945098,0) * tex2DNode1 ) , tex2DNode2 , 0.4);
			float4 temp_output_24_0 = ( float4(0.2688679,0.5613208,1,0) * lerpResult3 );
			o.Albedo = temp_output_24_0.rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNDotV32 = dot( normalize( ase_worldNormal ), ase_worldViewDir );
			float fresnelNode32 = ( 0.35 + 1.0 * pow( 1.0 - fresnelNDotV32, 10.0 ) );
			float4 temp_output_35_0 = ( float4(0,0.7372549,0.6783143,0) * ( fresnelNode32 * 0.65 ) );
			o.Emission = temp_output_35_0.rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecular keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutputStandardSpecular o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecular, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
-1673;67;1666;877;538.5327;50.49072;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1607.165,-295.1798;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;140,140;False;1;FLOAT2;0.2,0.5;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1581.565,-165.0798;Float;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;False;0;-0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-1607.265,-1.364973;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;180,180;False;1;FLOAT2;-0.5,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-1541.265,144.9202;Float;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;False;0;0.03;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;8;-1339.265,-202.0798;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1106.265,-224.0798;Float;True;Property;_water;water;0;0;Create;True;0;0;False;0;91d08be02b447134da547e6ede594bf6;91d08be02b447134da547e6ede594bf6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;33;-1117.172,-427.4746;Float;False;Constant;_Color1;Color 1;4;0;Create;True;0;0;False;0;0,0.4424198,0.945098,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;5;-1328.265,66.92019;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-405.8884,371.1814;Float;False;Constant;_Float5;Float 5;4;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-389.5017,288.3067;Float;False;Constant;_Float9;Float 9;4;0;Create;True;0;0;False;0;0.35;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1058.265,-25.07981;Float;True;Property;_water_2;water_2;1;0;Create;True;0;0;False;0;91d08be02b447134da547e6ede594bf6;91d08be02b447134da547e6ede594bf6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;59;41.46729,415.5093;Float;True;Constant;_Float8;Float 8;4;0;Create;True;0;0;False;0;0.65;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-771.172,-410.4746;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-730.2652,114.9202;Float;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;32;-195.4174,294.8633;Float;True;Tangent;4;0;FLOAT3;0,0,1;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;230.4673,332.5093;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;36;-17.748,-3.711752;Float;False;Constant;_Color2;Color 2;4;0;Create;True;0;0;False;0;0,0.7372549,0.6783143,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;-509.2322,-375.2934;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;0.2688679,0.5613208,1,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;3;-636.2652,-193.0798;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;15;-203.4041,95.299;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;42;401.2271,-313.6611;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;48;-259.9155,-632.1564;Float;True;Property;_Foam;Foam;3;0;Create;True;0;0;False;0;4d41775d2e873bf4abf205026d5740b3;3557262c84e142a42a8bbd9dcf58a025;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;38.10413,-785.1579;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-364.4041,166.299;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DepthFade;51;635.655,-70.03442;Float;False;True;1;0;FLOAT;35;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-1542.016,636.0925;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0.2,0.5;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;440.8425,280.9929;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-1516.415,766.1926;Float;False;Constant;_Float7;Float 7;2;0;Create;True;0;0;False;0;-0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-550.4041,266.299;Float;False;Constant;_Float3;Float 3;2;0;Create;True;0;0;False;0;0.65;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;-1027.797,588.9672;Float;True;Property;_noise;noise;2;0;Create;True;0;0;False;0;f5420b2f9cb6d904bb190ae9f6912a91;f5420b2f9cb6d904bb190ae9f6912a91;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-334.998,644.8375;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;56;-333.8959,-830.1579;Float;False;Constant;_Color4;Color 4;4;0;Create;True;0;0;False;0;0.1556604,1,0.3084456,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-269.019,-199.3931;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;48.59973,585.4851;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-530.4041,85.29901;Float;False;Constant;_Float4;Float 4;2;0;Create;True;0;0;False;0;0.65;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-700.4195,581.6511;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-342.4041,48.29901;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-886.3957,873.6478;Float;False;Constant;_Float6;Float 6;3;0;Create;True;0;0;False;0;0.001;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;53;650.9656,-437.4911;Float;False;Constant;_Color3;Color 3;4;0;Create;True;0;0;False;0;0.9620397,1,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;41;266.4777,60.6625;Float;False;True;1;0;FLOAT;150;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;46;684.1949,38.44901;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;50;893.7495,-237.3584;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;28;-1274.116,729.1926;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1437.125,50.5452;Float;False;True;2;Float;ASEMaterialInspector;0;0;StandardSpecular;New AmplifyShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;8;0;10;0
WireConnection;8;2;9;0
WireConnection;1;1;8;0
WireConnection;5;0;7;0
WireConnection;5;2;6;0
WireConnection;2;1;5;0
WireConnection;34;0;33;0
WireConnection;34;1;1;0
WireConnection;32;1;38;0
WireConnection;32;3;37;0
WireConnection;58;0;32;0
WireConnection;58;1;59;0
WireConnection;3;0;34;0
WireConnection;3;1;2;0
WireConnection;3;2;4;0
WireConnection;15;0;11;0
WireConnection;15;1;12;0
WireConnection;15;2;4;0
WireConnection;42;0;48;0
WireConnection;42;1;24;0
WireConnection;42;2;41;0
WireConnection;48;1;7;0
WireConnection;54;0;56;0
WireConnection;54;1;48;0
WireConnection;12;0;2;0
WireConnection;12;1;13;0
WireConnection;35;0;36;0
WireConnection;35;1;58;0
WireConnection;25;1;28;0
WireConnection;23;0;20;0
WireConnection;23;1;22;0
WireConnection;24;0;31;0
WireConnection;24;1;3;0
WireConnection;40;0;32;0
WireConnection;40;1;23;0
WireConnection;20;0;1;1
WireConnection;20;1;25;0
WireConnection;11;0;1;0
WireConnection;11;1;14;0
WireConnection;46;0;54;0
WireConnection;46;1;35;0
WireConnection;46;2;41;0
WireConnection;50;0;53;0
WireConnection;50;1;42;0
WireConnection;50;2;51;0
WireConnection;28;0;26;0
WireConnection;28;2;27;0
WireConnection;0;0;24;0
WireConnection;0;2;35;0
ASEEND*/
//CHKSM=A30E1292DFEE54D72EBB73FF6C9181E09054C318