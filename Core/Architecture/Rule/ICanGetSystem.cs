using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 允许获取System
/// </summary>
public interface ICanGetSystem:IBelongToArchitecture
{

    
}

/// <summary>
/// 允许获取System的接口的静态扩展
/// </summary>
public static class CanGetSystemExtension
{
    public static T GetSystem<T>(this ICanGetSystem self) where T : class, ISystem
    {
        return self.GetArchitecture().GetSystem<T>();
    }
}
