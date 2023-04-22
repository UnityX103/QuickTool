using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 允许发送事件的接口
/// </summary>
public interface ICanRegisterEvent : IBelongToArchitecture
{

}

/// <summary>
/// 允许发送事件的接口的静态拓展
/// </summary>
public static class CanRegisterEventExtension
{

    public static IUnRegister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
    {
        return self.GetArchitecture().RegisterEvent(onEvent);
    }

    public static void UnRegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
    {
        self.GetArchitecture().UnRegisterEvent(onEvent);
    }
}
