using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当游戏物体被销毁时，若使用了obj.Destroy则会调用WhenDestroy
/// </summary>
public interface ITriggerWhenDestroy
{
    /// <summary>
    /// 当游戏物体被销毁时，若使用了obj.Destroy则会调用此方法
    /// </summary>
    void WhenDestroy();
}

