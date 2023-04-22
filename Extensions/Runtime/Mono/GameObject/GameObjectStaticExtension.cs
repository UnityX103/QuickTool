using UnityEngine;

namespace FrameworkExtensions.Mono.GameObject
{
    using  GameObject = UnityEngine.GameObject;
    public static class GameObjectStaticExtension
    {
        public static void Destroy(this GameObject obj)
        {
            if (obj == null)
            {
                return;
            }
            foreach (var item in obj.GetComponentsInChildren<ITriggerWhenDestroy>(true))
            {
                item.WhenDestroy();
            }
            Object.Destroy(obj);
        }

    }
}