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

Ten grabbable avatar objects. つかむことができる10個のアバターオブジェクト。

<details>
  <summary>Install notes</summary>
  
> Testing in Unity requires the 3.0 Emulator by Lyuma.
>
> Merge the FX controller to your own FX controller, using the Avatars 3.0 Manager tool.
>
> "LeftGrabFX" and "RightGrabFX" are synced parameters, so click the checkbox within the tool to add them to your avatar's parameter asset. If you are using only one hand, sync only that parameter.
>
> The Grab FX.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
>
> Inside the Grab FX/Targets hierarchy, there will be a LeftHandTarget and a RightHandTarget. Place each target under your corresponding wrist and position it where you want each hand's grab radius to start.
>
> Review the hierarchy under Grab FX/Items. Each numbered hierarchy has a Container. Place your item prop in a Container and reset the prop's transforms.
>
> Enable the Box object under Grab FX/Colliders that corresponds to your item. The Cube under each Box object is for visualization, and can be deleted after setup.
>
> Scale the Box object, and adjust it's transforms until the Box covers the handle of your item.
>
> In Grab FX/Targets there will be a numbered hierarchy that corresponds to your item. Locate the appropriate Item#Target.
>
> Item#Target represents your item's starting transforms while not grabbed. Move Item#Target anywhere in your hierarchy, and adjust it's transforms until your item is where you want it.
>
> Select the numbered object for your Container under Grab FX/Items. There will be a parent constraint. Set the Item#Target source weight to 0. Set the Left#Target source weight to 1.
>
> Place the Left#Target object under your left wrist bone and adjust the transforms until your item appears correctly in your hand.
>
> Set the Left#Target source weight back to 0 and repeat a similar process for the Right#Target. When finished, set the source weights back to their defaults. Item#Target 1, Left#Target 0, Right#Target 0.
>
> Hierarchies in Grab FX/Items will be weighted to the Left#Target or Right#Target as the LeftHandTarget and RightHandTarget objects touch an enabled Box from Grab FX/Colliders. 
>
> Review the onLeftGrab and onRightGrab layers that were merged into your FX controller.
>
> If you do not need one of these layers, delete it. If you want to prevent a certain hand from grabbing a certain item, select the Idle state and mute the "to" transition for your item number.
>
> If you want to make the prefab smaller, delete the objects you will not use.

</details>

<details>
  <summary>導入手順</summary>
  
> ※Unity内でテストプレイする場合はLyumaさん作成の3.0エミュレーターが必要となります。
> 
> アバター3.0のManager toolを使用し、FX controllerを自身のFX controllerとマージしてください。
> 
> "LeftGrabFX" と "RightGrabFX" は同期型のパラメターなのでアバターのパラメターに追加する場合はツール内でチェックを入れてください。片手のみの使用の場合はそのパラメターのみ同期してください。
> 
> “Grab FX.prefab”はUnity sceneのベース（一番下）に置くとbase Unityのスケールが使用できます。
> 
> Prefabを右クリックして"Unpack the prefab"を選択してからプレハブごとアバターのベースに追加してください。
> 
> Grab FX/Targetsのヒエラルキーを開くとLeftHandTargetとRightHandTargetがあります。各Targetを手首の位置に調整をしてください。
> 
> Grab FX/Itemsのヒエラルキーを開くと番号の振ってあるContainerがあります。任意のアイテムをContainerに子入れしてからアイテムのTransformをリセットしてください。
> 
> Grab FX/Collidersに子入れしてあるBox objectをEnableしてください。各オブジェクトのしたのキューブは確認用なのでセットアップ後消去してください。
> 
> Boxオブジェクトのスケールと位置をアイテムの掴む部分を全部覆うように設定してください。
> 
> Grab FX/Targetsには番号が振ってあるHierarchyがあります。正しいItem#Target番号を探してください。
> 
> Item#Targetは掴まれていない状態でのアイテムの初期地点を指定しています。Item#Targetをヒエラルキーの任意の場所に移動、オブジェクト自体も置きたい場所に移動させてください。
> 
> Container用にGrab FX/Itemsでオブジェクトの番号を探してください。Parentの紐付けがあります。Item#Targetのsource weightを０に、Left#Targetのsource weightを１に設定してください。
> 
> Left#Targetのオブジェクトを左手首に子入れしてからアイテムが正しく手に入るように移動させてください。
> 
> Left#Targetのsource weightを0戻し、Right#Targetも同様に設定してください。作業が終わったらsource weightをデフォルトの値に戻してください。
（Item#Target 1, Left#Target 0, Right#Target 0）
> 
> Grab FX/Itemsのヒエラルキー内にあるオブジェクトはLeft#TargetとRight#TargetがLeftHandTargetとRightHandTargetのオブジェクトが有効化されたGrab FX/CollidersのBoxとぶつかったときに移動します。
> 
> FX Controllerに追加したonLeftGrabとonRightGrabのレイヤーをご確認ください。
> 
> レイヤーが必要ない場合は消してください。特定のアイテムを指定の手でのみ使用したい場合は"Idle"状態からアイテムナンバーの"to"をミュート設定にしてください。
> 
> Prefabを小さくしたい場合は使用されていないオブジェクトを消してください。

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
> Review the onJiggle layer that was merged into your FX controller. Animate what you want here.

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
> FX Controllerに追加したonJiggleのレイヤーをご確認ください。アニメーションを好きに追加できます。

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
> Targets/MotionTarget is for motion detection. The detection direction is X+, with the red arrow.  
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
> Targets/MotionTargetはモーション判定用です。判定方向はX+、赤い矢印のになります。
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
> Expand the prefab, and locate Particle Driver/ᴛʀɪɢɢᴇʀ. ᴛʀɪɢɢᴇʀ is a particle that when killed will drive a parameter change within your playable layers.
>
> By default, the particle settings on ᴛʀɪɢɢᴇʀ will have it die inside Particle Driver/Cube.
>
> ᴛʀɪɢɢᴇʀ is constrained to ParticleTarget.
> 
> ᴛʀɪɢɢᴇʀ drives the local parameter, "ParticleDeath", and the onParticleDeath layer in the FX controller drives the synced parameter, "FX".
>
> The handleFX layer will play the example Cube.anim clip, which will resize the cube and sync state.
>
> Animate what you want. This is a blank template.

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
> デフォルトでᴛʀɪɢɢᴇʀのパーティクル設定で死ぬときParticle Driver/Cube内で行われます。
> 
> ᴛʀɪɢɢᴇʀはParticleTargetにconstraint（紐付け）されています。
>
> ᴛʀɪɢɢᴇʀはデフォルトでローカルパラメターの"ParticleDeath"を操作し、FX controller内のonParticleDeathレイヤーが同期してある"FX"を操作するように設定してあります。
> 
> handleFXレイヤーは例のCube.animを再生し、キューブのサイズを調整、同期を正しい状態にします。
>
> 白紙のテンプレートなので任意に好きなアニメーションを追加できます。

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

</details>

## [World Physics](https://github.com/VRLabs/VRChat-Avatars-3.0/releases/download/1/WorldPhysics.unitypackage)

Some bandaids to make physics work on avatars. アバターで物理を機能させるためのいくつかの修正。

<details>
  <summary>Install notes</summary>

> This package fixes two problems that break avatar physics in VRChat. First, it destroys colliders in the mirror copy of your avatar to prevent physics collisions from breaking locally. Second, it fixes incorrect movement with rigidbodies in world space non-locally.
>
> Testing in Unity requires the 3.0 Emulator by Lyuma.
> 
> Merge the FX, Gesture controllers to your own FX, Gesture controllers, using the Avatars 3.0 Manager tool.
> 
> The World Physics.prefab should go to the base of your Unity scene, which will give it base Unity scaling.
>
> Unpack the prefab by right-clicking it and move the prefab to base of your avatar.
> 
> The World Physics/Rigidbody is set up for a physics demo. A rigidbody with a Cube falls and collides with the world.
>
> If you want to observe the demo, move World Physics/RigidbodyTarget outside of the prefab to the base of the avatar, and raise the height. When the scene starts the rigidbody will have the constraint disabled, and Is Kinematic set inactive, enabling it to fall and collide. You can take this in-game for testing.
>
> Look at World Physics/Rigidbody/Collider. There is a particle system component on this object. Copy and paste this particle system onto any object with a physics collider. Every object with this particle system will be deleted in the local mirror.
>
> The mirror collider destroy process happens at avatar load in. It requires that the colliders' hierarchy be enabled by default, so the particle systems can be awake. The hierarchy can be disabled after this process.
>
> Review the handlePhysics layer that was merged into your FX controller. This is for the demo. The layer waits for the "Physics" parameter to be True. You should similarly wait for the "Physics" parameter to be True before animating physics in your layers.
> 
> An important note is that the "Is Kinematic" property doesn't seem to persist, so you must constantly animate this property to the desired state. The demo layer is split between Local and Remote animation sets because I am constantly animating the World Physics rigidbody as kinematic depending on which type of client is active. You should do something similar.
>
> Using gravity seems to have some minor local-only issues on the Y axis and with culling. Not really a big deal, hard to even notice. Doesn't happen if you don't use gravity on a given rigidbody.
>
> This package just fixes VRChat-related physics problems. Unity physics is rather open-ended and making things work as you intend beyond these fixes is your responsibility.

</details>

<details>
  <summary>導入手順</summary>

> 近日公開。

</details>
