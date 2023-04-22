using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 允许获取Model
/// </summary>
public interface ICanGetModel:IBelongToArchitecture
{


}

/// <summary>
/// 获取Model的静态拓展
/// </summary>
public static class CanGetModelExtension
{
    public static T GetModel<T>(this ICanGetModel self) where T : class, IModel 
    {
        return self.GetArchitecture().GetModel<T>();
    }
    public static IModel GetModelFromType(this ICanGetModel self,Type type) 
    {
        return self.GetArchitecture().GetModel(type);
    }
}
