namespace FrameworkExtensions.SystemClass.Type
{
    using  Type =System.Type;
    public static class TypeStateExtension 
    {
        /// <summary>
        /// 获取去除命名空间后的类名
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string RemoveNamespaceTypeName(this  Type self)
        {
            string typeName = self.ToString();
            var names = typeName.Split('.');
            if (names.Length == 0)
            {
                return typeName;
            }
            return  names[^1];
        }
    }
}