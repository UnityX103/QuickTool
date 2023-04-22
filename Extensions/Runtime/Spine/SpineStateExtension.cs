using Cysharp.Threading.Tasks;
using Spine.Unity;
using UnityEngine;

namespace FrameworkExtensions.Spine
{
    public static class SpineStateExtension
    {
        public static UniTask SetAnim(this SkeletonAnimation self, AnimData data)
        {
            return SpineExtension.SetAnim(self, data);
        }
        
        public static void SetColor(this SkeletonAnimation self, Color color)
        {
             SpineExtension.SetColor(self.GetComponent<Renderer>(), color);
        }
    }
}