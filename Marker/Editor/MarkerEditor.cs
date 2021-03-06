// Marker and script by ksivl / VRLabs 3.0 Assets - https://vrlabs.dev
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

namespace MarkerSystem
{

	[CustomEditor(typeof(Marker))]
	class MarkerEditor : Editor
	{

		public VRCAvatarDescriptor descriptor;
		public Animator avatar;
		public System.Collections.Generic.List<string> warnings = new System.Collections.Generic.List<string>();
		public int bitCount;

		public bool leftHanded, writeDefaults, brushSize, eraserSize, localSpace;
		public int localSpaceFullBody;

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
				localSpace = EditorGUILayout.ToggleLeft(new GUIContent("Customize Local Space", "Check this if you want to attach your drawings to various locations on your body (recommended)! If unchecked, you can only attach your drawing to your player capsule."), localSpace);

				if (localSpace)
				{
					GUIContent[] layoutOptions = { new GUIContent("Half-Body (Hips, Chest, Head, Hands)", "You can attach the drawing to your hips, chest, head, or either hand."), new GUIContent("Full-Body (Half-Body Plus Feet)", "You can also attach the drawing to your feet! (This will work without full-body tracking; the drawing will just follow the automatic footstep IK of VRChat)") };
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
					GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
					{
						fontStyle = FontStyle.Bold
					};
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
				if (GUILayout.Button(new GUIContent("Adjust MarkerTarget", "Move and rotate MarkerTarget so it's positioned at the tip of your index finger.")))
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

				GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
				{
					fontStyle = FontStyle.Bold
				};
				if (GUILayout.Button("Finish Setup", buttonStyle))
				{
					Debug.Log("Finished, destroying Marker script!");
					DestroyImmediate((Marker)target);
				}
			}

			((Marker)target).leftHanded = leftHanded;
			((Marker)target).writeDefaults = writeDefaults;
			((Marker)target).brushSize = brushSize;
			((Marker)target).eraserSize = eraserSize;
			((Marker)target).localSpace = localSpace;
			((Marker)target).localSpaceFullBody = localSpaceFullBody;
		}

		public void Generate()
		{
			// Unique folder and file name setup
			string directory = "Assets/ksivl/Marker/Generated/" + DateTime.Now.ToString("MM.dd HH.mm.ss") + "/";
			Directory.CreateDirectory(directory);

			// Install FX layer, parameters, and menu

			// FX layer 
			if (leftHanded)
			{
				AssetDatabase.CopyAsset("Assets/ksivl/Marker/Resources/FX_Left.controller", directory + "FXtemp.controller");
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/ksivl/Marker/Resources/FX_Right.controller", directory + "FXtemp.controller");
			}
			AnimatorController FX = AssetDatabase.LoadAssetAtPath(directory + "FXtemp.controller", typeof(AnimatorController)) as AnimatorController;

			if (writeDefaults)
			{
				AV3ManagerFunctions.SetWriteDefaults(FX, true);
			}

			if (!brushSize)
			{
				for (int i = 0; i < FX.layers.Length; i++)
				{
					if (FX.layers[i].name.Equals("M_Size"))
					{
						FX.RemoveLayer(i);
						break;
					}
				}
			}

			if (!eraserSize)
			{
				for (int i = 0; i < FX.layers.Length; i++)
				{
					if (FX.layers[i].name.Equals("M_EraserSize"))
					{
						FX.RemoveLayer(i);
						break;
					}
				}
			}

			if (!localSpace)
			{
				for (int i = 0; i < FX.layers.Length; i++)
				{
					if (FX.layers[i].name.Equals("M_Space"))
					{
						FX.RemoveLayer(i);
						break;
					}
				}
			}
			else
			{
				for (int i = 0; i < FX.layers.Length; i++)
				{
					if (FX.layers[i].name.Equals("M_SpaceSimple"))
					{
						FX.RemoveLayer(i);
						break;
					}
				}
			}
			
			if (!brushSize)
			{
				for (int i = 0; i < FX.parameters.Length; i++)
				{
					if (FX.parameters[i].name.Equals("M_Size"))
					{
						FX.RemoveParameter(i);
						break;
					}
				}
			}

			if (!eraserSize)
			{
				for (int i = 0; i < FX.parameters.Length; i++)
				{
					if (FX.parameters[i].name.Equals("M_EraserSize"))
					{
						FX.RemoveParameter(i);
						break;
					}
				}
			}

			if (!localSpace)
			{
				for (int i = 0; i < FX.parameters.Length; i++)
				{
					if (FX.parameters[i].name.Equals("M_Space"))
					{
						FX.RemoveParameter(i);
						break;
					}
				}
			} 
			else
			{
				for (int i = 0; i < FX.parameters.Length; i++)
				{
					if (FX.parameters[i].name.Equals("M_SpaceSimple"))
					{
						FX.RemoveParameter(i);
						break;
					}
				}
			}

			EditorUtility.SetDirty(FX);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			AV3ManagerFunctions.MergeToLayer(descriptor, FX, AV3ManagerFunctions.PlayableLayer.FX, directory);

			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(FX)); // remove modified temporary FX layer

			// Parameters

			VRCExpressionParameters.Parameter p_marker = new VRCExpressionParameters.Parameter
			{
				name = "M_Marker",
				valueType = VRCExpressionParameters.ValueType.Bool,
				saved = false
			};
			VRCExpressionParameters.Parameter p_eraser = new VRCExpressionParameters.Parameter
			{
				name = "M_Eraser",
				valueType = VRCExpressionParameters.ValueType.Bool,
				saved = false
			};
			VRCExpressionParameters.Parameter p_clear = new VRCExpressionParameters.Parameter
			{
			    name = "M_Clear",
				valueType = VRCExpressionParameters.ValueType.Bool,
				saved = false
			};
			VRCExpressionParameters.Parameter p_color = new VRCExpressionParameters.Parameter
			{
				name = "M_Color",
				valueType = VRCExpressionParameters.ValueType.Float,
				saved = false
			};
			AV3ManagerFunctions.AddParameter(descriptor, p_marker, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_eraser, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_clear, directory);
			AV3ManagerFunctions.AddParameter(descriptor, p_color, directory);

			if (localSpace)
			{
				VRCExpressionParameters.Parameter p_localSpace = new VRCExpressionParameters.Parameter
				{
					name = "M_Space",
					valueType = VRCExpressionParameters.ValueType.Int,
					saved = false
				};
				AV3ManagerFunctions.AddParameter(descriptor, p_localSpace, directory);

			}
			else
			{
				VRCExpressionParameters.Parameter p_localSpaceSimple = new VRCExpressionParameters.Parameter
				{
					name = "M_SpaceSimple",
					valueType = VRCExpressionParameters.ValueType.Bool,
					saved = false
				};
				AV3ManagerFunctions.AddParameter(descriptor, p_localSpaceSimple, directory);
			}

			if (brushSize)
			{
				VRCExpressionParameters.Parameter p_size = new VRCExpressionParameters.Parameter
				{
					name = "M_Size",
					valueType = VRCExpressionParameters.ValueType.Float,
					saved = false
				};
				AV3ManagerFunctions.AddParameter(descriptor, p_size, directory);
			}
			if (eraserSize)
			{
				VRCExpressionParameters.Parameter p_eraserSize = new VRCExpressionParameters.Parameter
				{
					name = "M_EraserSize",
					valueType = VRCExpressionParameters.ValueType.Float,
					saved = false
				};
				AV3ManagerFunctions.AddParameter(descriptor, p_eraserSize, directory);
			}

			// handle menu instancing
			AssetDatabase.CopyAsset("Assets/ksivl/Marker/Resources/Menu - Marker.asset", directory + "Menu - Marker.asset");
			VRCExpressionsMenu markerMenu = AssetDatabase.LoadAssetAtPath(directory + "Menu - Marker.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;

			if (!localSpace)
			{
				VRCExpressionsMenu.Control.Parameter p_spaceSimple = new VRCExpressionsMenu.Control.Parameter
				{
					name = "M_SpaceSimple"
				};
				markerMenu.controls[6].type = VRCExpressionsMenu.Control.ControlType.Toggle;
				markerMenu.controls[6].parameter = p_spaceSimple;
			}
			else
			{
				AssetDatabase.CopyAsset("Assets/ksivl/Marker/Resources/Menu - Marker Space.asset", directory + "Menu - Marker Space.asset");
				VRCExpressionsMenu subMenu = AssetDatabase.LoadAssetAtPath(directory + "Menu - Marker Space.asset", typeof(VRCExpressionsMenu)) as VRCExpressionsMenu;

				if (localSpaceFullBody == 0)
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
				for (int i = 0; i < 8; i++)
				{
					if (markerMenu.controls[i].name.Equals("Brush Size"))
					{
						markerMenu.controls.RemoveAt(i);
						break;
					}
				}
			}

			if (!eraserSize)
			{
				for (int i = 0; i < 8; i++)
				{
					if (markerMenu.controls[i].name.Equals("Eraser Size"))
					{
						markerMenu.controls.RemoveAt(i);
						break;
					}
				}
			}

			EditorUtility.SetDirty(markerMenu);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			AV3ManagerFunctions.AddSubMenu(descriptor, markerMenu, "Marker System", directory);

			// Setup in scene

			GameObject marker = PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/ksivl/Marker/Resources/Marker.prefab", typeof(GameObject))) as GameObject;
			PrefabUtility.UnpackPrefabInstance(marker, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
			marker.transform.SetParent(avatar.transform);

			Transform targets = marker.transform.Find("Targets");
		    Transform markerTarget = targets.Find("MarkerTarget");
			Transform eraser = marker.transform.Find("Eraser");
			
			// prefer the end bone of the index finger if it exists
			if (leftHanded)
			{
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

			if (localSpace)
			{
				targets.Find("LocalSpace2").SetParent(avatar.GetBoneTransform(HumanBodyBones.Hips), false);
				targets.Find("LocalSpace3").SetParent(avatar.GetBoneTransform(HumanBodyBones.Chest), false);

				Transform headRef = targets.Find("HeadRef"); // special head setup b/c local shrink
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Head), false); // get head position but be parented to neck
				headRef.SetParent(avatar.GetBoneTransform(HumanBodyBones.Neck), true);
				ConstraintSource headConstraintSrc = new ConstraintSource
				{
					sourceTransform = avatar.GetBoneTransform(HumanBodyBones.Head),
					weight = 1f
				};
				headRef.GetComponent<RotationConstraint>().SetSource(0, headConstraintSrc);

				targets.Find("LocalSpace5").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftHand), false);
				targets.Find("LocalSpace6").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightHand), false);

				if (localSpaceFullBody == 1)
				{
					targets.Find("LocalSpace7").SetParent(avatar.GetBoneTransform(HumanBodyBones.LeftFoot), false);
					targets.Find("LocalSpace8").SetParent(avatar.GetBoneTransform(HumanBodyBones.RightFoot), false);
				}
			} 
			targets.Find("LocalSpace1").SetParent(avatar.transform);
			//DestroyImmediate(targets); // remove the unnecessary "Targets" container GameObject when finished

			if (!eraserSize) // set the eraser size to an average amount
			{
				eraser.transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
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
				if ((avatar.GetBoneTransform(HumanBodyBones.LeftIndexDistal).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightIndexDistal).gameObject == null))
				{
					warnings.Add("Your humanoid rig's left index distal and/or right index distal is unmapped!");
				}
				if (localSpace)
				{
					if ((avatar.GetBoneTransform(HumanBodyBones.Hips).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Chest).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Head).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.LeftHand).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightHand).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.Neck).gameObject == null))
					{
						warnings.Add("Your humanoid rig's wrists, hips, chest, neck, and/or head is unmapped!");
					}
					if (localSpaceFullBody == 1)
					{
						if ((avatar.GetBoneTransform(HumanBodyBones.LeftFoot).gameObject == null) || (avatar.GetBoneTransform(HumanBodyBones.RightFoot).gameObject == null))
						{
							warnings.Add("Your humanoid rig's left foot and/or right foot is unmapped!");
						}
					}
				}
			}

		}
		private int GetBitCount()
		{
			// M_Marker, M_Clear, and M_Eraser are bools(1+1+1); M_Color is a float(+8). always included
			bitCount = 11;

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

	}
}
#endif