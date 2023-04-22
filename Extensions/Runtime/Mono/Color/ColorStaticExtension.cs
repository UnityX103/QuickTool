using UnityEngine;

namespace FrameworkExtensions.Mono.Color
{
    using  Color=UnityEngine.Color;
    public static class ColorStaticExtension
    {
        public static Color SetAlpha( this Color self, float value)
        {
            self.a = Mathf.Clamp01(value);
            return self;
        }
    }
}