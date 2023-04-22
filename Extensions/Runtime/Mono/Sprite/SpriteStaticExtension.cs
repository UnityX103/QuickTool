using FrameworkExtensions.Mono.Color;
using UnityEngine;

namespace OtherTool.Mono.Sprite
{
    public static class SpriteStaticExtension
    {
        /// <summary>
        /// 设置透明度
        /// </summary>
        /// <param name="spriteRenderer"></param>
        /// <param name="a"></param>
        public static void SetAlpha(this SpriteRenderer spriteRenderer,float a)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = spriteRenderer.color.SetAlpha(a);
        }
        
        
        
        
    }
    
  
}