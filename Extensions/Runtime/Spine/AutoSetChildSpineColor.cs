using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FrameworkExtensions.Spine
{
    /// <summary>
    /// 自动设置所有子物体的颜色
    /// </summary>
    [ExecuteInEditMode]
    public class AutoSetChildSpineColor : MonoBehaviour
    {
        //shader变量名
        private const string colorTag = "_Color";
        [OnValueChanged("ChangerColor")]
        public Color color;
        private void OnEnable()
        {
            ChangerColor();
        }

        private void ChangerColor()
        {
            foreach (var item in   GetComponentsInChildren<Renderer>(true))
            {
                SpineExtension.SetColor(item, color);
            }
        }
    }
}