using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace FrameworkExtensions.DoTween
{
    public static class DoTweenStateExtension
    {
        public static UniTask Wait(this Tweener self)
        {
            return self.AsyncWaitForCompletion().AsUniTask();
        }
    }
}