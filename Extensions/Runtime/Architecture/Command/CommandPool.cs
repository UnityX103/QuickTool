using UnityEngine;
using UnityEngine.Pool;

namespace Framework.Extensions.Runtime.Architecture
{
    public interface ICommandPool
    {
        T Get<T>() where T : BaseCommand, new();
        void Release(BaseCommand baseCommand);
    }

    public class CommandPool<T> : ObjectPool<T>, ICommandPool where T : BaseCommand, new()
    {
        public CommandPool() : base(createFunc, null, OnRelease, null, true, 50, 500)
        {
        }

        private static void OnRelease(T command)
        {
            command.OnRelease();
        }


        private static T createFunc()
        {
            return new T();
        }


        T1 ICommandPool.Get<T1>()
        {
            return Get() as T1;
        }

        void ICommandPool.Release(BaseCommand baseCommand)
        {
            if (!baseCommand.Available)
                 Release((T)baseCommand);
        }
    }
}