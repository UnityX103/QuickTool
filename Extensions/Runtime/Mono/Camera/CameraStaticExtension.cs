
using UnityEngine;
using  Vector3 = UnityEngine.Vector3;

namespace FrameworkExtensions.Mono.Camera
{
    using Camera= UnityEngine.Camera;

    public static class CameraStaticExtension
    {
        /// <summary>
        ///  获取鼠标在世界坐标中的位置
        /// </summary>
        /// <param name="self">参考摄像机</param>
        /// <param name="charaPos">参考Z轴对象</param>
        /// <returns></returns>
        public static Vector3 GetMouseWorldPos(this  Camera self  ,Vector3 referencePoints)
        {
            var charaScreen = self.WorldToScreenPoint(referencePoints);
            var mousePos = Input.mousePosition;
            mousePos.z = charaScreen.z;
            return self.ScreenToWorldPoint(mousePos);
        }
        
        

     /// <summary>
     /// 辅助摄像机所有参数
     /// </summary>
     /// <param name="self">从</param>
     /// <param name="targetCamera">到</param>
     public static void CopeTo(this  Camera self  ,Camera targetCamera)
     {
         targetCamera.cameraType = self.cameraType;
            targetCamera.enabled = self.enabled;
            targetCamera.clearFlags = self.clearFlags;
            targetCamera.fieldOfView = self.fieldOfView;
            targetCamera.farClipPlane = self.farClipPlane;
            targetCamera.nearClipPlane = self.nearClipPlane;
            targetCamera.backgroundColor = self.backgroundColor;
        
        }
    }
}