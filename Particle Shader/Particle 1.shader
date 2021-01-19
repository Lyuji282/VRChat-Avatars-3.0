// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "VRLabs/Particle Shader1"
{
    Properties
    {
        _MainTex("Albedo", 2D) = "white" {}
        _UVScroll("Albedo Scroll", Vector) = (0.0,0.0,0.0)
        _Color("Albedo color", Color) = (1,1,1,1)

        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Color", 2D) = "white" {}

        _DistortionStrength(" ", Float) = 1.0
        _DistortionBlend(" ", Range(0.0, 1.0)) = 0.5

        _SoftParticlesNearFadeDistance("Near fade", Float) = 0.0
        _SoftParticlesFarFadeDistance("Far fade", Float) = 1.0
        _CameraNearFadeDistance("Near fade", Float) = 1.0
        _CameraFarFadeDistance("Far fade", Float) = 2.0

        // Hidden properties
        [HideInInspector] _MainTex2Enabled ("Albedo2", Float) = 0.0
        [HideInInspector] _MainTex2("Albedo2", 2D) = "white" {}
        [HideInInspector] _UVScroll2("Albedo2 Scroll (Scale ZW)", Vector) = (0.0,0.0,1.0,1.0)
        [HideInInspector] _Color2("Albedo2 Color", Color) = (1,1,1,1)
        [HideInInspector] _MainTex2Blend ("Blend Mode", Float) = 13
        [HideInInspector] _UVDistEnable ("UV Distortion", Float) = 0.0
        [HideInInspector] _UVDisTex("Distortion Texture", 2D) = "white" {}
        [HideInInspector] _UVDisSpeed("Speed (Scale ZW)", Vector) = (0.5,0.5,1.0)
        [HideInInspector] _Mode ("Rendering Mode", Float) = 0.0
        [HideInInspector] _ColorMode ("Color Mode", Float) = 0.0
        [HideInInspector] _FlipbookMode ("Flip-Book Frame Blending", Float) = 0.0
        [HideInInspector] _LightingEnabled (" ", Float) = 0.0
        [HideInInspector] _DistortionEnabled ("UV Distortion", Float) = 0.0
        [HideInInspector] _DissolveEnabled ("Dissolve", Float) = 0.0
        [HideInInspector] _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        [HideInInspector] _DCutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        [HideInInspector] _DissolveScale ("Dissolve Scale", Float) = 1.0
        [HideInInspector] _EmissionEnabled ("Emission", Float) = 0.0
        [HideInInspector] _BlendOp (" ", Float) = 0.0
        [HideInInspector] _SrcBlend (" ", Float) = 1.0
        [HideInInspector] _DstBlend (" ", Float) = 0.0
        [HideInInspector] _ZWrite (" ", Float) = 1.0
        [HideInInspector] _Cull ("Two Sided", Float) = 2.0
        [HideInInspector] _VertexMod ("Vertex Modulation", Float) = 0.0
        [HideInInspector] _VUVScroll ("Modulation Scroll (Scale ZW)", Vector) = (1.0,1.0,1.0)
        [HideInInspector] _NoiseTex ("Vertex Noise",2D) = "grey" {}
        [HideInInspector] _Amplitude ("Amplitude", Float) = 3.0
        [HideInInspector] _RimEn ("Rim Effect", Float) = 0.0
        [HideInInspector] _RimVal ("Power", Float) = 1.0
        [HideInInspector] [HDR] _RimColor ("Color", Color) = (1,1,1,1)
        [HideInInspector] _SoftParticlesEnabled ("Soft Particles", Float) = 0.0
        [HideInInspector] _CameraFadingEnabled ("Camera Fading", Float) = 0.0
        [HideInInspector] _SoftParticleFadeParams ("", Vector) = (0,0,0,0)
        [HideInInspector] _CameraFadeParams (" ", Vector) = (0,0,0,0)
        [HideInInspector] _ColorAddSubDiff (" ", Vector) = (0,0,0,0)
        [HideInInspector] _DistortionStrengthScaled (" ", Float) = 0.0
    }

    Category
    {
        SubShader
        {
            Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "PreviewType"="Plane" "PerformanceChecks"="False" }

            BlendOp [_BlendOp]
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            Cull [_Cull]
            ColorMask RGB

            Pass
            {
                Name "ShadowCaster"
                Tags { "LightMode" = "ShadowCaster" }

                BlendOp Add
                Blend One Zero
                ZWrite On
                Cull Off

                CGPROGRAM
                #pragma target 2.5

                #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
                #pragma shader_feature _ _COLOROVERLAY_ON _COLORCOLOR_ON _COLORADDSUBDIFF_ON
                #pragma shader_feature _REQUIRE_UV2
                #pragma multi_compile_shadowcaster
                #pragma multi_compile_instancing

                #pragma vertex vertParticleShadowCaster
                #pragma fragment fragParticleShadowCaster

                #include "UnityStandardParticleShadow.cginc"
                ENDCG
            }

            Pass
            {
                Name "SceneSelectionPass"
                Tags { "LightMode" = "SceneSelectionPass" }

                BlendOp Add
                Blend One Zero
                ZWrite On
                Cull Off

                CGPROGRAM
                #pragma target 2.5

                #pragma shader_feature _ _ALPHATEST_ON
                #pragma shader_feature _REQUIRE_UV2
                #pragma multi_compile_instancing

                #pragma vertex vertEditorPass
                #pragma fragment fragSceneHighlightPass

                #include "UnityStandardParticleEditor.cginc"
                ENDCG
            }

            Pass
            {
                Name "ScenePickingPass"
                Tags{ "LightMode" = "Picking" }

                BlendOp Add
                Blend One Zero
                ZWrite On
                Cull Off

                CGPROGRAM
                #pragma target 2.5

                #pragma shader_feature _ _ALPHATEST_ON
                #pragma shader_feature _REQUIRE_UV2
                #pragma multi_compile_instancing

                #pragma vertex vertEditorPass
                #pragma fragment fragScenePickingPass

                #include "UnityStandardParticleEditor.cginc"
                ENDCG
            }

            Pass
            {
                Tags { "LightMode"="ForwardBase" }

                CGPROGRAM
                #pragma multi_compile __ SOFTPARTICLES_ON
                #pragma multi_compile_fog
                #pragma target 2.5

                #pragma shader_feature _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
                #pragma shader_feature _ _COLOROVERLAY_ON _COLORCOLOR_ON _COLORADDSUBDIFF_ON
                #pragma shader_feature _NORMALMAP
                #pragma shader_feature _EMISSION
                #pragma shader_feature _FADING_ON
                #pragma shader_feature _REQUIRE_UV2
                #pragma shader_feature EFFECT_BUMP

                #pragma vertex vertParticleUnlit
                #pragma fragment fragParticleUnlit
                #pragma multi_compile_instancing

                #include "CGINC/ParticleShaderIncludes.cginc"
                ENDCG
            }
        }
    }

    Fallback "VertexLit"
    CustomEditor "VRLabs.ParticleShader.ParticleShaderGUITest"
}
