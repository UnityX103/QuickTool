using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace FrameworkExtensions.Spine
{
    [System.Serializable]
    public struct AnimData
    {
        [LabelText("层")]
        public int Layer;
        
        [LabelText("动画名")]
        [ValueDropdown("GetThisAllAnimName")] public string AnimName;

        [LabelText("循环")]
        public bool Loop;

        [LabelText("手动绑定Spine动画机")]
        public bool selectAnim;
        [ShowIf(nameof(selectAnim))] public SkeletonAnimation anim;

        public string[] GetThisAllAnimName
        {
            get
            {
                if (Application.isPlaying)
                {
                    return null;
                }
#if UNITY_EDITOR
                else
                {
                    var anims = (anim ? anim : Selection.activeGameObject.GetComponentInChildren<SkeletonAnimation>())
                        .skeletonDataAsset
                        .GetAnimationStateData().SkeletonData.Animations;

                    List<string> temp = new List<string>();
                    foreach (var VARIABLE in anims)
                    {
                        temp.Add(VARIABLE.Name);
                    }

                    return temp.ToArray();
                }
#else
 return null;
#endif
            }
        }
        
    }

    public class SpineExtension
    {
        public static UniTask SetAnim(SkeletonAnimation _skeleton, string animName, int track = 0, bool isLoop = false)
        {
            if (_skeleton == null || _skeleton.state == null)
            {
                //     Debug.LogError(animName + "丢失");
                _skeleton.Awake();
            }

            var pEntry = _skeleton.state.GetCurrent(track);

            if (pEntry != null && animName == pEntry.Animation.Name && isLoop && pEntry.Loop)
            {
                Debug.LogError("为空");
                return UniTask.CompletedTask;
            }

            var entry = _skeleton.state.SetAnimation(track, animName, isLoop);
            //entry.MixDuration = mixDuration;

            return UniTask.WaitUntil(() => entry.IsComplete);
        }

        public static UniTask SetAnim(SkeletonAnimation _skeleton, AnimData data)
        {
            return SetAnim(_skeleton, data.AnimName, data.Layer, data.Loop);
        }

        public static void SetColor(Renderer renderer, Color color)
        {
            var propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}