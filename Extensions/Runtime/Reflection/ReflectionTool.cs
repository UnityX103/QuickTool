namespace FrameworkExtensions.Reflection
{
    public static class ReflectionTool
    {
        
        /// <summary>
        ///  复制所有的参数到目标类
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        public static void CopyAllProperty<T>(this  object from,T to ) 
        {
            foreach (var item in  from.GetType().GetFields())
            {
               item.SetValue(to,item.GetValue(from));
            }
        }
    }
}