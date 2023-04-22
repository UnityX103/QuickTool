using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace FrameworkExtensions.DoTween
{
    public class ColorChanger
    {
        DG.Tweening.Core.DOGetter<Color> getter;
        DG.Tweening.Core.DOSetter<Color> setter;


        public ColorChanger(Func<Color> _getter, Action<Color> _setter)
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

        public async UniTask Start(Color targetColor,float time)
        {
            if (mIsStart)
            {
                Stop();
            }

            mTweener = DOTween.To(getter, setter, targetColor, time).SetEase(Ease.InOutQuad);

            await mTweener.AsyncWaitForCompletion();
        }

        public void Stop()
        {
            mTweener.Kill();
            mIsStart = false;
        }
    }
}