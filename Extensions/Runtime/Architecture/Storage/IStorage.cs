using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorage : IUtility
{
    bool DataExist(string key, string fileName = "default");
    /// <summary>
    /// 存储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    void Save<T>(string key, T value, string fileName = "default");

    /// <summary>
    /// 读取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    bool Load<T>(string key, out T value, string fileName = "default");

    
     T LoadAndReplace<T>(string key, T orgData)
     {
         if (Load<T>(key, out var loadData))
         {
             return loadData;
         }

         return orgData;
     }
    void ClearFile();
}




public static class ICanSLExtension 
{

    public static void RegisterSaveAndLoad(this ICanSL self)
    {
        self.RegisterEvent<E_Save>(e => self.Save(e.mValue));
        self.RegisterEvent<E_Load>(e => self.Load(e.mValue));
    }
    
}

