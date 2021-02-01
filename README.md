<div align="center">
  <h1>
      VRChat Avatars 3.0 Assets. VRChat アバター3.0用アセット
  </h1>
  <p>
     Assets and prefabs made for VRChat Avatars 3.0 system. VRChatのアバター3.0用に作られたアセットやPrefab
  </p>

  <a href="https://github.com/VRLabs/VRChat-Avatars-3.0/releases/latest">
    <img src="https://img.shields.io/github/v/release/VRLabs/VRChat-Avatars-3.0.svg?style=flat-square">
  </a>
  <br />
  <br />
</div>

# Downloads ダウンロード

If you need help, our support channel is on [Discord](https://discord.gg/THCRsJc). 質問等は[Discord](https://discord.gg/THCRsJc)にて専用サポートチャンネルがあります。

Testing in Unity may require the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator). Unity内でテストプレイをする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さんの3.0エミュレーターを必要とする物もあります。

Do not mix old 2.0 and new 3.0 VRLabs folders. Delete your old VRLabs folder. 古い2.0と新しい3.0のVRLabsフォルダーを混在させないでください。古いVRLabsフォルダを削除します。

These assets follow the VRChat animation standard with write defaults turned off. これらのアセットは、書き込みのデフォルトがオフになっているVRChatアニメーション標準に準拠しています。

## [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)

 A tool for managing playable layers and parameters for Avatars 3.0. アバター3.0でプレイするレイヤーやパラメターを管理するツール。

## [Damping Constraints](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/DampingConstraints.unitypackage)

Constraints with damping effects. Damping（制動、減衰）エフェクト付きのConstraint（紐付け）。

<details>
  <summary>Install notes</summary>

> There are constraints for position and rotation. 
> 
> Replace the Cube under Damping Constraint/Container with your own objects.
>
> Damping Constraint/Container will follow Damping Constraint/Target.
>
> Within the Container constraint, the smaller the weight to the Target, the more motion will be dampened.

</details>

<details>
  <summary>導入手順</summary>

> 位置(Position)と回転(Rotation)用のConstraintがあります。
> 
> Damping Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> Damping Constraint/ContainerはDamping Constraint/Targetを追尾します。
>
> ContainerのConstraint内では重さ（weight）が少なければ少ないほどモーションが減衰します。

</details>

## [Follower](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Follower.unitypackage)

A world space follower, driven by constraints. Constraintによってワールドスペースでついてくるfollower。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool. 
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
>
> As a result of breaking changes, a hotfix has been applied to this package so it will continue to work in 3.0.
>
> You must make sure the layers "IsStopping-1" and "IsStopping1" are below your base layer(in layer slots 1 and 2 if base layer is 0), otherwise the hotfix will not work properly.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0の[Manager tool](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)を使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "Follower.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからPrefabごとアバターのベースに追加してください。
> 
> Prefabを開き、Follower/FollowerTargetを探し、そのオブジェクトをFollowerのヒエラルキーから抜いてください。FollowerTargetを任意の場所に移動させてください。
> 
> Follower/Containerには追尾したいオブジェクトを置いてください
> 
> Followerのスピードを変えたい場合はAnimationsのフォルダー内にあるSpeed.animのクリップを編集してください。

</details>

## [Marker](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Marker.unitypackage)

A pen for drawing. お絵描き用のペン。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> The Marker.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Marker/DrawingTarget. Move this object under your drawing wrist bone, then adjust it's position and rotation.
> 
> Review the markerLeft and markerRight layers that were merged into your FX controller. Unmute the transition for the gestures you would like for drawing and erasing.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0の[Manager tool](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)を使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "The Marker.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Marker/DrawingTargetを探してください。このオブジェクトを利き手のwristボーンに入れ子してから位置と回転を調整してください。
> 
> FX Controllerに追加したmarkerLeftとmarkerRightのレイヤーをご確認ください。使用したいお絵描き、消しゴム用のジェスチャーのtransitionをunmuteしてください。

</details>

## [Particle Shader](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleShader.unitypackage)

A shader for particle effects. パーティクルエフェクト用のシェーダー。
 
## [Spring Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/SpringConstraint.unitypackage)

A constraint with spring behavior. バネモーションの入ったConstraint。

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

<details>
  <summary>導入手順</summary>

> Spring Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> ContainerはSpring Constraint/SpringTargetを追尾します。
>
> Springの調整をする場合はSpring Constraint/Motionオブジェクトのposition constraintの数値を編集してください。
> 
> Sources > SpringTarget (デフォルト値 1.1)はバネの強さをコントロールします。数値が高ければ高いほど伸びにくくなります（最小値１、最大値２）
>
> Sources > Motion (デフォルト値 4)は加速を減衰、数値が高ければ高いほどSpring Constraint/Containerの加速がゆっくりになります。

</details>

## [World Constraint](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldConstraint.unitypackage)

A world fixed object, held in place with a constraint. Constraintを使ってオブジェクトをワールド固定。

<details>
  <summary>Install notes</summary>

> The world constraining method itself is 1 constraint and simple. Look at it and profit.
>
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
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
> Review the handleWorldFX layer that was merged into your FX controller. Change "WorldFX" parameter to cause transitions within this layer.

</details>

<details>
  <summary>導入手順</summary>

> 1つのConstraintで完結する比較的シンプルなメソッドです。
>
> ※Unity内でテストプレイする場合は[Lyuma](https://github.com/lyuma/Av3Emulator)さん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0の[Manager tool](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage)を使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "WorldFX"は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。
>
> "World Constraint.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
>
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、World Constraint/ResetTargetを探してください。Prefab外の任意の場所（アバター内）に移動させてください。
>
> World Constraint/ContainerはResetTargetからスタート、リセットします。
>
> World Constraint/ContainerについているCubeを任意のオブジェクトと交換してください。
>
> FX Controllerに追加したhandleWorldFXのレイヤーをご確認ください。このレイヤーでトランジションを使いたい場合は"WorldFX"のパラメターを使ってください。

</details>

## [World Physics](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldPhysics.unitypackage)

Some bandaids to make physics work on avatars. アバターで物理を機能させるためのいくつかの修正。

<details>
  <summary>Install notes</summary>

> This package fixes two problems that break avatar physics in VRChat. First, it destroys colliders in the mirror copy of your avatar to fix local collision. Second, it fixes incorrect movement with rigidbodies in world space.
>
> Testing in Unity requires the [3.0 emulator by Lyuma](https://github.com/lyuma/Av3Emulator).
> 
> Merge the FX controller to your own FX controller and merge the Gesture controller to your own Gesture controller, using the [Avatars 3.0 Manager](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/AV3Manager.unitypackage) tool.
> 
> The World Physics.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> World Physics/Rigidbody is set up for a physics demo. It falls and collides with the world.
>
> If you want to see the demo, move World Physics/RigidbodyTarget outside of the World Physics hierarchy, to the base of the avatar, and raise the height. You can take this in-game or use the emulator for testing.
>
> Look at World Physics/Rigidbody/Collider. There is a particle system component on this object. Copy and paste this particle system onto any object with a physics collider. Every object with this particle system will be deleted in the local mirror.
>
> The mirror collider destroy process happens at avatar load in. It requires that the colliders' hierarchy be enabled by default, so the particle systems can be awake. The hierarchy can be disabled after this process.
>
> Review the handlePhysics layer that was merged into your FX controller. This is for the demo. The layer waits for the "Physics" parameter to be True. You should similarly wait for the "Physics" parameter to be True before animating physics in your layers.
> 
> A very important note is that the "Is Kinematic" property doesn't seem to persist, so you must constantly animate this property to the desired state.
>
> The World Physics object has a world fixed joint on it. The rigidbody for this joint should have "Is Kinematic" enabled locally(IsLocal True), and disabled remotely(IsLocal False), or physics will break.
>
> The demo layer is split between Local and Remote animation sets because I am constantly animating the World Physics "Is Kinematic" property depending on which type of client(IsLocal True or False) is active. You should do something similar.
>
> Using gravity seems to have some minor local-only issues on the Y axis and with culling. Not really a big deal, hard to even notice. Doesn't happen if you don't use gravity on a given rigidbody.
>
> This package just fixes VRChat-related physics problems. Unity physics is rather open-ended and making things work as you intend beyond these fixes is your responsibility.

</details>

<details>
  <summary>導入手順</summary>

> 近日公開。

</details>
