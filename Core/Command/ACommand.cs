using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Plugins.Framework.Core
{
    public abstract class ACommand : BaseCommand
    {
        private UniTask task;

        private event Action _onComplete;

        /// <summary>
        ///  添加结束回调
        /// </summary>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public ACommand AddOnComplete(Action onComplete)
        {
            _onComplete += onComplete;
            return this;
        }

        protected override void Execute()
        {
            base.Execute();

            task = AExecute();
        }

        private async UniTask AExecute()
        {
            await OnExecute();
            _onComplete?.Invoke();
            if (mAutoRelease)
                this.Release();
        }

        protected abstract UniTask OnExecute();

        public UniTask Awaiter()
        {
            return task;
        }

        public override void OnRelease()
        {
            base.OnRelease();
            OnDispose();
        }

        protected abstract void OnDispose();
    }


    public abstract class ACommand<T> : ACommand
    {
        private UniTask task;
        private bool mAutoRelease = true;
        protected T mValue;

        /// <summary>
        ///  设置值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual ACommand<T> SetValue(T value)
        {
            mValue = value;
            return this;
        }
    }
}