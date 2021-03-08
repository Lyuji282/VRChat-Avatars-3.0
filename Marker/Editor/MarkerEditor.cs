// Marker by ksivl / VRLabs 3.0 Assets https://vrlabs.dev
#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;
using Vector3 = UnityEngine.Vector3;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRLabs.AV3Manager;
using System.Linq;
using Boo.Lang;
using System.CodeDom;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;

namespace MarkerSystem
{

	[CustomEditor(typeof(Marker))]
	class MarkerEditor : Editor
	{

		public VRCAvatarDescriptor descriptor;
		public Animator avatar;
		public System.Collections.Generic.List<string> warnings = new System.Collections.Generic.List<string>();
		public int bitCount;

		public bool leftHanded, writeDefaults, useIndexFinger, brushSize, eraserSize, localSpace;
		public int localSpaceFullBody;

		private readonly string path_defaultGesture = "Assets/VRCSDK/Examples3/Animation/Controllers/vrc_AvatarV3HandsLayer.controller";
		private readonly string path_defaultMaskL = "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Left.mask";
		private readonly string path_defaultMaskR = "Assets/VRCSDK/Examples3/Animation/Masks/vrc_Hand Right.mask";

		public void Reset()
		{
			if (((Marker)target).gameObject.GetComponent<VRCAvatarDescriptor>() != null)
			{
				descriptor = ((Marker)target).gameObject.GetComponent<VRCAvatarDescriptor>();
			}

			leftHanded = ((Marker)target).leftHanded;
			writeDefaults = ((Marker)target).writeDefaults;
			brushSize = ((Marker)target).brushSize;
			eraserSize = ((Marker)target).eraserSize;
			localSpace = ((Marker)target).localSpace;
			localSpaceFullBody = ((Marker)target).localSpaceFullBody;
			useIndexFinger = ((Marker)target).useIndexFinger;
		}


		public override void OnInspectorGUI()
		{
			if (EditorApplication.isPlaying)
			{
				GUILayout.Space(10);
				EditorGUILayout.LabelField("Please exit Play Mode to use this script.");
				return;
			}

			GUILayout.Space(8);

			descriptor = (VRCAvatarDescriptor)EditorGUILayout.ObjectField("Avatar", descriptor, typeof(VRCAvatarDescriptor), true);

			avatar = descriptor.gameObject.GetComponent<Animator>();

			GUILayout.Space(8);

			if (((Marker)target).finished == false)
			{

				leftHanded = EditorGUILayout.ToggleLeft("Left-handed", leftHanded);
				writeDefaults = EditorGUILayout.ToggleLeft(new GUIContent("Write Defaults", "Check this if you are animating your avatar with Write Defaults on. Otherwise, leave unchecked."), writeDefaults);

				GUILayout.Space(8);

				brushSize = EditorGUILayout.ToggleLeft("Adjustable brush size", brushSize);
				eraserSize = EditorGUILayout.ToggleLeft("Adjustable eraser size", eraserSize);
				useIndexFinger = EditorGUILayout.ToggleLeft(new GUIContent("Use index finger to draw", "By default, you draw with a shiny pen. Check this to draw with your index finger instead."), useIndexFinger);
				localSpace = EditorGUILayout.ToggleLeft(new GUIContent("Enable local space", "Check this to be able to attach your drawings to various locations on your body (recommended)! If unchecked, you can only attach your drawing to your player capsule."), localSpace);

				if (localSpace)
				{
					GUIContent[] layoutOptions = { new GUIContent("Half-Body (Hips, Chest, Head, Hands)", "You can attach the drawing to your hips, chest, head, or either hand."), new GUIContent("Full-Body (Half-Body Plus Feet)", "You can also attach the drawing to your feet! (For half-body users, the drawing will follow VRChat's auto-footstep IK)") };
					GUILayout.BeginVertical("Box");
					localSpaceFullBody = GUILayout.SelectionGrid(localSpaceFullBody, layoutOptions, 1);
					GUILayout.EndVertical();
				}

				GUILayout.Space(8);

				GetBitCount();
				EditorGUILayout.LabelField("Current bit count: " + bitCount);

				CheckRequirements();

				if (warnings.Count == 0)
				{
					GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold };
					if (GUILayout.Button("Generate Marker", buttonStyle))
					{
						Debug.Log("Generating Marker...");
						Generate();
					}
				}
				else
				{
					GUIStyle warningStyle = new GUIStyle("box");
					warningStyle.normal.textColor = new GUIStyle("label").normal.textColor;

					for (int i = 0; i < warnings.Count; i++)
					{
						GUILayout.Box(warnings[i], warningStyle);
					}
					GUI.enabled = false;
					GUILayout.Button("Generate Marker");
					GUI.enabled = true;
				}
			}
			else if (((Marker)target).finished == true)
			{
				if (GUILayout.Button(new GUIContent("Adjust MarkerTarget transform", "Move, rotate, or scale MarkerTarget so it's either in your hand (marker model) or at the tip of your index finger (no marker model), if needed.")))
				{
					if (((Marker)target).markerTarget.gameObject == null)
					{
						Debug.LogError("Can't find MarkerTarget! It may have been moved or deleted.");
					} 
					else
					{
						Selection.activeGameObject = ((Marker)target).markerTarget.gameObject;
					}
				}

				GUILayout.Space(8);

				GUIStyle buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold };
				if (GUILayout.Button("Finish Setup", buttonStyle))
				{
					if (!((Marker)target).useIndexFinger) // turn off marker model by default
					{
						((Marker)target).markerModel.GetComponent<MeshRenderer>().enabled = false;
					}
					Debug.Log("Finished, destroying Marker script!");
					DestroyImmediate((Marker)target);
					// END SCRIPT
				}
			}

			((Marker)target).leftHanded = leftHanded;
			((Marker)target).writeDefaults = writeDefaults;
			((Marker)target).brushSize = brushSize;
			((Marker)target).eraserSize = eraserSize;
			((Marker)target).localSpace = localSpace;
			((Marker)target).localSpaceFullBody = localSpaceFullBody;
			((Marker)target).useIndexFinger = useIndexFinger;
		}

		public void Generate()
		{
			// Unique directory setup
			string directory = "Assets/VRLabs/Marker/Generated/" + DateTime.Now.ToString("MM.dd HH.mm.ss") + "/";
			Directory.CreateDirectory(directory);

			// Install layers, parameters, and menu before prefab setup
			// FX layer
			if (leftHanded)
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_FX (L).controller", directory + "FXtemp.controller");
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_FX (R).controller", directory + "FXtemp.controller");
			}
			AnimatorController FX = AssetDatabase.LoadAssetAtPath(directory + "FXtemp.controller", typeof(AnimatorController)) as AnimatorController;

			// remove controller layers before merging to avatar, corresponding to setup
			if (!brushSize)
			{
				RemoveLayerAndParameter(FX, "M_Size");
			}

			if (!eraserSize)
			{
				RemoveLayerAndParameter(FX, "M_EraserSize");
			}

			if (!localSpace)
			{
				RemoveLayerAndParameter(FX, "M_Space");
			}
			else
			{
				RemoveLayerAndParameter(FX, "M_SpaceSimple");
			}

			if (writeDefaults)
			{
				AV3ManagerFunctions.SetWriteDefaults(FX);
			}

			if (!useIndexFinger) // rocknroll to draw if using model, not fingerpoint
			{
				ChangeGestureCondition(FX, 0, 3, 5);
			}

			EditorUtility.SetDirty(FX);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			AV3ManagerFunctions.MergeToLayer(descriptor, FX, AV3ManagerFunctions.PlayableLayer.FX, directory);
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(FX)); // remove modified temporary FX layer

			// Gesture layer

			AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Gesture.controller", directory + "gestureTemp.controller"); // to modify
			AnimatorController gesture = AssetDatabase.LoadAssetAtPath(directory + "gestureTemp.controller", typeof(AnimatorController)) as AnimatorController;

			if (descriptor.baseAnimationLayers[2].isDefault == true || descriptor.baseAnimationLayers[2].animatorController == null)
			{
				AssetDatabase.CopyAsset(path_defaultGesture, directory + "Gesture.controller");
				AnimatorController gestureOriginal = AssetDatabase.LoadAssetAtPath(directory + "Gesture.controller", typeof(AnimatorController)) as AnimatorController;

				descriptor.customExpressions = true;
				descriptor.baseAnimationLayers[2].isDefault = false;
				descriptor.baseAnimationLayers[2].animatorController = gestureOriginal;

				if (writeDefaults)
				{
					AV3ManagerFunctions.SetWriteDefaults(gestureOriginal);
					EditorUtility.SetDirty(gestureOriginal);
					AssetDatabase.SaveAssets();
					AssetDatabase.Refresh();
				}
			}
			
			int layerRemove = (leftHanded) ? 1 : 0;
			if (useIndexFinger)
			{   // use the other set of hand animations
				gesture.RemoveLayer(layerRemove);
				for (int i = 0; i < 3; i++)
				{
					if (gesture.layers[0].stateMachine.states[i].state.motion.name == "M_Gesture")
					{
						gesture.layers[0].stateMachine.states[i].state.motion = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Animations/Gesture/M_Gesture (Finger).anim", typeof(AnimationClip)) as AnimationClip;
					}
					else if (gesture.layers[0].stateMachine.states[i].state.motion.name == "M_Gesture Draw")
					{
						gesture.layers[0].stateMachine.states[i].state.motion = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Animations/Gesture/M_Gesture Draw (Finger).anim", typeof(AnimationClip)) as AnimationClip;
					}
				}
			}
			else
			{
				gesture.RemoveLayer(layerRemove);
			}

			if (!useIndexFinger) // rocknroll to draw if using model, not fingerpoint
			{
				ChangeGestureCondition(gesture, 0, 3, 5);
			}

			EditorUtility.SetDirty(gesture);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			AV3ManagerFunctions.MergeToLayer(descriptor, gesture, AV3ManagerFunctions.PlayableLayer.Gesture, directory);
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(gesture)); // remove modified temporary gesture layer

			// layer weight control from merged layer may need index set correctly
			AnimatorController avatarGesture = (AnimatorController)descriptor.baseAnimationLayers[2].animatorController;
			for (int i = 0; i < avatarGesture.layers.Length; i++)
			{
				if (avatarGesture.layers[i].name.Contains("M_Gesture") && (i != 3))
				{   // the controls' layer is set to 3 by default (AllParts, LeftHand, RightHand, >>>M_Marker<<<)
					for (int j = 0; j < 3; j++)
					{
						if (avatarGesture.layers[i].stateMachine.states[j].state.behaviours.Length != 0)
						{
							VRCAnimatorLayerControl ctrl = (VRCAnimatorLayerControl)avatarGesture.layers[i].stateMachine.states[j].state.behaviours[0];
							ctrl.layer = i;
						}
					}
				}
			}
			if (writeDefaults)
			{
				AV3ManagerFunctions.SetWriteDefaults(avatarGesture, true);
			}
			EditorUtility.SetDirty(avatarGesture);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			// Parameters
			VRCExpressionParameters.Parameter
			p_marker = new VRCExpressionParameters.Parameter
				{ name = "M_Marker", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_eraser = new VRCExpressionParameters.Parameter
				{ name = "M_Eraser", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_clear = new VRCExpressionParameters.Parameter
				{ name = "M_Clear", valueType = VRCExpressionParameters.ValueType.Bool, saved = false },
			p_color = new VRCExpressionParameters.Parameter
				{ name = "M_Color", valueType = VRCExpressionParameters.ValueType.Float, saved = false };
			AV3ManagerFunctions.AddParameter(descriptor, p_marker, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_eraser, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_clear, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_color, directory);

			if (localSpace)
			{
				VRCExpressionParameters.Parameter p_localSpace = new VRCExpressionParameters.Parameter
					{ name = "M_Space", valueType = VRCExpressionParameters.ValueType.Int, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_localSpace, directory);
			}
			else
			{
				VRCExpressionParameters.Parameter p_localSpaceSimple = new VRCExpressionParameters.Parameter
					{ name = "M_SpaceSimple", valueType = VRCExpressionParameters.ValueType.Bool, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_localSpaceSimple, directory);
			}

			if (brushSize)
			{
				VRCExpressionParameters.Parameter p_size = new VRCExpressionParameters.Parameter
					{ name = "M_Size", valueType = VRCExpressionParameters.ValueType.Float, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_size, directory);
			}
			if (eraserSize)
			{
				VRCExpressionParameters.Parameter p_eraserSize = new VRCExpressionParameters.Parameter
					{ name = "M_EraserSize", valueType = VRCExpressionParameters.ValueType.Float, saved = false };
				AV3ManagerFunctions.AddParameter(descriptor, p_eraserSize, directory);
			}

			// handle menu instancing
			AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Menu.asset", directory + "Marker Menu.asset");
			VRCExpressionsMenu markerMenu = AssetDatabase.LoadAssetAtPath(directory + "Marker Menu.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;
			
			if (!localSpace) // change from submenu to 1 toggle
			{
				VRCExpressionsMenu.Control.Parameter p_spaceSimple = new VRCExpressionsMenu.Control.Parameter 
					{ name = "M_SpaceSimple" };
				markerMenu.controls[6].type = VRCExpressionsMenu.Control.ControlType.Toggle;
				markerMenu.controls[6].parameter = p_spaceSimple;
				EditorUtility.SetDirty(markerMenu);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/VRLabs/Marker/Resources/M_Menu Space.asset", directory + "Marker Space Submenu.asset");
				VRCExpressionsMenu subMenu = AssetDatabase.LoadAssetAtPath(directory + "Marker Space Submenu.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;

				if (localSpaceFullBody == 0) // remove left and right foot controls
				{
					subMenu.controls.RemoveAt(7);
					subMenu.controls.RemoveAt(6);
				}
				markerMenu.controls[6].subMenu = subMenu;
				EditorUtility.SetDirty(subMenu);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}

			if (!brushSize)
			{
				RemoveMenuControl(markerMenu, "Brush Size");
			}

			if (!eraserSize)
			{
				RemoveMenuControl(markerMenu, "Eraser Size");
			}

			EditorUtility.SetDirty(markerMenu);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			AV3ManagerFunctions.AddSubMenu(descriptor, markerMenu, "Marker", directory);

			// icon
			VRCExpressionsMenu.Control markerControl = descriptor.expressionsMenu.controls.FirstOrDefault(x => x.name.Equals("Marker"));
			markerControl.icon = AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Icons/M_Icon_Menu.png", typeof(Texture2D)) as Texture2D;

			// setup in scene
			GameObject marker = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/VRLabs/Marker/Resources/Marker.prefab", typeof(GameObject))) as GameObject;
			PrefabUtility.UnpackPrefabInstance(marker, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
			marker.transform.SetParent(avatar.transform, false);

			Transform targets = marker.transform.Find("Targets");
		    Transform markerTarget = targets.Find("MarkerTarget");
			Transform markerModel = targets.Find("MarkerModel");
			Transform eraser = marker.transform.Find("Eraser");
			Transform eraserTarget = markerModel.GetChild(0);

			if (useIndexFinger) 
			{   // eraserTarget not needed, constrain eraser to markerTarget
				ParentConstraint eraserConstraint = eraser.GetComponent<ParentConstraint>();
				eraserConstraint.SetSource(0, new ConstraintSource { sourceTransform = markerTarget, weight = 1.0f });
				eraser.localPosition = Vector3.zero; // reset offset

				if (leftHanded)
				{   // prefer the end bone of the index finger if it exists
					Transform leftIndexDistal = avatar.GetBoneTransform(HumanBodyBones.LeftIndexDistal);
					if (leftIndexDistal.Find(leftIndexDistal.gameObject.name + "_end") != null)
					{
						markerTarget.SetParent(leftIndexDistal.Find(leftIndexDistal.gameObject.name + "_end"), false);
					}
					else
					{
						markerTarget.SetParent(leftIndexDistal, false);
					}
				}
				else
				{
					Transform rightIndexDistal = avatar.GetBoneTransform(HumanBodyBones.RightIndexDistal);

					if (rightIndexDistal.Find(rightIndexDistal.gameObject.name + "_end") != null)
					{
						markerTarget.SetParent(rightIndexDistal.Find(rightIndexDistal.gameObject.name + "_end"), false);
					}
					else
					{
						markerTarget.SetParent(rightIndexDistal, false);
					}
				}
			}
			else // using model
			{
				markerModel.SetParent(marker.transform); // move it out of Targets hierarchy
				if (leftHanded)
				{   // keep scale
					Transform leftWrist = avatar.GetBoneTransform(HumanBodyBones.LeftHand);
					markerTarget.SetParent(leftWrist, true);
				}
				else
				{
					Transform rightWrist = avatar.GetBoneTransform(HumanBodyBones.RightHand);
					markerTarget.SetParent(rightWrist, true);
				}
				markerTarget.localPosition = Vector3.zero;
				markerTarget.localRotation = Quaternion.Euler(0f, 0f, 0f);
				((Marker)target).markerModel = markerModel; // to turn its mesh renderer off when script is finished
			}

			if (localSpace)
			{
				targets.Find("LocalSpace2").SetParent(avatar.GetBoneTransform(HumanBodyBones.Hips), false);
				targets.Find("LocalSpace3").SetParent(avatar.GetBoneTransform(HumanBodyBones.Chest), false);

				Transform headRef = targets.Find("HeadRef"); // special setup for head bone b/c local shrink
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Head), false); // get head position,
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Neck), true); // but parent to neck
				headRef.GetComponent<RotationConstraint>().SetSource(0, new ConstraintSource
					{ sourceTransform = avatar.GetBoneTransform(HumanBodyBones.Head), weight = 1f} );

				targets.Find("LocalSpace5").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftHand), false);
				targets.Find("LocalSpace6").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightHand), false);

				if (localSpaceFullBody == 1)
				{
					targets.Find("LocalSpace7").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftFoot), false);
					targets.Find("LocalSpace8").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightFoot), false);
				}
			} 
			targets.Find("LocalSpace1").SetParent(avatar.transform);
			DestroyImmediate(targets.gameObject); // remove the "Targets" container object when finished

			// set anything not adjustable to a medium-ish amount
			if (!eraserSize) 
			{
				eraser.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
			}
			if (!brushSize)
			{
				ParticleSystem.MinMaxCurve size = new ParticleSystem.MinMaxCurve(0.024f);
				Transform draw = marker.transform.Find("Draw");
				Transform preview = draw.GetChild(0);
				ParticleSystem.MainModule main = draw.GetComponent<ParticleSystem>().main;
				main.startSize = size;
				main = preview.GetComponent<ParticleSystem>().main;
				main.startSize = size;
			}

			((Marker)target).markerTarget = markerTarget;
			((Marker)target).finished = true;
			Debug.Log("Successfully generated Marker!");
		}
		private void CheckRequirements()
		{
			warnings.Clear();

			if (descriptor == null)
			{
				warnings.Add("There is no avatar descriptor on this GameObject. Please move this script onto your avatar, or create an avatar descriptor here.");
			}
			else
			{
				if (descriptor.expressionParameters != null && descriptor.expressionParameters.CalcTotalCost() > (128 - bitCount))
				{
					warnings.Add("You don't have enough free memory in your avatar's Expression Parameters to generate. You need " + (128 - bitCount) + " or less bits of parameter memory utilized.");
				}
				if (descriptor.expressionsMenu != null)
				{
					if (descriptor.expressionsMenu.controls.Count == 8)
					{
						warnings.Add("Your avatar's topmost menu is full. Please have at least one empty control slot available.");
					}
				}

				if (AssetDatabase.LoadAssetAtPath(path_defaultGesture, typeof(AnimatorController)) as AnimatorController == null)
				{
					warnings.Add("VRCSDK Gesture controller at path '" + path_defaultGesture + "' could not be found. Please place the default Gesture controller there.");
				}
				if (AssetDatabase.LoadAssetAtPath(path_defaultMaskL, typeof(AvatarMask)) as AvatarMask == null)
				{
					warnings.Add("VRCSDK default avatar mask for the left hand at path '" + path_defaultMaskL + "' could not be found. Please place the default left hand mask there.");
				}
				if (AssetDatabase.LoadAssetAtPath(path_defaultMaskR, typeof(AvatarMask)) as AvatarMask == null)
				{
					warnings.Add("VRCSDK default avatar mask for the right hand at path '" + path_defaultMaskR + "' could not be found. Please place the default left hand mask there.");
				}

				if (!descriptor.baseAnimationLayers[2].isDefault) // check gesture layer validity
				{
					if (descriptor.baseAnimationLayers[2].animatorController != null && descriptor.baseAnimationLayers[2].animatorController.name != "")
					{
						AnimatorController gesture = (AnimatorController)descriptor.baseAnimationLayers[2].animatorController;
						if (gesture.layers[0] == null || gesture.layers[0].name == "")
						{
							warnings.Add("Your avatar's gesture layer is empty. Try using a copy of the VRCSDK gesture controller or removing the controller from your avatar descriptor.");
						}
						else if (gesture.layers[0].avatarMask == null || gesture.layers[0].avatarMask.name == "")
						{
							warnings.Add("The first layer of your avatar's gesture layer is missing a mask. Try setting a mask, or using a copy of the VRCSDK gesture controller, or removing the controller from your avatar descriptor.");
						}
					}
				}
			}	

			if (avatar == null)
			{
				warnings.Add("There is no Animator on this avatar. Please add an Animator component on your avatar.");
			}
			else if (avatar.avatar == null)
			{
				warnings.Add("Please add an avatar in this avatar's Animator component.");
			}
			else
			{
				if (!avatar.isHuman)
				{
					warnings.Add("Please use this script on an avatar with a humanoid rig.");
				}

				else
				{
					if (useIndexFinger && ((avatar.GetBoneTransform(HumanBodyBones.LeftIndexDistal).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightIndexDistal).gameObject == null)))
					{
						warnings.Add("Your avatar rig's left and/or right index finger's 3rd bone is unmapped!");
					}
					if (localSpace)
					{
						if ((avatar.GetBoneTransform(HumanBodyBones.Hips).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Chest).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Head).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.LeftHand).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightHand).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Neck).gameObject == null))
						{
							warnings.Add("Your avatar rig's wrists, hips, chest, neck, and/or head is unmapped!");
						}
						if (localSpaceFullBody == 1)
						{
							if ((avatar.GetBoneTransform(HumanBodyBones.LeftFoot).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightFoot).gameObject == null))
							{
								warnings.Add("Your avatar rig's left foot and/or right foot is unmapped!");
							}
						}
					}
				}
			}
		}

		private int GetBitCount()
		{
			bitCount = 11; // M_Marker, M_Clear, and M_Eraser are bools(1+1+1); M_Color is a float(+8). always included
			if (brushSize) // float
			{
				bitCount += 8;
			}
			if (eraserSize) // float
			{
				bitCount += 8;
			}
			if (localSpace) // int
			{
				bitCount += 8;
			}
			else // bool
			{
				bitCount += 1;
			}
			return bitCount;
		}

		private void RemoveLayerAndParameter(AnimatorController controller , string name, bool layerOnly = false)
		{   // helper function: remove layer and parameter of same name
			for (int i = 0; i < controller.layers.Length; i++)
			{
				if (controller.layers[i].name.Equals(name))
				{
					controller.RemoveLayer(i);
					break;
				}
			}
			if (!layerOnly)
			{
				for (int i = 0; i < controller.parameters.Length; i++)
				{
					if (controller.parameters[i].name.Equals(name))
					{
						controller.RemoveParameter(i);
						break;
					}
				}
			}
		}

		private void RemoveMenuControl(VRCExpressionsMenu menu, string name)
		{	// helper function: remove menu control
			for (int i = 0; i < menu.controls.Count; i++)
			{
				if (menu.controls[i].name.Equals(name))
				{
					menu.controls.RemoveAt(i);
					break;
				}
			}
		}

		private void ChangeGestureCondition(AnimatorController controller, int layerToModify, int oldGesture, int newGesture)
		{   // helper function: change gesture condition, in all transitions of 1 layer of controller
			AnimatorStateMachine stateMachine = controller.layers[layerToModify].stateMachine;
			ChildAnimatorState[] states = stateMachine.states;
			List<AnimatorStateTransition> transitions = new List<AnimatorStateTransition>();
			for (int i = 0; i < states.Length; i++)
			{
				transitions.AddRange(states[i].state.transitions);
			}
			AnimatorCondition[] conditions;
			for (int i = 0; i < transitions.Count; i++)
			{
				conditions = transitions[i].conditions;
				for (int j = 0; j < conditions.Length; j++)
				{
					if (conditions[j].threshold == oldGesture)
					{
						AnimatorCondition conditionToRemove = conditions[j];
						transitions[i].RemoveCondition(conditionToRemove);
						transitions[i].AddCondition(conditionToRemove.mode, newGesture, conditionToRemove.parameter);
						break; // in my case, only one condition per transition includes GestureLeft / GestureRight
					}
				}
			}
		}
	}
}
#endif