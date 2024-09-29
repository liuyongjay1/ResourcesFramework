/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
//using System.Reflection;
//using System.Linq;

//配置的详细介绍请看Doc下《XLua的配置.doc》
public static class ExampleGenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
        /*
                typeof(System.Object),
                typeof(UnityEngine.Object),
                typeof(Vector2),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Ray),
                typeof(Bounds),
                typeof(Ray2D),
                typeof(Time),
                typeof(GameObject),
                typeof(Material),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Resources),
                typeof(TextAsset),
                typeof(Keyframe),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(ParticleSystem),
                typeof(SkinnedMeshRenderer),
                typeof(Renderer),
                typeof(WWW),
                typeof(Light),
                typeof(Mathf),
                typeof(System.Collections.Generic.List<int>),
                typeof(Action<string>),
                typeof(UnityEngine.Debug)
        */
        typeof(UnityEngine.Object),
		typeof(UnityEngine.Debug),

		typeof(Application),
		typeof(PlayerPrefs),
		typeof(Time),


		typeof(WWWForm),

		//数学
		typeof(Mathf),
		typeof(Matrix4x4),
		typeof(Vector2),
		typeof(Vector3),
		typeof(Vector4),
		typeof(Quaternion),

		//物理
		//typeof(Physics), //代码量巨大禁用
		//typeof(Physics2D), //代码量巨大禁用
		typeof(Ray),
		typeof(Ray2D),
		typeof(RaycastHit),
		typeof(RaycastHit2D),

		//组件
		typeof(Component),
		typeof(Behaviour),
		typeof(MonoBehaviour),
		typeof(GameObject),
		typeof(Transform),
		typeof(RectTransform),
		typeof(RectTransformUtility),

		//资源
		typeof(Resources),
		typeof(AssetBundle),
		typeof(AssetBundleManifest),
		typeof(AssetBundleRequest),
		typeof(Shader),
		typeof(Material),
		typeof(MaterialPropertyBlock),
		typeof(TextAsset),
		typeof(AudioClip),
		typeof(AudioSource),
		typeof(Texture),
		typeof(Texture2D),
		typeof(Sprite),
		typeof(Font),
		//UI
		typeof(Canvas),
		typeof(CanvasGroup),
		typeof(CanvasRenderer),
		typeof(SpriteMask),
		typeof(SortingLayer),
		typeof(RenderTexture),
		typeof(RenderTextureFormat),
		typeof(RenderTextureReadWrite),
		typeof(Bounds),
		typeof(Rect),
		typeof(Color),
		typeof(ColorUtility),
		typeof(TMPro.TextMeshProUGUI),
		typeof(TMPro.TMP_Dropdown),
		typeof(TMPro.TMP_InputField),
		typeof(TMPro.TMP_Text),
		typeof(TMPro.TMP_Settings),
		typeof(TMPro.TMP_FontAsset),
		typeof(TMPro.TMP_SpriteAsset),
		typeof(TMPro.TMP_Style),
		//动画
		typeof(Animator),
		typeof(Animation),
		typeof(AnimationCurve),
		typeof(AnimationClip),
		typeof(AnimationEvent),
		typeof(AnimationState),
		typeof(AnimationBlendMode),
		typeof(AnimationCullingType),
		typeof(AnimationPlayMode),
		typeof(Keyframe),
/*
		//Spine
		typeof(Spine.Unity.BoneFollower),
		typeof(Spine.Unity.BoneFollowerGraphic),
		typeof(Spine.Unity.BoundingBoxFollower),
		typeof(Spine.Unity.PointFollower),
		typeof(Spine.Unity.SkeletonAnimation),
		typeof(Spine.Unity.SkeletonDataAsset),
		typeof(Spine.Unity.SkeletonGraphic),
		typeof(Spine.Unity.SkeletonMecanim),
		typeof(Spine.Unity.SkeletonPartsRenderer),
		typeof(Spine.Unity.SkeletonRenderer),
		typeof(Spine.Unity.SkeletonRendererInstruction),
		typeof(Spine.Unity.SkeletonRenderSeparator),
		typeof(Spine.Unity.SkeletonUtility),
		typeof(Spine.Unity.SkeletonUtilityBone),
		typeof(Spine.Unity.SkeletonUtilityConstraint),
		typeof(Spine.Unity.SpineAnimation),
		typeof(Spine.Unity.SpineAtlasAsset),
		typeof(Spine.Unity.SpineAtlasRegion),
		typeof(Spine.Unity.SpineAttachment),
		typeof(Spine.Unity.SpineEvent),
		typeof(Spine.Unity.SpineIkConstraint),
		typeof(Spine.Unity.SpineMesh),
		typeof(Spine.Unity.SpinePathConstraint),
		typeof(Spine.Unity.SpineSkin),
		typeof(Spine.Unity.SpineSlot),
		typeof(Spine.Unity.SpineTransformConstraint),
		typeof(Spine.Unity.SkeletonExtensions),
*/
		//视频
		typeof(UnityEngine.Video.VideoPlayer),

		//渲染
		typeof(Camera),
		typeof(Screen),
		typeof(Renderer),
		typeof(MeshRenderer),
		typeof(SkinnedMeshRenderer),
		typeof(TrailRenderer),
		typeof(ParticleSystem),

		//U2D
		typeof(UnityEngine.U2D.SpriteAtlas),

		//DOTween
		typeof(DG.Tweening.DOTween),
		typeof(DG.Tweening.DOVirtual),
		typeof(DG.Tweening.EaseFactory),
		typeof(DG.Tweening.Tweener),
		typeof(DG.Tweening.Tween),
		typeof(DG.Tweening.Sequence),
		typeof(DG.Tweening.TweenParams),
		typeof(DG.Tweening.Core.ABSSequentiable),
		typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),
		typeof(DG.Tweening.TweenCallback),
		typeof(DG.Tweening.TweenExtensions),
		typeof(DG.Tweening.TweenSettingsExtensions),
		typeof(DG.Tweening.ShortcutExtensions),
		typeof(DG.Tweening.DOTweenModuleUI),

		//Plugins
		//typeof(LuaInterface.Debugger),
			};

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
				typeof(Action<int, LuaTable>),
				typeof(Action<Gesture>),
				typeof(Action<XLua.LuaTable, AssetLoaderBase, bool>),
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Action<string>),
                typeof(Action<Vector2>),
                typeof(Action<double>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(System.Collections.IEnumerator)
            };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},

				//Texture
                new List<string>(){"UnityEngine.Texture", "imageContentsHash"},

				//MeshRenderer
				new List<string>(){"UnityEngine.MeshRenderer", "scaleInLightmap"},
				new List<string>(){"UnityEngine.MeshRenderer", "receiveGI"},
                new List<string>(){"UnityEngine.MeshRenderer", "stitchLightmapSeams"},
                new List<string>(){"UnityEngine.MeshRenderer", "scaleInLightmap"},
				new List<string>(){"UnityEngine.MeshRenderer", "receiveGI"},
                new List<string>(){"UnityEngine.MeshRenderer", "stitchLightmapSeams"},

				//AudioSource
				new List<string>(){"UnityEngine.AudioSource", "gamepadSpeakerOutputType"},
                new List<string>(){"UnityEngine.AudioSource", "PlayOnGamepad","System.Int32"},
                new List<string>(){"UnityEngine.AudioSource", "DisableGamepadOutput"},
                new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerMixLevel","System.Int32","System.Int32"},
                new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerMixLevelDefault","System.Int32"},
                new List<string>(){"UnityEngine.AudioSource", "SetGamepadSpeakerRestrictedAudio","System.Int32","System.Boolean"},
                new List<string>(){"UnityEngine.AudioSource", "GamepadSpeakerSupportsOutputType","UnityEngine.GamepadSpeakerOutputType"},

				//Light
				new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.Light", "SetLightDirty"},
                new List<string>(){"UnityEngine.Light", "shadowRadius"},
                new List<string>(){"UnityEngine.Light", "shadowAngle"},


                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
            };
}
