// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New AmplifyShader"
{
	Properties
	{
		_sand("sand", 2D) = "white" {}
		_sand_Flat("sand_Flat", 2D) = "white" {}
		_Grass("Grass", 2D) = "white" {}
		_Alpha_islands("Alpha_islands", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _sand;
		uniform float4 _sand_ST;
		uniform sampler2D _sand_Flat;
		uniform float4 _sand_Flat_ST;
		uniform sampler2D _Alpha_islands;
		uniform float4 _Alpha_islands_ST;
		uniform sampler2D _Grass;
		uniform float4 _Grass_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_sand = i.uv_texcoord * _sand_ST.xy + _sand_ST.zw;
			float2 uv_sand_Flat = i.uv_texcoord * _sand_Flat_ST.xy + _sand_Flat_ST.zw;
			float4 tex2DNode4 = tex2D( _sand_Flat, uv_sand_Flat );
			float2 uv_Alpha_islands = i.uv_texcoord * _Alpha_islands_ST.xy + _Alpha_islands_ST.zw;
			float4 tex2DNode5 = tex2D( _Alpha_islands, uv_Alpha_islands );
			float4 lerpResult1 = lerp( tex2D( _sand, uv_sand ) , tex2DNode4 , tex2DNode5.r);
			float2 uv_Grass = i.uv_texcoord * _Grass_ST.xy + _Grass_ST.zw;
			float4 lerpResult6 = lerp( lerpResult1 , tex2D( _Grass, uv_Grass ) , tex2DNode5.g);
			float4 lerpResult11 = lerp( lerpResult6 , ( tex2DNode4 * float4(0.3113208,0.2022746,0.1453809,0) ) , tex2DNode5.b);
			o.Albedo = lerpResult11.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
-14;131;1666;877;2191.302;718.5588;1.645932;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-1716.475,-393.41;Float;True;Property;_sand_Flat;sand_Flat;1;0;Create;True;0;0;False;0;e35a49dea4e9f7743b9e8b722fe57a4c;e35a49dea4e9f7743b9e8b722fe57a4c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;5;-1721.037,-180.5597;Float;True;Property;_Alpha_islands;Alpha_islands;3;0;Create;True;0;0;False;0;710ab9c84cc3fba4d876687ac4f5afb1;710ab9c84cc3fba4d876687ac4f5afb1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-1716.475,-575.764;Float;True;Property;_sand;sand;0;0;Create;True;0;0;False;0;3557262c84e142a42a8bbd9dcf58a025;3557262c84e142a42a8bbd9dcf58a025;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-1457.216,377.632;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;0.3113208,0.2022746,0.1453809,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;7;-1542.751,69.58105;Float;True;Property;_Grass;Grass;2;0;Create;True;0;0;False;0;9b76a6a115bc9204cbc3ce89fb9090ba;9b76a6a115bc9204cbc3ce89fb9090ba;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1;-1291.475,-396.7641;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1162.595,257.4789;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;6;-1156.432,-83.44617;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;11;-851.5139,132.3881;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New AmplifyShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;1;0;3;0
WireConnection;1;1;4;0
WireConnection;1;2;5;1
WireConnection;9;0;4;0
WireConnection;9;1;10;0
WireConnection;6;0;1;0
WireConnection;6;1;7;0
WireConnection;6;2;5;2
WireConnection;11;0;6;0
WireConnection;11;1;9;0
WireConnection;11;2;5;3
WireConnection;0;0;11;0
ASEEND*/
//CHKSM=D4E6A500523DA2CABDF1DCF14AC9684127A9133C