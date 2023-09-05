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

    public static R DoSend<R, T>(this RCommand<R, T> self, T value)
    {
        self.TurnOffAutoRelease();
        self.SetValue(value).Send();
        var res = self.Result;
        self.DoRelease();
        return res;
    }
    


    public static T GetCommand<T>(this ICanSendCommand self) where T : BaseCommand, new()
    {
        return self.GetArchitecture().GetCommand<T>();
    }

    
    public static T GetLockCommand<T>(this IArchitecture self) where T : BaseCommand, new()
    {
        var cmd =  self.GetCommand<T>();
        cmd.TurnOffAutoRelease();
        return cmd;
    }
    
    public static T GetLockCommand<T>(this ICanSendCommand self) where T : BaseCommand, new()
    {
        var cmd =  self.GetCommand<T>();
        cmd.TurnOffAutoRelease();
        return cmd;
    }
    public static void DoSend(this AbstractCommand self) 
    {
        self.Send();
    }
    public static void DoSend<T>(this TCommand<T> self,T value) 
    {
        self.SetValue(value).Send();
    }

    public static void DoSend<T1, T2>(this TCommand<T1,T2> self,T1 value,T2 value2) 
    {
        self.SetValue(value,value2).Send();
    }
    public static void DoSend<T1, T2,T3>(this TCommand<T1,T2,T3> self,T1 value,T2 value2,T3 value3) 
    {
        self.SetValue(value,value2,value3).Send();
    }
    
    public static void DoSend<T1, T2,T3,T4>(this TCommand<T1,T2,T3,T4> self,T1 value,T2 value2,T3 value3,T4 value4) 
    {
        self.SetValue(value,value2,value3,value4).Send();
    }
    
    public static void DoSend<T1, T2,T3,T4,T5>(this TCommand<T1,T2,T3,T4,T5> self,T1 value,T2 value2,T3 value3,T4 value4,T5 value5) 
    {
        self.SetValue(value,value2,value3,value4,value5).Send();
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
    public static void DoRelease(this BaseCommand self)
    {
        self.GetArchitecture().ReleaseCommand(self);
    }
    
    
}