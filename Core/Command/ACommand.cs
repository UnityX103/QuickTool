using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
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
                this.DoRelease();
        }

        protected abstract UniTask OnExecute();

        public UniTask Awaiter()
        {
            return task;
        }

        public override void Release()
        {
            base.Release();
            OnDispose();
        }

     protected abstract void OnDispose();
    }


    public abstract class ACommand<T> : ACommand
    {
        private UniTask task;

        [SerializeField]
        protected T Value;

        /// <summary>
        ///  设置值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual ACommand<T> SetValue(T value)
        {
            Value = value;
            return this;
        }
        
        protected override void OnDispose()
        {
            Value = default;
        }
    }
    
    
    public abstract class ACommand<T,T2> : ACommand
    {
        private UniTask task;

        [ShowInInspector]
        protected T Value;
        [ShowInInspector]
        protected T2 Value2;
        
        /// <summary>
        ///  设置值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual ACommand<T,T2> SetValue(T value,T2 value2)
        {
            Value = value;
            Value2 = value2;
            return this;
        }
        
        protected override void OnDispose()
        {
            Value = default;
            Value2 = default;
        }
    }
}