using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtensions.SystemClass.List
{
    public static class ListStaticExtension
    {
        public static T Random<T>(this List<T> self) where T : class
        {
            if (self.Count <= 0)
            {
                return null;
            }
            int index = UnityEngine.Random.Range(0,self.Count);
            return self[index];
        }
        
        public static List<T> CloneList<T>(this List<T> self)
        {
            List<T> list = new List<T>();
            list.AddRange(self);
            return list;
        }
        
    }
  
}