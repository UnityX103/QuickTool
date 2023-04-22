using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Plugins.Framework.Core;
using UnityEngine;

/// <summary>
/// 允许发送命令
/// </summary>
public interface ICanSendCommand : IBelongToArchitecture
{
}

/// <summary>
/// 允许发送命令的静态扩展
/// </summary>
public static class CanSendCommandExtension
{
    public static T GetCommand<T>(this ICanSendCommand self) where T : BaseCommand, new()
    {
        return self.GetArchitecture().GetCommand<T>();
    }

    /// <summary>
    /// 发送命令
    /// </summary>
    /// <param name="self"></param>
    public static void Send(this ICommand self)
    {
        self.GetArchitecture().SendCommand(self);
    }

    /// <summary>
    /// 异步发送命令
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static UniTask Send(this ACommand self)
    {
        self.GetArchitecture().SendCommand(self);
        return self.Awaiter();
    }
    /// <summary>
    /// 忽略返回值 不等待
    /// </summary>
    /// <param name="self"></param>
    public static void ForgetSend(this ACommand self)
    {
          self.GetArchitecture().SendCommand(self);
    }
    public static void Release(this BaseCommand self)
    {
        self.GetArchitecture().ReleaseCommand(self);
    }
}