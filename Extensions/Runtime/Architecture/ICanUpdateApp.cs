using System;

namespace FrameworkExtensions.Architecture
{
    public interface ICanUpdateApp
    {
        void AddUpdate(Action action);
        void RemoveUpdate(Action action);
        void AddLateUpdate(Action action);
        void RemoveLateUpdate(Action action);
        void AddFixedUpdate(Action action);

        void RemoveFixedUpdate(Action action);
        void Update();
        void FixedUpdate();
        void LateUpdate();
    }
}