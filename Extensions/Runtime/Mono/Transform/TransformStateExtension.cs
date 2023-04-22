using System.Collections.Generic;
using FrameworkExtensions.Mono.GameObject;
using UnityEngine;

namespace FrameworkExtensions.Mono.Transform
{
    using  GameObject=UnityEngine.GameObject;
    using Transform = UnityEngine.Transform;
    
    public static class TransformStateExtension
    {
        /// <summary>
        /// 从列表中获取距离当前Transform最近的点
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="target">所有点列表</param>
        /// <returns></returns>
        public static Transform GetClosetTransform(this Transform self,List<Transform> target)
        {
            float minDistance = float.MaxValue;
            Transform closestTrans = null;
            foreach (var trans in target)
            {
                if (trans == self)
                {
                    continue;
                }
                var distance = Vector2.Distance(self.position, trans.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTrans = trans;
                }
            }
            return closestTrans;
        }
        public static void CopeTo(this Transform self,Transform target)
        {
            target.position = self.position;
            target.rotation = self.rotation;
            target.localScale = self.localScale;
        }
        
        /// <summary>
        /// 遍历查找所有子物体
        /// </summary>
        /// <param name="self"></param>
        /// <param name="name">物体名</param>
        /// <returns></returns>
        public static GameObject FindObj(this Transform self, string name)
        {
            return TransformExtensions.FindObj(self,name);
        }
        
        public static GameObject FindObjAccordingLevel(this Transform self, string levelName)
        {
            return TransformExtensions.FindObj(self,levelName);
        }
        
        /// <summary>
        ///  自动反转X轴 
        /// </summary>
        /// <param name="transform"></param>
        public static void FlipX(this Transform transform)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3(-1*scale.x, scale.y, scale.z);
        }
        
        /// <summary>
        /// 反转X 到指定方向
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="isPositive"></param>
        public static void FlipXPositive(this Transform transform, bool isPositive)
        {
            var scale = transform.localScale;
            transform.localScale = new Vector3((isPositive? 1:-1)*Mathf.Abs(scale.x), scale.y, scale.z);
        }
        
   
        /// <summary>
        /// 只修改X轴位置
        /// </summary>
        /// <param name="self"></param>
        /// <param name="x"></param>
        public static void SetPositionX(this Transform self,float x)
        {
            var pos = self.position;
            self.position = new Vector3(x,pos.y,pos.z);
        }

        /// <summary>
        /// 只修改Z轴位置
        /// </summary>
        /// <param name="self"></param>
        public static void SetPositionZ(this Transform self,float z)
        {
            var pos = self.position;
            self.position = new Vector3(pos.x,pos.y,z);
        }

        /// <summary>
        /// 只修改Y轴位置
        /// </summary>
        /// <param name="self"></param>
        public static void SetPositionY(this Transform self,float y)
        {
            var pos = self.position;
            self.position = new Vector3(pos.x,y,pos.z);
        }
        
        public static void Destroy(this Transform trans)
        {
            trans.gameObject.Destroy();
        }
    }
}