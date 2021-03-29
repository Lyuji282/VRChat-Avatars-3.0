// Marker by ksivl / VRLabs 3.0 Assets https://github.com/VRLabs/VRChat-Avatars-3.0
#if UNITY_EDITOR

using UnityEngine;

namespace MarkerSystem
{
	[ExecuteAlways]
	public class Marker : MonoBehaviour
	{	// data storage
		public bool leftHanded, writeDefaults, eraserSize, useIndexFinger;
		public bool brushSize = true, localSpace = true;
		public int localSpaceFullBody;
		public int gestureToDraw = 3;

		public Transform markerTarget, markerModel, system;
		public bool finished;

		public void Update()
		{
			if (finished) // constantly uniformly scale Draw and Eraser with MarkerTarget
			{
				UnityEngine.Vector3 scale = system.localScale;
				scale.y = scale.x;
				scale.z = scale.x;
				system.localScale = scale;
				// also scale Draw's triggers module radius scale
				Transform draw = system.GetChild(0);
				ParticleSystem.TriggerModule triggerModule = draw.GetComponent<ParticleSystem>().trigger;
				triggerModule.radiusScale = system.localScale.x * 0.6f; // bit more than half is OK
			}
		}
	}
}
#endif