using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FrameworkExtensions.Reflection;
using Sirenix.Utilities;
using UnityEngine;
using Object = System.Object;

namespace FrameworkExtensions.OdinStateExtensions
{
    public static class OdinStaticExtansion
    {
        public static IEnumerable<Type> GetTypes<T>(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = typeof(T).Assembly;
            }
           
            var q =  assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseClass
                .Where(x => !x.IsGenericTypeDefinition) // Excludes C1<>
                .Where(x => typeof(T).IsAssignableFrom(x)); // Excludes classes not inheriting from BaseClass
            return q;
        }
        
        public static IEnumerable<Type> GetTypes(Type[] types ,Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = AssemblyExtensions.Runtime;
            }
           
            var q =  assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseClass
                .Where(x => !x.IsGenericTypeDefinition) // Excludes C1<>
                .Where(x=> types.IsAssignableFromTypes(x)); // Excludes classes not inheriting from BaseClass
            return q;
        }

        private static bool IsAssignableFromTypes( this  Type[]   self , Type x)
           {
                foreach (var type in self)
                {
                    if (type.IsAssignableFrom(x))
                    {
                        return true;
                    }
                }
                return false;
            }
       
        
        /// <summary>
        /// 获取指定类型可选择的值，排除Unity.Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<Type> GetTypes_UnObject<T>(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = typeof(T).Assembly;
            }
            var q = typeof(T).Assembly.GetTypes()
                .Where(x => !x.IsAbstract) // Excludes BaseClass
                .Where(x => !x.IsGenericTypeDefinition) // Excludes C1<>
                .Where(x => typeof(T).IsAssignableFrom(x))
                .Where(x => !x.GetBaseTypes()
                    .Contains(typeof(Object))); 
            return q;
        }
    }
}