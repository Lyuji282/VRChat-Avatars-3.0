﻿#if VRC_SDK_VRCSDK3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;
using VRC.SDK3.Avatars.ScriptableObjects;

namespace VRLabs.AV3Manager
{
    /***
       * The first three functions won't overwrite original assets! Copies are created first in your provided 'directory'.
       * There, only inside 'directory' can a function overwrite the avatar descriptor's assets.
       * This is so you can call the functions repeatedly, and it won't create multiple unneccessary assets:

              // will create 1 new parameter asset, not 3
              AddParameter(myDescriptor, parameterA, uniqueDirectory);
              AddParameter(myDescriptor, parameterB, uniqueDirectory);
              AddParameter(myDescriptor, parameterC, uniqueDirectory);

       * Consequently,always specify a unique directory per script-run! Otherwise you can accidentally overwrite the avatar descriptor's assets.
       * A good method is to seed a folder name with the date and time, at the beginning of your script:
       
              // guarantees no assets are overwritten
              string uniqueDirectory = "Assets/MyCoolScript/Generated/" + DateTime.Now.ToString("MM.dd HH.mm.ss") + "/";
              Directory.CreateDirectory(uniqueDirectory);

       * In an emergency case, set the "overwrite" parameter to false in your first instance of the call to the function:
      
              // also guarantees no assets are overwritten
              AddParameter(myDescriptor, parameterA, someNotUniqueDirectory, false);
              AddParameter(myDescriptor, parameterB, someNotUniqueDirectory);
              AddParameter(myDescriptor, parameterC, someNotUniqueDirectory);
       
       * Contact ksivl#4278 if you experience problems or console errors with this class, and enjoy.
       ***/

    /// <summary>
    /// Helpful functions for script writers using the VRChat Avatars 3.0 SDK and VRLabs 3.0 manager.
    /// Merge controllers, add parameters, and add submenus to an avatar.
    /// </summary>
    public class AV3ManagerFunctions : Editor
    {
        private const string defaultDirectory = "Assets/VRLabs/GeneratedAssets/";
        private static readonly string[] defaultLayerPath = new string[]
        {
            "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3LocomotionLayer.controller",
            "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3IdleLayer.controller",
            "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3HandsLayer.controller",
            "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3ActionLayer.controller"
        };
        public enum PlayableLayer // for function MergeToLayer
        {
            Base = 0,
            Additive = 1,
            Gesture = 2,
            Action = 3,
            FX = 4
        }

        /// <summary>
        /// Creates a copy of the avatar descriptor's parameter asset or creates one if it doesn't exist, adds a provided parameter,
        /// assigns the new parameter asset, and stores it in the specified directory.
        /// </summary>
        /// <param name="descriptor">The avatar descriptor to add the parameter to.</param>
        /// <param name="parameter">The parameter to add.</param>
        /// <param name="directory">The unique directory to store the new parameter asset, ex. "Assets/MyCoolScript/GeneratedAssets/725638/".</param>
        /// <param name="overwrite">Optionally, choose to not overwrite an asset of the same name in directory. See class for more info.</param>
        public static void AddParameter(VRCAvatarDescriptor descriptor, VRCExpressionParameters.Parameter parameter, string directory, bool overwrite = true)
        {
            if (descriptor == null)
            {
                Debug.LogError("Couldn't add the parameter, the avatar descriptor is null!");
                return;
            } 
            else if ((parameter == null) || (parameter.name == null) )
            {
                Debug.LogError("Couldn't add the parameter, it or its name is null!");
                return;
            }
            else if ((directory == null) || (directory == ""))
            {
                Debug.Log("Directory was not specified, storing new parameters asset in " + defaultDirectory);
                directory = defaultDirectory;
            }

            descriptor.customExpressions = true;
            VRCExpressionParameters parameters = ScriptableObject.CreateInstance<VRCExpressionParameters>();
            parameters.parameters = new VRCExpressionParameters.Parameter[0];

            if (descriptor.expressionParameters == null)
            {
                AssetDatabase.CreateAsset(parameters, directory + "Parameters.asset");
            }
            else
            {
                if ((descriptor.expressionParameters.CalcTotalCost() + VRCExpressionParameters.TypeCost(parameter.valueType)) > VRCExpressionParameters.MAX_PARAMETER_COST)
                {
                    Debug.LogError("Couldn't add parameter '" + parameter.name + "', not enough memory free in the avatar's parameter asset!");
                    return;
                }

                string path = (directory + descriptor.expressionParameters.name + ".asset");
                path = (overwrite) ? path : AssetDatabase.GenerateUniqueAssetPath(path);
                if ( AssetDatabase.GetAssetPath(descriptor.expressionParameters) != path) // if we have not made a copy yet
                { // CopyAsset with two identical strings yields exception
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(descriptor.expressionParameters), path);
                }
                parameters = AssetDatabase.LoadAssetAtPath(path, typeof(VRCExpressionParameters)) as VRCExpressionParameters;
            }

            if (parameters.FindParameter(parameter.name) == null)
            {
                int count = parameters.parameters.Length;
                VRCExpressionParameters.Parameter[] parameterArray = new VRCExpressionParameters.Parameter[count + 1];
                for (int i = 0; i < count; i++)
                {
                    parameterArray[i] = parameters.GetParameter(i);
                }
                parameterArray[count] = parameter;
                parameters.parameters = parameterArray;
            }

            EditorUtility.SetDirty(parameters);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            descriptor.expressionParameters = parameters;
        }

        /// <summary>
        /// Creates a copy of the avatar descriptor's topmost menu asset or creates one if it doesn't exist, adds the provided menu as a submenu,
        /// assigns the new topmost menu asset, and stores it in the specified directory.
        /// </summary>
        /// <param name="descriptor">The avatar descriptor to add the submenu to.</param>
        /// <param name="menuToAdd">The menu to add, which will become a submenu of the topmost menu.</param>
        /// <param name="controlName">The name of the submenu control for the menu to add.</param>
        /// <param name="directory">The unique directory to store the new topmost menu asset, ex. "Assets/MyCoolScript/GeneratedAssets/725638/".</param>
        /// <param name="overwrite">Optionally, choose to not overwrite an asset of the same name in directory. See class for more info.</param>
        public static void AddSubMenu(VRCAvatarDescriptor descriptor, VRCExpressionsMenu menuToAdd, string controlName, string directory, bool overwrite = true)
        {
            if (descriptor == null)
            {
                Debug.LogError("Couldn't add the menu, the avatar descriptor is null!");
                return;
            }
            else if ((menuToAdd == null) || (controlName == null) || (controlName == ""))
            {
                Debug.LogError("Couldn't add the menu, it or the name of its control is null!");
                return;
            }
            else if ((directory == null) || (directory == ""))
            {
                Debug.Log("Directory was not specified, storing new topmost menu in " + defaultDirectory);
                directory = defaultDirectory;
            }

            descriptor.customExpressions = true;
            VRCExpressionsMenu topMenu = ScriptableObject.CreateInstance<VRCExpressionsMenu>();

            if (descriptor.expressionsMenu == null)
            {
                AssetDatabase.CreateAsset(topMenu, directory + "Menu Topmost.asset");
            }
            else
            {
                if (descriptor.expressionsMenu.controls.Count == 8)
                {
                    Debug.LogWarning("Couldn't add menu. Please have an available slot in your avatar's topmost Expression Menu.");
                    return;
                }

                string path = (directory + descriptor.expressionsMenu.name + ".asset");
                path = (overwrite) ? path : AssetDatabase.GenerateUniqueAssetPath(path);
                if (AssetDatabase.GetAssetPath(descriptor.expressionsMenu) != path) // if we have not made a copy yet
                { // CopyAsset with two identical strings yields exception
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(descriptor.expressionsMenu), path);
                }
                topMenu = AssetDatabase.LoadAssetAtPath(path, typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;
            }

            List<VRCExpressionsMenu.Control> controlList = topMenu.controls;

            for (int i = 0; i < controlList.Count; i++)
            {
                if (controlList[i].name.Equals(controlName) && controlList[i].type.Equals(VRCExpressionsMenu.Control.ControlType.SubMenu))
                { // if a control for a submenu exists with the same name, replace the submenu
                    controlList[i].subMenu = menuToAdd;
                    EditorUtility.SetDirty(topMenu);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    descriptor.expressionsMenu = topMenu;
                    return;
                }
            }

            VRCExpressionsMenu.Control control = new VRCExpressionsMenu.Control
                { name = controlName, type = VRCExpressionsMenu.Control.ControlType.SubMenu, subMenu = menuToAdd };
            controlList.Add(control);
            topMenu.controls = controlList;

            EditorUtility.SetDirty(topMenu);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            descriptor.expressionsMenu = topMenu;
        }

        /// <summary>
        /// Merges a controller "as new" with the specified playable layer on an avatar's descriptor,
        /// assigns it on the avatar, and stores the new controller at the given path.
        /// </summary>
        /// <param name="descriptor">The avatar descriptor that merging is being done on.</param>
        /// <param name="controllerToAdd">The controller to merge to the playable layer.</param>
        /// <param name="playable">The playable layer to merge to.</param>
        /// <param name="directory">The unique directory to store the new merged controller, ex. "Assets/MyCoolScript/GeneratedAssets/725638/".</param>
        /// <param name="overwrite">Optionally, choose to not overwrite an asset of the same name in directory. See class for more info.</param>
        public static void MergeToLayer(VRCAvatarDescriptor descriptor, AnimatorController controllerToAdd, PlayableLayer playable, string directory, bool overwrite = true)
        {
            int layer = (int)playable;

            if (descriptor == null)
            {
                Debug.LogError("The avatar descriptor is null! Merging was not performed.");
                return;
            }
            else if (controllerToAdd == null)
            {
                Debug.LogError("The controller to add is null! Merging was not performed.");
                return;
            }
            else if ((layer < 0) || (layer > 4))
            {
                Debug.LogError("Layer is out of bounds! Merging was not performed.");
                return;
            }
            else if (layer < 4) // fx layer has no default layer
            {
                if ((AssetDatabase.LoadAssetAtPath(defaultLayerPath[layer], typeof(AnimatorController)) as AnimatorController) == null)
                {
                    Debug.LogError("Couldn't find VRChat's default animator controller at path '" + defaultLayerPath[layer] + "'! Merging was not performed.");
                    return;
                }
            }
            else if (directory == null || directory == "")
            {
                Debug.Log("Directory was not specified, storing new controller in " + defaultDirectory);
                directory = defaultDirectory;
            }

            if ((descriptor.baseAnimationLayers[layer].isDefault == true) || descriptor.baseAnimationLayers[layer].animatorController == null)
            {
                descriptor.customizeAnimationLayers = true;
                descriptor.baseAnimationLayers[layer].isDefault = false;

                AnimatorController controllerFromNew = new AnimatorController();
                string pathFromNew = directory + playable.ToString() + ".controller";

                if (layer == 4) // fx layer has no default layer
                {   // you cannot add a layer to a controller without creating its asset first
                    AssetDatabase.CreateAsset(controllerFromNew, pathFromNew);
                    controllerFromNew.AddLayer("Base Layer");
                }
                else
                {
                    AssetDatabase.CopyAsset(defaultLayerPath[layer], pathFromNew);
                    controllerFromNew = AssetDatabase.LoadAssetAtPath(pathFromNew, typeof(AnimatorController)) as AnimatorController;
                }
                descriptor.baseAnimationLayers[layer].animatorController = controllerFromNew;
            }

            string path = (directory + descriptor.baseAnimationLayers[layer].animatorController.name + ".controller");
            path = (overwrite) ? path : AssetDatabase.GenerateUniqueAssetPath(path);
            if (AssetDatabase.GetAssetPath(descriptor.baseAnimationLayers[layer].animatorController) != path) // if we have not made a copy yet
            { // CopyAsset with two identical strings yields exception
                AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(descriptor.baseAnimationLayers[layer].animatorController), path);
            }

            AnimatorController controllerOriginal = AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController)) as AnimatorController;
            AnimatorController mergedController = AnimatorCloner.MergeControllers(controllerOriginal, controllerToAdd, null, false);

            descriptor.baseAnimationLayers[layer].animatorController = mergedController;
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// Sets the "Write Defaults" value of all the states in an entire animator controller to true or false.
        /// Will modify the controller directly.
        /// </summary>
        /// <param name="controller">The controller to modify.</param>
        /// <param name="writeDefaults">The value of "Write Defaults" to set the controller's states to. True if unspecified.</param>
        /// <returns></returns>
        public static void SetWriteDefaults(AnimatorController controller, bool writeDefaults = true)
        {
            if (controller == null)
            {
                Debug.LogError("Couldn't set Write Defaults value, the controller is null!");
                return;
            }
            for (int i = 0; i < controller.layers.Length; i++)
            {
                SetInStateMachine(controller.layers[i].stateMachine, writeDefaults);
            }
            EditorUtility.SetDirty(controller);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        private static AnimatorStateMachine SetInStateMachine (AnimatorStateMachine stateMachine, bool wd)
        {
            for (int i = 0; i < stateMachine.states.Length; i++)
            {
                stateMachine.states[i].state.writeDefaultValues = wd;
            }
            for (int i = 0; i < stateMachine.stateMachines.Length; i++)
            {
                SetInStateMachine(stateMachine.stateMachines[i].stateMachine, wd);
            }
            return stateMachine;
        }
    }
}
#endif