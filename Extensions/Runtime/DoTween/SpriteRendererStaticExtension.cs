using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FrameworkExtensions.DoTween
{
    public static class SpriteRendererStaticExtension
    {
        /// <summary>
        /// 颜色闪烁，眨眼
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target">目标颜色</param>
        /// <param name="time">效果需要多久完成s</param>
        /// <param name="origin">默认颜色</param>
        public static async UniTask ColorBlink(this SpriteRenderer self,Color target, float time = 0.2f,Color origin = default)
        {
            if (origin == default)
            {
                origin = Color.white;
            }
            await self.DOColor(target, time/2).AsyncWaitForCompletion();
            await self.DOColor(origin, time/2).AsyncWaitForCompletion();
        }
    }
}