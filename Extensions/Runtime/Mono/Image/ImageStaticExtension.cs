using FrameworkExtensions.Mono.Color;
using UnityEngine;

namespace FrameworkExtensions.Mono.Image
{
    using  Image=UnityEngine.UI.Image;
    public static class ImageStaticExtension
    {
        public static void SetAlpha( this  Image self, float value)
        {
            self.color = self.color.SetAlpha(value);
        }
    }
}