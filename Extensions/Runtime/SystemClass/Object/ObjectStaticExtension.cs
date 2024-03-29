using System.Collections.Generic;

namespace FrameworkExtensions.SystemClass.Object
{
    public static class ObjectStaticExtension
    {
        public static void Log(this object self, string text)
        {
            GameTool.Log(self+":"+text);
        }
        public static void LogError(this object self, string text)
        {
            GameTool.LogError(self+":"+text);
        }
        
        public static void LogPositive(this object self, string text)
        {
            GameTool.LogPositive(self+":"+text);
        }

        public static List<T> CopyList<T>(this List<T> origin)
        {
            var list = new List<T>();
            list.AddRange(origin);
            return list;
        }
    }
}