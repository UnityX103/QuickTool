using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace FrameworkExtensions.DoTween
{
    public class ValueChanger
    {
        DG.Tweening.Core.DOGetter<float> getter;
        DG.Tweening.Core.DOSetter<float> setter;


        public ValueChanger(Func<float> _getter, Action<float> _setter)
        {
            getter = () =>
            {
                return _getter();
            };
            setter = (value) =>
            {
                _setter(value);
            };
        }

        bool mIsStart = false;
        private Tweener mTweener;

        public async UniTask Start(float time, float targetValue)
        {
            if (mIsStart)
            {
                Stop();
            }

            mTweener = DOTween.To(getter, setter, targetValue, time).SetEase(Ease.InOutQuad);

            await mTweener.AsyncWaitForCompletion();
        }

        public void Stop()
        {
            mTweener.Kill();
            mIsStart = false;
        }
    }
}