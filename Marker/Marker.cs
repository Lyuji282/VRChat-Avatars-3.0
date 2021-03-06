// Marker and script by ksivl / VRLabs 3.0 Assets - https://vrlabs.dev
#if UNITY_EDITOR

using UnityEngine;

namespace MarkerSystem
{
	public class Marker : MonoBehaviour // Data storage
	{
		public bool leftHanded, writeDefaults, eraserSize;
		public bool brushSize = true, localSpace = true; // best (default) options
		public int localSpaceFullBody;

		public Transform markerTarget;
		public bool finished;
	}
}
#endif