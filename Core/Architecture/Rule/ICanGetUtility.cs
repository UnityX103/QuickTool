using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 允许获取工具
/// </summary>
public interface ICanGetUtility:IBelongToArchitecture
{

}

/// <summary>
/// 允许获取工具的静态拓展
/// </summary>
public static class CanGetUtilityExtension
{
    public static T GetUtility<T>(this IBelongToArchitecture self) where T:class,IUtility
    {
        return self.GetArchitecture().GetUtility<T>();
    }
}
