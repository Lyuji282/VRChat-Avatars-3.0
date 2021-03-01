#if VRC_SDK_VRCSDK3
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDKBase;

namespace VRLabs.AV3Manager
{
    public static class AnimatorCloner
    {
        private const string _standardNewAnimatorFolder = "Assets/VRLabs/GeneratedAssets/";
        private static Dictionary<string, string> _parametersNewName;
        private static string _assetPath;

        public static AnimatorController MergeControllers(AnimatorController mainController, AnimatorController controllerToMerge, Dictionary<string, string> paramNameSwap = null, bool saveToNew = false)
        {
            if (mainController == null)
            {
                return null;
            }

            _parametersNewName = paramNameSwap ?? new Dictionary<string, string>();
            _assetPath = AssetDatabase.GetAssetPath(mainController);

            if (saveToNew)
            {
                Directory.CreateDirectory("Assets/VRLabs/GeneratedAssets");
                /*f (!AssetDatabase.IsValidFolder(_standardNewAnimatorFolder.Substring(0, _standardNewAnimatorFolder.Length - 1)))
                {
                    AssetDatabase.CreateFolder("Assets/VRLabs", "GeneratedAssets");
                }*/

                string uniquePath = AssetDatabase.GenerateUniqueAssetPath(_standardNewAnimatorFolder + Path.GetFileName(_assetPath));
                AssetDatabase.CopyAsset(_assetPath, uniquePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                _assetPath = uniquePath;
                mainController = AssetDatabase.LoadAssetAtPath<AnimatorController>(_assetPath);
            }

            if (controllerToMerge == null)
            {
                return mainController;
            }

            foreach (var p in controllerToMerge.parameters)
            {
                var newP = new AnimatorControllerParameter
                {
                    name = _parametersNewName.ContainsKey(p.name) ? _parametersNewName[p.name] : p.name,
                    type = p.type,
                    defaultBool = p.defaultBool,
                    defaultFloat = p.defaultFloat,
                    defaultInt = p.defaultInt
                };
                if (mainController.parameters.Count(x => x.name.Equals(newP.name)) == 0)
                {
                    mainController.AddParameter(newP);
                }
            }

            for (int i = 0; i < controllerToMerge.layers.Length; i++)
            {
                AnimatorControllerLayer newL = CloneLayer(controllerToMerge.layers[i], i == 0);
                newL.name = MakeLayerNameUnique(newL.name, mainController);
                mainController.AddLayer(newL);
            }

            EditorUtility.SetDirty(mainController);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return mainController;
        }

        private static string MakeLayerNameUnique(string name, AnimatorController controller)
        {
            string st = "";
            int c = 0;
            bool combinedNameDuplicate = controller.layers.Count(x => x.name.Equals(name)) > 0;
            while (combinedNameDuplicate)
            {
                c++;
                combinedNameDuplicate = controller.layers.Count(x => x.name.Equals(name + st + c)) > 0;
            }
            if (c != 0)
            {
                st += c;
            }

            return name + st;
        }

        private static AnimatorControllerLayer CloneLayer(AnimatorControllerLayer old, bool isFirstLayer = false)
        {
            var n = new AnimatorControllerLayer
            {
                avatarMask = old.avatarMask,
                blendingMode = old.blendingMode,
                defaultWeight = isFirstLayer ? 1f : old.defaultWeight,
                iKPass = old.iKPass,
                name = old.name,
                syncedLayerAffectsTiming = old.syncedLayerAffectsTiming,
                stateMachine = CloneStateMachine(old.stateMachine)
            };
            //AssetDatabase.AddObjectToAsset(n, assetPath);
            CloneTransitions(old.stateMachine, n.stateMachine);
            return n;
        }

        private static AnimatorStateMachine CloneStateMachine(AnimatorStateMachine old)
        {
            var n = new AnimatorStateMachine
            {
                anyStatePosition = old.anyStatePosition,
                entryPosition = old.entryPosition,
                exitPosition = old.exitPosition,
                hideFlags = old.hideFlags,
                name = old.name,
                parentStateMachinePosition = old.parentStateMachinePosition,
                stateMachines = old.stateMachines.Select(x => CloneChildStateMachine(x)).ToArray(),
                states = old.states.Select(x => CloneChildAnimatorState(x)).ToArray()
            };
            AssetDatabase.AddObjectToAsset(n, _assetPath);
            n.defaultState = n.states.Where(x => x.state.nameHash == old.defaultState?.nameHash)
                .Select(x => x.state).FirstOrDefault();

            foreach (var oldb in old.behaviours)
            {
                var behaviour = n.AddStateMachineBehaviour(old.GetType());
                CloneBehaviourParameters(oldb, behaviour);
            }
            return n;
        }

        private static ChildAnimatorStateMachine CloneChildStateMachine(ChildAnimatorStateMachine old)
        {
            var n = new ChildAnimatorStateMachine
            {
                position = old.position,
                stateMachine = CloneStateMachine(old.stateMachine)
            };
            return n;
        }

        private static ChildAnimatorState CloneChildAnimatorState(ChildAnimatorState old)
        {
            var n = new ChildAnimatorState
            {
                position = old.position,
                state = CloneAnimatorState(old.state)
            };
            foreach (var oldb in old.state.behaviours)
            {
                var behaviour = n.state.AddStateMachineBehaviour(oldb.GetType());
                CloneBehaviourParameters(oldb, behaviour);
            }
            return n;
        }

        private static AnimatorState CloneAnimatorState(AnimatorState old)
        {
            // Checks if the motion is a blend tree, to avoid accidental blend tree sharing between animator assets
            Motion motion = old.motion;
            if (motion is BlendTree oldTree)
            {
                var tree = CloneBlendTree(null, oldTree);
                motion = tree;
                // need to save the blend tree into the animator
                tree.hideFlags = HideFlags.HideInHierarchy;
                AssetDatabase.AddObjectToAsset(motion, _assetPath);
            }
            
            var n = new AnimatorState 
            {
                cycleOffset = old.cycleOffset,
                cycleOffsetParameter = old.cycleOffsetParameter,
                cycleOffsetParameterActive = old.cycleOffsetParameterActive,
                hideFlags = old.hideFlags,
                iKOnFeet = old.iKOnFeet,
                mirror = old.mirror,
                mirrorParameter = old.mirrorParameter,
                mirrorParameterActive = old.mirrorParameterActive,
                motion = motion,
                name = old.name,
                speed = old.speed,
                speedParameter = old.speedParameter,
                speedParameterActive = old.speedParameterActive,
                tag = old.tag,
                timeParameter = old.timeParameter,
                timeParameterActive = old.timeParameterActive,
                writeDefaultValues = old.writeDefaultValues
            };
            AssetDatabase.AddObjectToAsset(n, _assetPath);
            return n;
        }
        
        // Taken from here: https://gist.github.com/phosphoer/93ca8dcbf925fc006e4e9f6b799c13b0
        private static BlendTree CloneBlendTree(BlendTree newTree, BlendTree oldTree)
        {
            // Create a child tree in the destination parent, this seems to be the only way to correctly 
            // add a child tree as opposed to AddChild(motion)
            BlendTree pastedTree = newTree is null ? new BlendTree() : newTree.CreateBlendTreeChild(newTree.maxThreshold);
            pastedTree.name = oldTree.name;
            pastedTree.blendType = oldTree.blendType;
            pastedTree.blendParameter = oldTree.blendParameter;
            pastedTree.blendParameterY = oldTree.blendParameterY;
            pastedTree.minThreshold = oldTree.minThreshold;
            pastedTree.maxThreshold = oldTree.maxThreshold;
            pastedTree.useAutomaticThresholds = oldTree.useAutomaticThresholds;

            // Recursively duplicate the tree structure
            // Motions can be directly added as references while trees must be recursively to avoid accidental sharing
            foreach (var child in oldTree.children)
            {
                if (child.motion is BlendTree tree)
                {
                    var childTree = CloneBlendTree(pastedTree, tree);
                    // need to save the blend tree into the animator
                    childTree.hideFlags = HideFlags.HideInHierarchy;
                    AssetDatabase.AddObjectToAsset(childTree, _assetPath);
                }
                else
                {
                    pastedTree.AddChild(child.motion);
                }
            }

            return pastedTree;
        }

        private static void CloneBehaviourParameters(StateMachineBehaviour old, StateMachineBehaviour n)
        {
            if (old.GetType() != n.GetType())
            {
                throw new ArgumentException("2 state machine behaviours that should be of the same type are not.");
            }
            switch (n)
            {
                case VRCAnimatorLayerControl l:
                    {
                        var o = old as VRCAnimatorLayerControl;
                        l.ApplySettings = o.ApplySettings;
                        l.blendDuration = o.blendDuration;
                        l.debugString = o.debugString;
                        l.goalWeight = o.goalWeight;
                        l.layer = o.layer;
                        l.playable = o.playable;
                        break;
                    }
                case VRCAnimatorLocomotionControl l:
                    {
                        var o = old as VRCAnimatorLocomotionControl;
                        l.ApplySettings = o.ApplySettings;
                        l.debugString = o.debugString;
                        l.disableLocomotion = o.disableLocomotion;
                        break;
                    }
                case VRCAnimatorTemporaryPoseSpace l:
                    {
                        var o = old as VRCAnimatorTemporaryPoseSpace;
                        l.ApplySettings = o.ApplySettings;
                        l.debugString = o.debugString;
                        l.delayTime = o.delayTime;
                        l.enterPoseSpace = o.enterPoseSpace;
                        l.fixedDelay = o.fixedDelay;
                        break;
                    }
                case VRCAnimatorTrackingControl l:
                    {
                        var o = old as VRCAnimatorTrackingControl;
                        l.ApplySettings = o.ApplySettings;
                        l.debugString = o.debugString;
                        l.trackingEyes = o.trackingEyes;
                        l.trackingHead = o.trackingHead;
                        l.trackingHip = o.trackingHip;
                        l.trackingLeftFingers = o.trackingLeftFingers;
                        l.trackingLeftFoot = o.trackingLeftFoot;
                        l.trackingLeftHand = o.trackingLeftHand;
                        l.trackingMouth = o.trackingMouth;
                        l.trackingRightFingers = o.trackingRightFingers;
                        l.trackingRightFoot = o.trackingRightFoot;
                        l.trackingRightHand = o.trackingRightHand;
                        break;
                    }
                case VRCAvatarParameterDriver l:
                    {
                        var d = old as VRCAvatarParameterDriver;
                        l.ApplySettings = d.ApplySettings;
                        l.debugString = d.debugString;
                        l.localOnly = d.localOnly;
                        l.parameters = d.parameters.Select(p =>
                        {
                            string name = _parametersNewName.ContainsKey(p.name) ? _parametersNewName[p.name] : p.name;
                            return new VRC_AvatarParameterDriver.Parameter { name = name, value = p.value, chance = p.chance, valueMin = p.valueMin, valueMax = p.valueMax, type = p.type };
                        }).ToList();
                        break;
                    }
                case VRCPlayableLayerControl l:
                    {
                        var o = old as VRCPlayableLayerControl;
                        l.ApplySettings = o.ApplySettings;
                        l.blendDuration = o.blendDuration;
                        l.debugString = o.debugString;
                        l.goalWeight = o.goalWeight;
                        l.layer = o.layer;
                        l.outputParamHash = o.outputParamHash;
                        break;
                    }
            }
        }

        private static void CloneTransitions(AnimatorStateMachine old, AnimatorStateMachine n)
        {
            AnimatorState[] oldStates = old.states.Select(x => x.state).ToArray();
            AnimatorState[] newStates = n.states.Select(x => x.state).ToArray();
            AnimatorStateMachine[] oldStateMachines = old.stateMachines.Select(x => x.stateMachine).ToArray();
            AnimatorStateMachine[] newStateMachines = n.stateMachines.Select(x => x.stateMachine).ToArray();
            // Generate state transitions
            for (int i = 0; i < oldStates.Length; i++)
            {
                foreach (var transition in oldStates[i].transitions)
                {
                    AnimatorStateTransition newTransition = null;
                    if (transition.isExit)
                    {
                        newTransition = newStates[i].AddExitTransition();
                    }
                    else if (transition.destinationState != null)
                    {
                        var dstState = Array.Find(newStates, x => x.name == transition.destinationState.name);
                        if (dstState != null)
                        {
                            newTransition = newStates[i].AddTransition(dstState);
                        }
                    }
                    else if (transition.destinationStateMachine != null)
                    {
                        var dstState = Array.Find(newStateMachines, x => x.name == transition.destinationStateMachine.name);
                        if (dstState != null)
                        {
                            newTransition = newStates[i].AddTransition(dstState);
                        }
                    }

                    if (newTransition != null)
                    {
                        ApplyTransitionSettings(transition, newTransition);
                    }
                }
            }

            // Generate AnyState transitiosn
            foreach (var transition in old.anyStateTransitions)
            {
                AnimatorStateTransition newTransition = null;
                if (transition.destinationState != null)
                {
                    var dstState = Array.Find(newStates, x => x.name == transition.destinationState.name);
                    if (dstState != null)
                    {
                        newTransition = n.AddAnyStateTransition(dstState);
                    }
                }
                else if (transition.destinationStateMachine != null)
                {
                    var dstState = Array.Find(newStateMachines, x => x.name == transition.destinationStateMachine.name);
                    if (dstState != null)
                    {
                        newTransition = n.AddAnyStateTransition(dstState);
                    }
                }
                if (newTransition != null)
                {
                    ApplyTransitionSettings(transition, newTransition);
                }
            }

            // Generate EntryState transitiosn
            foreach (var transition in old.entryTransitions)
            {
                AnimatorTransition newTransition = null;
                if (transition.destinationState != null)
                {
                    var dstState = Array.Find(newStates, x => x.name == transition.destinationState.name);
                    if (dstState != null)
                    {
                        newTransition = n.AddEntryTransition(dstState);
                    }
                }
                else if (transition.destinationStateMachine != null)
                {
                    var dstState = Array.Find(newStateMachines, x => x.name == transition.destinationStateMachine.name);
                    if (dstState != null)
                    {
                        newTransition = n.AddEntryTransition(dstState);
                    }
                }
                if (newTransition != null)
                {
                    ApplyTransitionSettings(transition, newTransition);
                }
            }

            for (int i = 0; i < oldStateMachines.Length; i++)
            {
                CloneTransitions(oldStateMachines[i], newStateMachines[i]);
            }
        }

        private static void ApplyTransitionSettings(AnimatorStateTransition transition, AnimatorStateTransition newTransition)
        {
            newTransition.canTransitionToSelf = transition.canTransitionToSelf;
            newTransition.duration = transition.duration;
            newTransition.exitTime = transition.exitTime;
            newTransition.hasExitTime = transition.hasExitTime;
            newTransition.hasFixedDuration = transition.hasFixedDuration;
            newTransition.hideFlags = transition.hideFlags;
            newTransition.isExit = transition.isExit;
            newTransition.mute = transition.mute;
            newTransition.name = transition.name;
            newTransition.offset = transition.offset;
            newTransition.interruptionSource = transition.interruptionSource;
            newTransition.orderedInterruption = transition.orderedInterruption;
            newTransition.solo = transition.solo;
            foreach (var contition in transition.conditions)
            {
                string conditionName = _parametersNewName.ContainsKey(contition.parameter) ? _parametersNewName[contition.parameter] : contition.parameter;
                newTransition.AddCondition(contition.mode, contition.threshold, conditionName);
            }
        }

        private static void ApplyTransitionSettings(AnimatorTransition transition, AnimatorTransition newTransition)
        {
            newTransition.hideFlags = transition.hideFlags;
            newTransition.isExit = transition.isExit;
            newTransition.mute = transition.mute;
            newTransition.name = transition.name;
            newTransition.solo = transition.solo;
            foreach (var contition in transition.conditions)
            {
                newTransition.AddCondition(contition.mode, contition.threshold, contition.parameter);
            }
        }
    }
}

#endif