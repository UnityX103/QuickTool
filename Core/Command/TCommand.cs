using UnityEngine;

namespace Plugins.Framework.Core
{
    [System.Serializable]
    public abstract class TCommand<T> : BaseCommand
    {
        [SerializeField] public T Value;

        public virtual TCommand<T> SetValue(T value)
        {
            Value = value;
            return this;
        }

        protected sealed override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.DoRelease();
        }

        protected abstract void OnExecute();

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Value = default;
        }
    }

    [System.Serializable]
    public abstract class TCommand<T1, T2> : BaseCommand
    {
        [SerializeField] protected T1 Value;
        [SerializeField] protected T2 Value2;

        public TCommand<T1, T2> SetValue(T1 v1, T2 v2)
        {
            Value = v1;
            Value2 = v2;
            return this;
        }


        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.DoRelease();
        }

        protected abstract void OnExecute();

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Value = default;
            Value2 = default;
        }
    }

    [System.Serializable]
    public abstract class TCommand<T1, T2, T3> : BaseCommand
    {
        [SerializeField] protected T1 Value;
        [SerializeField] protected T2 Value2;
        [SerializeField] protected T3 Value3;

        public TCommand<T1, T2, T3> SetValue(T1 v1, T2 v2, T3 v3)
        {
            Value = v1;
            Value2 = v2;
            Value3 = v3;
            return this;
        }


        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.DoRelease();
        }

        protected abstract void OnExecute();

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Value = default;
            Value2 = default;
            Value3 = default;
        }
    }

    public abstract class TCommand<T1, T2, T3, T4> : BaseCommand
    {
        [SerializeField] protected T1 Value;
        [SerializeField] protected T2 Value2;
        [SerializeField] protected T3 Value3;
        [SerializeField] protected T4 Value4;

        public TCommand<T1, T2, T3, T4> SetValue(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            Value = v1;
            Value2 = v2;
            Value3 = v3;
            Value4 = v4;
            return this;
        }

        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.DoRelease();
        }

        protected abstract void OnExecute();

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Value = default;
            Value2 = default;
            Value3 = default;
            Value4 = default;
        }
    }


    public abstract class TCommand<T1, T2, T3, T4, T5> : BaseCommand
    {
        [SerializeField] protected T1 Value;
        [SerializeField] protected T2 Value2;
        [SerializeField] protected T3 Value3;
        [SerializeField] protected T4 Value4;
        [SerializeField] protected T5 Value5;

        public TCommand<T1, T2, T3, T4, T5> SetValue(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
        {
            Value = v1;
            Value2 = v2;
            Value3 = v3;
            Value4 = v4;
            Value5 = v5;
            return this;
        }

        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.DoRelease();
        }

        protected abstract void OnExecute();

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Value = default;
            Value2 = default;
            Value3 = default;
            Value4 = default;
            Value5 = default;
        }
    }


    public abstract class RCommand<R, T> : BaseCommand
    {
        [SerializeField] public R Result;
        [SerializeField] protected T Value;



        public RCommand<R, T> SetValue(T value)
        {
            Value = value;
            return this;
        }

        protected override void Execute()
        {
            base.Execute();
            Result = OnExecute(Value);
        }

        protected abstract R OnExecute(T value);

        public sealed override void Release()
        {
            base.Release();
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            Result = default;
            Value = default;
        }
    }
}