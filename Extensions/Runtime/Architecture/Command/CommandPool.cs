using Sirenix.OdinInspector;
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
        public int _actCount;
        public int _countAll;
        public int _inCount;
        [Button]
        public void GetNum()
        {
            _countAll = CountAll;
            _actCount = CountActive;
            _inCount = CountInactive;
        }
        
        public CommandPool() : base(createFunc, null, OnRelease, null, true, 50, 500)
        {
        }

        private static void OnRelease(T command)
        { 
            command.Available = true;
            command.Release();
        }


        private static T createFunc()
        {
            return new T();
        }


        T1 ICommandPool.Get<T1>()
        {
            var cmd = Get() as T1;
            cmd.Available = false;
            return cmd;
        }

        void ICommandPool.Release(BaseCommand baseCommand)
        {
            if (!baseCommand.Available)
                 Release((T)baseCommand);
        }
    }
}