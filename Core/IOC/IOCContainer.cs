using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IOC容器
/// </summary>
public class  IOCContainer
{

    public Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

    /// <summary>
    /// 注册
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public void Register<T>(T instance)
    {
        var key = typeof(T);

        if (mInstances.ContainsKey(key))
        {
            mInstances[key] = null;
            mInstances[key] = instance;
        }
        else
        {
            mInstances.Add(key,instance);
        }
    }

    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Get<T>() where T : class
    {
        var key = typeof(T);

        object retObj;

        if (mInstances.TryGetValue(key, out retObj))
        {
            return retObj as T;
        }
        Debug.LogError($"IOC获取（{typeof(T)}）失败，未组册/注册的是接口");
        return null;
    }
    
    /// <summary>
    /// 获取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public object Get(Type type) 
    {

        if (mInstances.TryGetValue(type, out var retObj))
        {
            return retObj ;
        }
        Debug.LogError($"IOC获取（{type}）失败，未组册/注册的是接口");
        return null;
    }
    
}
