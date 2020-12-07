#if VRC_SDK_VRCSDK3
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using static VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;
using ValueType = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters.ValueType;

namespace VRLabs.AV3Manager
{
    public class AV3ManagerWindow : EditorWindow
    {
        // Menu item
        [MenuItem("VRLabs/Avatars 3.0 Manager")]
        public static void Init()
        {
            AV3ManagerWindow window = EditorWindow.GetWindow<AV3ManagerWindow>();
            window.titleContent = new GUIContent("AV3 Manager");
            window.minSize = new Vector2(400, 442);
        }

        // Default parameters
        public static readonly string[] VRCParameters =
        {
            "IsLocal",
            "Viseme",
            "GestureLeft",
            "GestureRight",
            "GestureLeftWeight",
            "GestureRightWeight",
            "AngularY",
            "VelocityX",
            "VelocityY",
            "VelocityZ",
            "Upright",
            "Grounded",
            "Seated",
            "AFK",
            "Supine",
            "GroundProximity"
        };

        private const string _standardNewAnimatorFolder = "Assets/VRLabs/GeneratedAssets/";

        public bool HasFreeParameterSlots { get; set; }
        // UI text
        private static class Content
        {
            public static GUIContent PlaymodeError = new GUIContent("Please exit Play Mode to use this script.");
            public static GUIContent Avatar = new GUIContent("Avatar", "Your avatar.");
        }

        private Vector2 _mainScrollviewPosition;

        // Avatar descriptor
        private VRCAvatarDescriptor _avatar = null;
        // Animator layers
        private LayerOptions[] _layers;
        // Show layers
        private bool _showLayers = false;

        // Rebuild layer objects
        private void RebuildLayers()
        {
            _showLayers = _avatar != null;
            if (_avatar != null)
            {
                _layers = new LayerOptions[_avatar.baseAnimationLayers.Length];
                for (int i = 0; i < _avatar.baseAnimationLayers.Length; i++)
                {
                    _layers[i] = new LayerOptions(this, _avatar.baseAnimationLayers[i], i);
                }
            }
        }

        //Draw GUI
        public void OnGUI()
        {
            // Show message if in play mode
            if (EditorApplication.isPlaying)
            {
                GUILayout.Space(10);
                EditorGUILayout.LabelField(Content.PlaymodeError);
                return;
            }
            _mainScrollviewPosition = EditorGUILayout.BeginScrollView(_mainScrollviewPosition);
            GUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            _avatar = (VRCAvatarDescriptor)EditorGUILayout.ObjectField(Content.Avatar, _avatar, typeof(VRCAvatarDescriptor), true);

            if (EditorGUI.EndChangeCheck())
            {
                _showLayers = _avatar != null;
                if (_avatar != null)
                {
                    _avatar.customExpressions = true;
                    if (_avatar.expressionParameters == null)
                    {
                        GenerateNewExpressionParametersAsset();
                    }
                    if (_avatar.expressionsMenu == null)
                    {
                        GenerateNewExpressionMenuAsset();
                    }

                    HasFreeParameterSlots = _avatar.expressionParameters.parameters.Count(x => string.IsNullOrEmpty(x.name)) > 0;

                    _layers = new LayerOptions[_avatar.baseAnimationLayers.Length];
                    for (int i = 0; i < _avatar.baseAnimationLayers.Length; i++)
                    {
                        _layers[i] = new LayerOptions(this, _avatar.baseAnimationLayers[i], i);
                    }
                }
            }

            if (_showLayers)
            {
                if (_layers == null)
                {
                    RebuildLayers();
                }
                foreach (var l in _layers)
                {
                    GUILayout.Space(10);
                    l.DrawLayerOptions();
                }
            }

            EditorGUILayout.EndScrollView();
        }

        // Generates new Expression parameters Asset
        private void GenerateNewExpressionParametersAsset()
        {
            if (!AssetDatabase.IsValidFolder(_standardNewAnimatorFolder.Substring(0, _standardNewAnimatorFolder.Length - 1)))
            {
                AssetDatabase.CreateFolder("Assets/VRLabs", "GeneratedAssets");
            }
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(_standardNewAnimatorFolder + "Parameters.asset");
            _avatar.expressionParameters = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            // Initialize vrc parameters array
            _avatar.expressionParameters.parameters = new Parameter[VRCExpressionParameters.MAX_PARAMETERS];
            for (int i = 0; i < _avatar.expressionParameters.parameters.Length; i++)
            {
                _avatar.expressionParameters.parameters[i] = new Parameter
                {
                    name = "",
                    valueType = VRCExpressionParameters.ValueType.Int
                };
            }

            // Add default parameters
            _avatar.expressionParameters.parameters[0].name = "VRCEmote";
            _avatar.expressionParameters.parameters[0].valueType = VRCExpressionParameters.ValueType.Int;
            _avatar.expressionParameters.parameters[1].name = "VRCFaceBlendH";
            _avatar.expressionParameters.parameters[1].valueType = VRCExpressionParameters.ValueType.Float;
            _avatar.expressionParameters.parameters[2].name = "VRCFaceBlendV";
            _avatar.expressionParameters.parameters[2].valueType = VRCExpressionParameters.ValueType.Float;

            AssetDatabase.CreateAsset(_avatar.expressionParameters, uniquePath);
            EditorUtility.SetDirty(_avatar.expressionParameters);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // Generates new expression menu asset
        private void GenerateNewExpressionMenuAsset()
        {
            if (!AssetDatabase.IsValidFolder(_standardNewAnimatorFolder.Substring(0, _standardNewAnimatorFolder.Length - 1)))
            {
                AssetDatabase.CreateFolder("Assets/VRLabs", "GeneratedAssets");
            }
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(_standardNewAnimatorFolder + "Menu.asset");
            _avatar.expressionsMenu = ScriptableObject.CreateInstance<VRCExpressionsMenu>();
            AssetDatabase.CreateAsset(_avatar.expressionsMenu, uniquePath);
            EditorUtility.SetDirty(_avatar.expressionsMenu);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // Updates a layer value, need to do this cause the CustomAnimLayer is a struct and not a class.
        public void UpdateLayer(int index, VRCAvatarDescriptor.CustomAnimLayer layer)
        {
            _avatar.baseAnimationLayers[index] = layer;
        }

        // Check if the provided parameter is in the list.
        public bool IsParameterInList(AnimatorControllerParameter parameter)
        {
            Parameter o = _avatar.expressionParameters.FindParameter(parameter.name);
            return o != null;
        }
        // Adds or removes a parameter based on the enabled boolean.
        public void UpdateParameter(AnimatorControllerParameter parameter, bool enabled)
        {
            if (parameter.type == AnimatorControllerParameterType.Int || parameter.type == AnimatorControllerParameterType.Float)
            {
                bool somethingModified = false;
                if (enabled)
                {
                    foreach (var par in _avatar.expressionParameters.parameters)
                    {
                        if (string.IsNullOrEmpty(par.name))
                        {
                            par.name = parameter.name;
                            par.valueType = parameter.type == AnimatorControllerParameterType.Int ? ValueType.Int : ValueType.Float;
                            somethingModified = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var par in _avatar.expressionParameters.parameters)
                    {
                        if (par.name.Equals(parameter.name))
                        {
                            par.name = "";
                            par.valueType = ValueType.Int;
                            somethingModified = true;
                        }
                    }
                }

                if (somethingModified)
                {
                    foreach (var layer in _layers)
                    {
                        layer.UpdateParameterList();
                    }
                }
                EditorUtility.SetDirty(_avatar.expressionParameters);
            }
            HasFreeParameterSlots = _avatar.expressionParameters.parameters.Count(x => string.IsNullOrEmpty(x.name)) > 0;
        }

        // Check if a specific parameter is a duplicate
        public bool IsParameterDuplicate(string name)
        {
            foreach (var layer in _layers)
            {
                if (layer.Controller?.parameters.Count(x => x.name.Equals(name)) > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

#endif