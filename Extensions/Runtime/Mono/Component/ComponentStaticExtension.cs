using FrameworkExtensions.Mono.Transform;

namespace FrameworkExtensions.Mono.Component
{
    using  Component =  UnityEngine.Component;
    public static class ComponentStaticExtension
    {
        /// <summary>
        /// 获取一个物体的组件并返回，如果没有则给它添加上
        /// </summary>
        /// <typeparam name="T">组件</typeparam>
        /// <param name="obj">物体</param>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>( this  Component self) where T : Component
        {
            if (!self.TryGetComponent<T>(out T component))
            {
                component = self.gameObject.AddComponent<T>();
            }

            return component;
        }

        /// <summary>
        /// 查找子物体并获取身上的对应组件
        /// </summary>
        /// <param name="self">父节点</param>
        /// <param name="childName">子物体名字</param>
        /// <typeparam name="T">组件名</typeparam>
        /// <returns></returns>
        public static T GetChildComponent<T>(this Component self, string childName) where T : Component
        {
            return self.transform.FindObj(childName)?.GetComponent<T>();
        }
        
        
        /// <summary>
        ///  尝试获取子物体身上的制定组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="t"></param>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public static bool TryGetChildCom<T>(this Component self, out T t) where T : Component
        {
            var com = self.GetComponentInChildren<T>();
            if (com != null)
            {
                t = com;
                return true;
            }

            t = null;
            return false;
        }

        /// <summary>
        ///  尝试获取父物体身上的制定组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="t"></param>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns></returns>
        public static bool TryGetParentCom<T>(this Component self,out T t) where T: Component
        {
            var com = self.GetComponentInParent<T>();
            if (com != null)
            {
                t = com;
                return true;
            }

            t = null;
            return false;
        }

    }
}