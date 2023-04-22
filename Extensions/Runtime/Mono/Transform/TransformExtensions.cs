using UnityEngine;

namespace FrameworkExtensions.Mono.Transform
{
    using  GameObject=UnityEngine.GameObject;
    using Transform = UnityEngine.Transform;

    public class TransformExtensions
    {
        public static GameObject FindObj( Transform parent, string name)
        {
            var objs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var item in objs)
            {
                if (item.name == name)
                {
                    return item.gameObject;
                }
            }
            Debug. LogError("未找到子物体"+name);
            return null;
        }
        
        public static GameObject FindObjAccordingLevel( Transform parent, string levelName)
        {
            var objs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var item in objs)
            {
                if (item.gameObject.layer == LayerMask.NameToLayer(levelName))
                {
                    return item.gameObject;
                }
            }

            Debug. LogError("未找到子物体"+levelName);
            return null;
        }
    }
}