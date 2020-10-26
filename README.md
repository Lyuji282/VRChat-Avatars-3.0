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

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
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

## [Grab FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/GrabFX.unitypackage)

Handle an avatar object with touch. アバターオブジェクトをタッチで操作。

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
> Grab FX/ColliderTarget is a box collider that represents the area you need to touch. Move it outside of the prefab to an appropriate spot in your hierarchy. Scale it as needed.
>
> Grab FX/Container will be weighted between GrabTarget and ColliderTarget as they touch. Replace the Cube under the Container with your own objects, and fix transforms as needed.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "GrabFX"は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。
>  
> Grab FX.prefabはUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Grab FX/GrabTargetを探し、ヒエラルキーの任意の場所に設置してください。GrabTargetはタッチに使います。
> 
> Grab FX/ColliderTargetは触る対象用のボックスコライダーです。Prefab外の任意の場所に移動させてください。必要にリサイズ可です。
>
> Grab FX/ContainerはGrabTargetとColliderTargetが触れている間にウエイトを設定します。Container内のCubeを任意のオブジェクトに置き換え、必要に応じて大きさを調整してください。

</details>

## [Jiggle FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/JiggleFX.unitypackage)

Play an effect when the target object is jiggled or shaken. 対象オブジェクトが揺らされた場合に再生されるエフェクト。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> The Jiggle FX.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Jiggle FX/JiggleTarget. JiggleTarget is what you use for jiggling. Move it outside of the prefab to an appropriate spot in your hierarchy.
> 
> Jiggle FX/Detection is where you adjust difficulty. Under the emission module of the particle system, the lower the rate over distance, the harder it is to jiggle.
> 
> Review the handleJiggleFX layer that was merged into your FX controller. Animate what you want here.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "Jiggle FX.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Jiggle FX/JiggleTargetを探してください。JiggleTargetは揺らすために使用します。Prefab外の任意の場所に移動させてください。
> 
> Jiggle FX/Detectionでは判定の難易度を設定します。パーティクルシステム下のemission moduleにて距離のレートを低くすると揺らしにくくなります。
> 
> FX Controllerに追加したhandleJiggleFXのレイヤーをご確認ください。アニメーションを好きに追加できます。

</details>

## [Light Slash](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/LightSlash.unitypackage)

Perform consecutive slashes with motion. モーションからのスラッシュ効果が表示できます。

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

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "LightSlashFX"は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。
> 
> "Light Slash.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Light Slash/Targetsを探してください。このオブジェクトはPropヒエラルキー下に入れ、PositionとRotationをすべてリセットしてください。
> 
> Targets/MotionTargetはモーション判定用です。判定方向はX-、赤い矢印の反対側になります。
> 
> Targets/EffectTargetはスラッシュエフェクトが表示される位置です。エフェクト表示場所に納得がいくまで位置と回転を調整してください。
> 
> 判定の難易度を上げる場合はLight Slash/Colliderの一番下のConstraint Sourceを調整してください。デフォルトは.2、.1はとても切りづらくなります。
> 
> このシステムはLight Slash/Colliderのオブジェクトをanimate offにすると作動しなくなります。

</details>

## [Marker](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/Marker.unitypackage)

A pen for drawing. お絵描き用のペン。

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

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "The Marker.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Marker/DrawingTargetを探してください。このオブジェクトを利き手のwristボーンに入れ子してから位置と回転を調整してください。
> 
> FX Controllerに追加したmarkerLeftとmarkerRightのレイヤーをご確認ください。使用したいお絵描き、消しゴム用のジェスチャーのtransitionをunmuteしてください。

</details>

## [Particle Driver](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ParticleDriver.unitypackage)

A method for animating on particle death. パーティクルによるデスアニメーション用のツール。

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

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "FX"は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。
>  
> "Particle Driver.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Particle Driver/ᴛʀɪɢɢᴇʀを探してください。ᴛʀɪɢɢᴇʀはデス判定が入った時にプレイ可能レイヤーのパラメターを変えるパーティクルです。
>
> ᴛʀɪɢɢᴇʀはデフォルトでローカルパラメターの"ParticleDeath"を操作します。
> 
> デフォルトのFX Controllerの設定でParticleDeathの変更は同期してある"FX"を操作するように設定してあります。
>
> 白紙のテンプレートなので任意に好きなアニメーションを追加できます。
> 
> ᴛʀɪɢɢᴇʀはParticleTargetにcontraint（紐付け）されています。

</details>

## [Proximity FX](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/ProximityFX.unitypackage)

Player-local proximity effects. プレイヤー近接発動エフェクト。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma. Test with a non-local clone and the testDummy.
> 
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
> 
> The Proximity FX.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
> 
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> Expand the prefab, and locate Proximity FX/ᴍɪɴ, Proximity FX/ᴍɪᴅ, Proximity FX/ᴍᴀx. These are particles, that when killed by player-local collision, will drive parameter changes within your playable layers.
>
> Adjust the collision radius on these particles as you see fit.
> 
> Review the handleProximityFX layer that was merged into your FX controller. Notice the Blend Tree.
>
> Edit the Idle.anim, minProximity.anim, midProximity.anim, maxProximity.anim clips to animate what you want.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。テストプレイの場合はnon-localのクローンとtestDummyをご使用ください。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "Proximity FX.prefab"はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Prefabを開き、Proximity FX/ᴍɪɴ, Proximity FX/ᴍɪᴅ, Proximity FX/ᴍᴀxを探してください。これらはプレイヤー近接発動でのデス判定が入った時にプレイ可能レイヤーのパラメターを変えるパーティクルです。
>
> パーティクルのCollision Radius（抵触半径）を任意に調整してください。
> 
> FX Controllerに追加したhandleProximityFXのレイヤーをご確認ください。Blend Treeを注視してください。
>
> Idle.anim, minProximity.anim, midProximity.anim, maxProximity.animに任意のアニメーションを追加してください。

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
> Review the handleWorldFX layer that was merged into your FX controller. Change "WorldFX" parameter to cause transitions within this layer.
>
> Edit the handleWorldFX.anim clip to customize.

</details>

<details>
  <summary>導入手順</summary>

> 1つのConstraintで完結する比較的シンプルなメソッドです。
>
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
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
>
> カスタマイズしたい場合はhandleWorldFX.animを編集してください。

</details>
