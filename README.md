<div align="center">
  <h1>VRChat Avatars 3.0 Assets</h1>
  <p>
     Assets and prefabs made for VRChat Avatars 3.0 system.
  </p>

  <a href="https://github.com/VRLabs/VRChat-Avatars-3.0/releases/latest">
    <img src="https://img.shields.io/github/v/release/VRLabs/VRChat-Avatars-3.0.svg?style=flat-square">
  </a>
  <br />
  <br />
</div>

# Downloads

If you need help, our support channel is on [Discord](https://discord.gg/THCRsJc).

Testing in Unity may require the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).

## [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)

 A tool for managing playable layers and parameters for Avatars 3.0.

## [Damping Constraints](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/DampingConstraints.unitypackage)

Constraints with damping effects.

## [Follower](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Follower.unitypackage)

A world space follower, driven by constraints.

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> The Follower.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Follower/FollowerTarget. Move this object out of the Follower hierarchy. Position the FollowerTarget where you want.
> 
> Follower/Container is where you place your objects that you want to follow.
> 
> To change the speed of the follower, you can edit the Speed.anim clip inside the Animations folder.

</details>

## [Grab FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/GrabFX.unitypackage)

Handle an avatar object with touch.

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> "GrabFX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
>  
> The Grab FX.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Grab FX/GrabTarget. GrabTarget is what you use for touching. Move it to an appropriate spot in your hierarchy.
> 
> Grab FX/ColliderTarget is a box collider that represents the area you need to touch. Move it to an appropriate spot in your hierarchy. Scale it as needed.
>
> Grab FX/Container will be weighted between GrabTarget and ColliderTarget as they touch. Replace the Cube under the Container with your own objects, and fix transforms as needed.

</details>

## [Jiggle FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/JiggleFX.unitypackage)

Play an effect when the target object is jiggled or shaken.

## [Light Slash](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/LightSlash.unitypackage)

Perform consecutive slashes with motion.

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> "LightSlashFX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
> 
> The Light Slash.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Light Slash/Targets. Move this object under your prop hierarchy, then reset it's position and rotation.
> 
> Targets/MotionTarget is for motion detection. The detection direction is X-, opposite of the red arrow.  
> 
> Targets/EffectTarget is where the slash effect will appear. Position and angle this transform until you are happy with where the effect appears.
> 
> If you need to adjust difficulty of the motion, adjust the bottom constraint source on Light Slash/Collider. The default is .2, and .1 should be very hard to slash.
> 
> The system is disabled when you animate off the Light Slash/Collider object.

</details>

## [Marker](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Marker.unitypackage)

A pen for drawing.

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> The Marker.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Marker/DrawingTarget. Move this object under your drawing wrist bone, then adjust it's position and rotation.
> 
> Review the markerLeft and markerRight layers that were merged into your FX controller. Unmute the transition for the gestures you would like for drawing and erasing.

</details>

## [Particle Driver](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleDriver.unitypackage)

A method for animating on particle death.

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> "FX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
>  
> The Particle Driver.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Particle Driver/ᴛʀɪɢɢᴇʀ. ᴛʀɪɢɢᴇʀ is a particle, that when killed, will drive a parameter change within your playable layers.
>
> By default, ᴛʀɪɢɢᴇʀ drives the local parameter, "ParticleDeath".
> 
> The default FX controller is set up so "ParticleDeath" changes will drive the synced parameter, "FX".
>
> Animate what you want. This is a blank template.
> 
> ᴛʀɪɢɢᴇʀ is constrained to ParticleTarget.

</details>

## [Proximity FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ProximityFX.unitypackage)

Player-local proximity effects.

## [Particle Shader](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleShader.unitypackage)

A shader for particle effects.
 
## [Spring Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/SpringConstraint.unitypackage)

A constraint with spring behavior.

<details>
  <summary>Install notes</summary>

> Replace the Cube under Spring Constraint/Container with your own objects.
>
> The Container will follow Spring Constraint/SpringTarget.
>
> To change the characteristics of the spring, change the position constraint values on the Spring Constraint/Motion object. 
> 
> Sources > SpringTarget (default 1.1) controls the strength of the spring. Higher values make it harder to stretch the spring. Min: 1, Max: 2
>
> Sources > Motion (default 4) dampens acceleration, the higher the value the slower Spring Constraint/Container accelerates.

</details>

## [World Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldConstraint.unitypackage)

A world fixed object, held in place with a constraint.

<details>
  <summary>Install notes</summary>

> The world constraining method itself is 1 constraint and simple.
>
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> "WorldFX" is a synced parameter, so click the checkbox within the tool to add it to your avatar's parameter asset.
>
> The World Constraint.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate World Constraint/ResetTarget. Move this object out of the prefab to anywhere else on your avatar.
>
> World Constraint/Container will start at and reset to ResetTarget.
>
> Replace the Cube under World Constraint/Container with your own objects.
>
> Review the handleWorldFX layer that was merged into your FX controller. Change "WorldFX" parameter to cause transitions with this layer.
>
> Edit the handleWorldFX.anim clip to customize.

</details>
