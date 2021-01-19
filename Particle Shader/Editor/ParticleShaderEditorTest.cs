// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using VRLabs.SimpleShaderInspectors;
using VRLabs.SimpleShaderInspectors.Controls;
using VRLabs.SimpleShaderInspectors.Controls.Sections;

namespace VRLabs.ParticleShader
{
    internal class ParticleShaderGUITest : SimpleShaderInspector
    {
        public enum BlendMode
        {
            Opaque,
            Cutout,
            Fade,   // Old school alpha-blending mode, fresnel does not affect amount of transparency
            Transparent, // Physically plausible transparency mode, implemented as alpha pre-multiply
            Additive,
            Subtractive,
            Modulate
        }

        public enum FlipbookMode
        {
            Simple,
            Blended
        }

        public enum ColorMode
        {
            Multiply,
            Additive,
            Subtractive,
            Overlay,
            Color,
            Difference
        }

        public enum VRLBlend
        {
            ColorBurn,
            LinearBurn,
            DarkerColor,
            Lighten,
            Screen,
            ColorDodge,
            LinearDodge,
            LighterColor,
            Overlay,
            Difference,
            Subtract,
            Divide,
            Add,
            Multiply,
            Hue,
            Color,
            Saturation,
            Luminosity,
        }

        private static class Styles
        {
            public static GUIContent albedoText = EditorGUIUtility.TrTextContent("Albedo", "Albedo (RGB) and Transparency (A).");
            public static GUIContent albedo2EnabledText = EditorGUIUtility.TrTextContent("Albedo2", "Albedo2 Options.");
            public static GUIContent albedo2Text = EditorGUIUtility.TrTextContent("Albedo2", "Albedo2 (RGB) and Transparency (A).");
            public static GUIContent albedoScrollText = EditorGUIUtility.TrTextContent("Albedo Scroll", "Albedo Scroll Speed. (XY) )");
            public static GUIContent albedo2ScrollText = EditorGUIUtility.TrTextContent("Albedo2 Scroll (Scale ZW)", "Albedo2 Scroll Speed. (XY) Scale. (Z) )");
            public static GUIContent noiseText = EditorGUIUtility.TrTextContent("Vertex Noise", "Vertex Noise.");
            public static GUIContent amplitudeText = EditorGUIUtility.TrTextContent("Amplitude", "Vertex Modulation Amplitude");
            public static GUIContent modScrollText = EditorGUIUtility.TrTextContent("Modulation Scroll (Scale ZW)", "Vertex Modulation Scroll Speed. (XY) Scale. (ZW) )");
            public static GUIContent alphaCutoffText = EditorGUIUtility.TrTextContent("Alpha Cutoff", "Threshold for alpha cutoff.");
            public static GUIContent DalphaCutoffText = EditorGUIUtility.TrTextContent("Alpha Cutoff", "Threshold for alpha cutoff.");
            public static GUIContent dissolveText = EditorGUIUtility.TrTextContent("Dissolve Texture", "Texture for alpha clipping. (R)");
            public static GUIContent dissolveScaleText = EditorGUIUtility.TrTextContent("Dissolve Scale", "Scale of dissovle texture.");
            public static GUIContent smoothnessText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness value.");
            public static GUIContent smoothnessScaleText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness scale factor.");
            public static GUIContent normalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map.");
            public static GUIContent emissionText = EditorGUIUtility.TrTextContent("Color", "Emission (RGB).");

            public static GUIContent renderingMode = EditorGUIUtility.TrTextContent("Rendering Mode", "Determines the transparency and blending method for drawing the object to the screen.");
            public static GUIContent[] blendNames = Array.ConvertAll(Enum.GetNames(typeof(BlendMode)), item => new GUIContent(item));

            public static GUIContent colorMode = EditorGUIUtility.TrTextContent("Color Mode", "Determines the blending mode between the particle color and the texture albedo.");
            public static GUIContent[] colorNames = Array.ConvertAll(Enum.GetNames(typeof(ColorMode)), item => new GUIContent(item));
            public static GUIContent albedo2BlendText = EditorGUIUtility.TrTextContent("Blend Mode", "Albedo2 Blend Mode)");
            public static GUIContent[] VRLblendNames = Array.ConvertAll(Enum.GetNames(typeof(VRLBlend)), item => new GUIContent(item));

            public static GUIContent flipbookBlending = EditorGUIUtility.TrTextContent("Flip-Book Frame Blending", "Enables blending between the frames of animated texture sheets.");
            public static GUIContent twoSidedEnabled = EditorGUIUtility.TrTextContent("Two Sided", "Render both front and back faces of the particle geometry.");
            public static GUIContent vertexModEnabled = EditorGUIUtility.TrTextContent("Vertex Modulation", "Distort verts with noise texture.");
            public static GUIContent dissolveEnabledText = EditorGUIUtility.TrTextContent("Dissolve", "Dissolve with mask texture.");
            public static GUIContent rimEnabled = EditorGUIUtility.TrTextContent("Rim Effect", "Fresnel rim effect.");
            public static GUIContent rimValText = EditorGUIUtility.TrTextContent("Power", "Rim effect power.");
            public static GUIContent rimColorText = EditorGUIUtility.TrTextContent("Color", "Rim effect color.");

            public static GUIContent distortionEnabled = EditorGUIUtility.TrTextContent("UV Distortion", "Use a noise texture to distort UVs.");
            public static GUIContent distortionMapText = EditorGUIUtility.TrTextContent("Distortion Texture", "UV Distortion Texture. (R)");
            public static GUIContent distortionSpeedText = EditorGUIUtility.TrTextContent("Speed (Scale ZW)", "UV Distortion scrolling speed. (XY) Scale. (ZW)");

            public static GUIContent softParticlesEnabled = EditorGUIUtility.TrTextContent("Soft Particles", "Fade out particle geometry when it gets close to the surface of objects written into the depth buffer.");
            public static GUIContent softParticlesNearFadeDistanceText = EditorGUIUtility.TrTextContent("Near fade", "Soft Particles near fade distance.");
            public static GUIContent softParticlesFarFadeDistanceText = EditorGUIUtility.TrTextContent("Far fade", "Soft Particles far fade distance.");

            public static GUIContent cameraFadingEnabled = EditorGUIUtility.TrTextContent("Camera Fading", "Fade out particle geometry when it gets close to the camera.");
            public static GUIContent cameraNearFadeDistanceText = EditorGUIUtility.TrTextContent("Near fade", "Camera near fade distance.");
            public static GUIContent cameraFarFadeDistanceText = EditorGUIUtility.TrTextContent("Far fade", "Camera far fade distance.");

            public static GUIContent emissionEnabled = EditorGUIUtility.TrTextContent("Emission");

            public static GUIContent blendingOptionsText = EditorGUIUtility.TrTextContent("Blending Options");
            public static GUIContent mainOptionsText = EditorGUIUtility.TrTextContent("Main Options");
            public static GUIContent mapsOptionsText = EditorGUIUtility.TrTextContent("Maps");
            public static GUIContent requiredVertexStreamsText = EditorGUIUtility.TrTextContent("Required Vertex Streams");

            public static GUIContent streamPositionText = EditorGUIUtility.TrTextContent("Position (POSITION.xyz)");
            public static GUIContent streamNormalText = EditorGUIUtility.TrTextContent("Normal (NORMAL.xyz)");
            public static GUIContent streamColorText = EditorGUIUtility.TrTextContent("Color (COLOR.xyzw)");
            public static GUIContent streamColorInstancedText = EditorGUIUtility.TrTextContent("Color (INSTANCED0.xyzw)");
            public static GUIContent streamUVText = EditorGUIUtility.TrTextContent("UV (TEXCOORD0.xy)");
            public static GUIContent streamUV2Text = EditorGUIUtility.TrTextContent("UV2 (TEXCOORD0.zw)");
            public static GUIContent streamAnimBlendText = EditorGUIUtility.TrTextContent("AnimBlend (TEXCOORD1.x)");
            public static GUIContent streamAnimFrameText = EditorGUIUtility.TrTextContent("AnimFrame (TEXCOORD0.x)");
            public static GUIContent streamApplyToAllSystemsText = EditorGUIUtility.TrTextContent("Apply to Systems", "Apply the vertex stream layout to all Particle Systems using this material");

            public static string undoApplyCustomVertexStreams = L10n.Tr("Apply custom vertex streams from material");
        }

        private EnumControl<BlendMode> _blendMode;
        private EnumControl<ColorMode> _colorMode;
        private KeywordToggleControl _flipbook;
        private ControlContainer _zWriteOffOptions;
        private ToggleListControl _softParticles;
        private ToggleListControl _cameraFading;
        private ToggleListControl _dissolve;
        private ToggleListControl _vertexMod;
        private ToggleListControl _rim;
        private ToggleListControl _distorsion;
        private ToggleListControl _albedo2;
        private KeywordToggleListControl _emission;

        private TilingAndOffsetControl _mainTileAndOffset;

        private TextureControl _albedoControl;
        private TextureControl _emissionControl;

        private VertexStreamsControl _streamsControl;

        private PropertyControl _softParticleNearFade;
        private PropertyControl _softParticleFarFade;
        private PropertyControl _cameraNearFade;
        private PropertyControl _cameraFarFade;

        protected override void Start()
        {
            this.AddLabelControl("BlendingOptionsLabel").SetLabelStyle(EditorStyles.boldLabel);
            _blendMode = this.AddEnumControl<BlendMode>("_Mode");
            _colorMode = this.AddEnumControl<ColorMode>("_ColorMode");
            this.AddSpaceControl();

            this.AddLabelControl("MainOptionsLabel").SetLabelStyle(EditorStyles.boldLabel);
            _flipbook = this.AddKeywordToggleControl("_REQUIRE_UV2");
            this.AddToggleControl("_Cull", 0, 2);

            _zWriteOffOptions = this.AddControlContainer();
            _softParticles = _zWriteOffOptions.AddToggleListControl("_SoftParticlesEnabled");
            _softParticleNearFade = _softParticles.AddPropertyControl("_SoftParticlesNearFadeDistance");
            _softParticleFarFade = _softParticles.AddPropertyControl("_SoftParticlesFarFadeDistance");
            _cameraFading = _zWriteOffOptions.AddToggleListControl("_CameraFadingEnabled");
            _cameraNearFade = _cameraFading.AddPropertyControl("_CameraNearFadeDistance");
            _cameraFarFade = _cameraFading.AddPropertyControl("_CameraFarFadeDistance");

            _dissolve = this.AddToggleListControl("_DissolveEnabled");
            _dissolve.AddTextureControl("_DissolveTex");
            _dissolve.AddPropertyControl("_DCutoff");
            _dissolve.AddPropertyControl("_DissolveScale");

            _vertexMod = this.AddToggleListControl("_VertexMod");
            _vertexMod.AddTextureControl("_NoiseTex");
            _vertexMod.AddPropertyControl("_Amplitude");
            _vertexMod.AddVectorControl("_VUVScroll", true, true, false, false).Alias("VertexUVScroll");
            _vertexMod.AddVectorControl("_VUVScroll", false, false, true, true).Alias("VertexUVScale");

            _rim = this.AddToggleListControl("_RimEn");
            _rim.AddPropertyControl("_RimVal");
            _rim.AddColorControl("_RimColor");

            _distorsion = this.AddToggleListControl("_UVDistEnable");
            _distorsion.AddTextureControl("_UVDisTex");
            _distorsion.AddVectorControl("_UVDisSpeed", true, true, false, false).Alias("UVScrollSpeed");
            _distorsion.AddVectorControl("_UVDisSpeed", false, false, true, true).Alias("UVScrollScale");
            this.AddSpaceControl();

            this.AddLabelControl("MapsOptionsLabel").SetLabelStyle(EditorStyles.boldLabel);
            _albedoControl = this.AddTextureControl("_MainTex", "_Color").SetHasHDRColor(true);
            this.AddVectorControl("_UVScroll", true, true, false, false);
            this.AddSpaceControl();

            _albedo2 = this.AddToggleListControl("_MainTex2Enabled");
            _albedo2.AddTextureControl("_MainTex2", "_Color2").SetHasHDRColor(true);
            _albedo2.AddEnumControl<VRLBlend>("_MainTex2Blend");
            _albedo2.AddVectorControl("_UVScroll2", true, true, false, false).Alias("Albedo2ScrollSpeed");
            _albedo2.AddVectorControl("_UVScroll2", false, false, true, true).Alias("Albedo2ScrollScale");
            this.AddSpaceControl();

            _emission = this.AddKeywordToggleListControl("_EMISSION");
            _emissionControl = _emission.AddTextureControl("_EmissionMap", "_EmissionColor").SetHasHDRColor(true);
            this.AddSpaceControl();

            _mainTileAndOffset = this.AddTilingAndOffsetControl("_MainTex");
            this.AddSpaceControl();

            _streamsControl = this.AddVertexStreamsControl("VertexStreams")
                .AddVertexStream(ParticleSystemVertexStream.Position)
                .AddVertexStream(ParticleSystemVertexStream.Color)
                .AddVertexStream(ParticleSystemVertexStream.UV);
        }

        protected override void StartChecks(MaterialEditor materialEditor)
        {
            SetupMaterialWithBlendMode(Materials, (BlendMode)_blendMode.Property.floatValue);
            SetupMaterialWithColorMode(Materials, (ColorMode)_colorMode.Property.floatValue);
            SetMaterialKeywords(Materials);
            _zWriteOffOptions.IsVisible = Materials.All(x => x.GetInt("_ZWrite") == 0);

            if (_vertexMod.ToggleEnabled || _rim.ToggleEnabled)
                _streamsControl.AddVertexStream(ParticleSystemVertexStream.Normal);
            else
                _streamsControl.RemoveVertexStream(ParticleSystemVertexStream.Normal);

            _mainTileAndOffset.SetVisibility(!_flipbook.ToggleEnabled);

            if (_flipbook.ToggleEnabled)
            {
                _streamsControl.AddVertexStream(ParticleSystemVertexStream.UV2)
                    .AddVertexStream(ParticleSystemVertexStream.AnimBlend)
                    .AddVertexStream(ParticleSystemVertexStream.AnimFrame);
            }
            else
            {
                _streamsControl.RemoveVertexStream(ParticleSystemVertexStream.UV2)
                    .RemoveVertexStream(ParticleSystemVertexStream.AnimBlend)
                    .RemoveVertexStream(ParticleSystemVertexStream.AnimFrame);
            }

            _emissionControl.Property.textureScaleAndOffset = _albedoControl.Property.textureScaleAndOffset;
        }

        protected override void CheckChanges(MaterialEditor materialEditor)
        {
            if (_blendMode.HasPropertyUpdated)
            {
                SetupMaterialWithBlendMode(Materials, (BlendMode)_blendMode.Property.floatValue);
                _zWriteOffOptions.IsVisible = Materials.All(x => x.GetInt("_ZWrite") == 0);
                SetMaterialKeywords(Materials);
            }

            if (_colorMode.HasPropertyUpdated)
            {
                SetupMaterialWithColorMode(Materials, (ColorMode)_colorMode.Property.floatValue);
            }

            if (_vertexMod.HasPropertyUpdated || _rim.HasPropertyUpdated)
            {
                if (_vertexMod.ToggleEnabled || _rim.ToggleEnabled)
                    _streamsControl.AddVertexStream(ParticleSystemVertexStream.Normal);
                else
                    _streamsControl.RemoveVertexStream(ParticleSystemVertexStream.Normal);
            }

            if (_flipbook.HasKeywordUpdated)
            {
                _mainTileAndOffset.SetVisibility(!_flipbook.HasKeywordUpdated);

                if (_flipbook.ToggleEnabled)
                {
                    _streamsControl.AddVertexStream(ParticleSystemVertexStream.UV2)
                        .AddVertexStream(ParticleSystemVertexStream.AnimBlend)
                        .AddVertexStream(ParticleSystemVertexStream.AnimFrame);
                }
                else
                {
                    _streamsControl.RemoveVertexStream(ParticleSystemVertexStream.UV2)
                        .RemoveVertexStream(ParticleSystemVertexStream.AnimBlend)
                        .RemoveVertexStream(ParticleSystemVertexStream.AnimFrame);
                }
            }

            if (_mainTileAndOffset.HasPropertyUpdated)
            {
                _emissionControl.Property.textureScaleAndOffset = _albedoControl.Property.textureScaleAndOffset;
            }
        }

        public override void OnClosed(Material material)
        {
            material.SetShaderPassEnabled("Always", true);
        }

        public static void SetupMaterialWithBlendMode(Material[] materials, BlendMode blendMode)
        {
            switch (blendMode)
            {
                case BlendMode.Opaque:
                    materials.SetOverrideTag("RenderType", "");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    materials.SetInt("_ZWrite", 1);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", false);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue(-1);
                    break;
                case BlendMode.Cutout:
                    materials.SetOverrideTag("RenderType", "TransparentCutout");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    materials.SetInt("_ZWrite", 1);
                    materials.SetKeyword("_ALPHATEST_ON", true);
                    materials.SetKeyword("_ALPHABLEND_ON", false);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.AlphaTest);
                    break;
                case BlendMode.Fade:
                    materials.SetOverrideTag("RenderType", "Transparent");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    materials.SetInt("_ZWrite", 0);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", true);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.Transparent);
                    break;
                case BlendMode.Transparent:
                    materials.SetOverrideTag("RenderType", "Transparent");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    materials.SetInt("_ZWrite", 0);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", false);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", true);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.Transparent);
                    break;
                case BlendMode.Additive:
                    materials.SetOverrideTag("RenderType", "Transparent");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials.SetInt("_ZWrite", 0);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", true);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.Transparent);
                    break;
                case BlendMode.Subtractive:
                    materials.SetOverrideTag("RenderType", "Transparent");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.ReverseSubtract);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    materials.SetInt("_ZWrite", 0);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", true);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", false);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.Transparent);
                    break;
                case BlendMode.Modulate:
                    materials.SetOverrideTag("RenderType", "Transparent");
                    materials.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
                    materials.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.DstColor);
                    materials.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    materials.SetInt("_ZWrite", 0);
                    materials.SetKeyword("_ALPHATEST_ON", false);
                    materials.SetKeyword("_ALPHABLEND_ON", false);
                    materials.SetKeyword("_ALPHAPREMULTIPLY_ON", false);
                    materials.SetKeyword("_ALPHAMODULATE_ON", true);
                    materials.SetRenderQueue((int)UnityEngine.Rendering.RenderQueue.Transparent);
                    break;
            }
        }

        public static void SetupMaterialWithColorMode(Material[] materials, ColorMode colorMode)
        {
            switch (colorMode)
            {
                case ColorMode.Multiply:
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    materials.SetKeyword("_COLORCOLOR_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", false);
                    break;
                case ColorMode.Overlay:
                    materials.SetKeyword("_COLORCOLOR_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", false);
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    break;
                case ColorMode.Color:
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", false);
                    materials.SetKeyword("_COLORCOLOR_ON", true);
                    break;
                case ColorMode.Difference:
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    materials.SetKeyword("_COLORCOLOR_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", true);
                    materials.SetVector("_ColorAddSubDiff", new Vector4(-1.0f, 1.0f, 0.0f, 0.0f));
                    break;
                case ColorMode.Additive:
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    materials.SetKeyword("_COLORCOLOR_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", true);
                    materials.SetVector("_ColorAddSubDiff", new Vector4(1.0f, 0.0f, 0.0f, 0.0f));
                    break;
                case ColorMode.Subtractive:
                    materials.SetKeyword("_COLOROVERLAY_ON", false);
                    materials.SetKeyword("_COLORCOLOR_ON", false);
                    materials.SetKeyword("_COLORADDSUBDIFF_ON", true);
                    materials.SetVector("_ColorAddSubDiff", new Vector4(-1.0f, 0.0f, 0.0f, 0.0f));
                    break;
            }
        }

        private void SetMaterialKeywords(Material[] materials)
        {
            // Set the define for fading
            bool useFading = (_softParticles.ToggleEnabled || _cameraFading.ToggleEnabled) && _zWriteOffOptions.IsVisible;
            materials.SetKeyword("_FADING_ON", useFading);
            if (_softParticles.ToggleEnabled)
                materials.SetVector("_SoftParticleFadeParams", new Vector4(_softParticleNearFade.Property.floatValue, 1.0f / (_softParticleFarFade.Property.floatValue - _softParticleNearFade.Property.floatValue), 0.0f, 0.0f));
            else
                materials.SetVector("_SoftParticleFadeParams", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            if (_cameraFading.ToggleEnabled)
                materials.SetVector("_CameraFadeParams", new Vector4(_cameraNearFade.Property.floatValue, 1.0f / (_cameraFarFade.Property.floatValue - _cameraNearFade.Property.floatValue), 0.0f, 0.0f));
            else
                materials.SetVector("_CameraFadeParams", new Vector4(0.0f, Mathf.Infinity, 0.0f, 0.0f));
        }
    }
} // namespace UnityEditor
