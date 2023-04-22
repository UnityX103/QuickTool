namespace Plugins.Framework.Core
{
    public abstract class TCommand<T> : BaseCommand
    {
        protected T mValue;

        public virtual TCommand<T> SetValue(T value)
        {
            mValue = value;
            return this;
        }

        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.Release();
        }

        protected abstract void OnExecute();

        public override void OnRelease()
        {
            base.OnRelease();
            OnDispose();
        }

        protected abstract void OnDispose();
    }


    public abstract class TCommand<T, T1> : BaseCommand
    {
    

        public abstract TCommand<T, T1> SetValue(T v1, T1 v2);


        protected override void Execute()
        {
            base.Execute();
            OnExecute();
            if (mAutoRelease)
                this.Release();
        }

        protected abstract void OnExecute();

        public override void OnRelease()
        {
            base.OnRelease();
            OnDispose();
        }

        protected abstract void OnDispose();
    }
}