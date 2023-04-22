using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件系统（底层向顶层）
/// </summary>
public interface ITypeEventSystem
{
    /// <summary>
    /// 发送事件（自动创建）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void Send<T>() where T : new();

    /// <summary>
    /// 发送事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    void Send<T>(T e);

    //UniTask SendAsync<T>(T e);

    IUnRegister Register<T>(Action<T> onEvent);

    void UnRegister<T>(Action<T> onEvent);
    
    
    
    

 
}

/// <summary>
/// 事件系统（实现）
/// </summary>
public class TypeEventSystem : ITypeEventSystem
{

    public interface IRegistrations
    {

    }


    public class Registrations<T> : IRegistrations
    {
        public Action<T> OnEvent = e => { };
    }

    //public interface IRegistrationsAsync : IRegistrations
    //{

    //}

    //public class RegistrationsAsync<T>: IRegistrationsAsync
    //{
    //    public UniTask<T> OnEventAsync = new UniTask<T>();
    //}
        


    Dictionary<Type, IRegistrations> mEventRegistration = new Dictionary<Type, IRegistrations>();

    public IUnRegister Register<T>(Action<T> onEvent)
    {
        var type = typeof(T);
        IRegistrations registrations;

        if (!mEventRegistration.TryGetValue(type,out registrations))
        {
            registrations = new Registrations<T>();
            mEventRegistration.Add(type, registrations);
        }

        (registrations as Registrations<T>).OnEvent += onEvent;

        return new TypeEventSystemUnRegister<T>()
        {
            OnEvent = onEvent,
            TypeEventSystem = this
        };
    }
    public void UnRegister<T>(Action<T> onEvent)
    {
        var type = typeof(T);
        IRegistrations registrations;

        if (mEventRegistration.TryGetValue(type, out registrations))
        {
            (registrations as Registrations<T>).OnEvent -= onEvent;
        }
    }


    public void Send<T>() where T : new()
    {
        var e = new T();
        Send(e);
    }

    public void Send<T>(T e)
    {
        var type = typeof(T);
        IRegistrations registrations;

        if (mEventRegistration.TryGetValue(type, out registrations))
        {
            (registrations as Registrations<T>).OnEvent(e);
        }
    }

    //public async UniTask SendAsync<T>(T e)
    //{
    //    var type = typeof(T);
    //    IRegistrations registrations;
    //    if (mEventRegistration.TryGetValue(type, out registrations))
    //    {
    //        await (registrations as Registrations<T>).OnEvent(e);
    //    }
    //}
}

#region UnRegister
/// <summary>
/// 注销接口
/// </summary>
public interface IUnRegister
{
    void UnRegister();
}

public class TypeEventSystemUnRegister<T> : IUnRegister
{
    public ITypeEventSystem TypeEventSystem;
    public Action<T> OnEvent;

    public void UnRegister()
    {
        TypeEventSystem.UnRegister<T>(OnEvent);
    }
}

/// <summary>
/// 自动注销事件触发器拓展方法
/// </summary>
public static class UnRegisterExtension
{

    /// <summary>
    /// 当gameobject销毁时自动注销事件
    /// </summary>
    /// <param name="register"></param>
    /// <param name="gameObject"></param>
    public static void UnRegisterWhenGameObjectDestory(this IUnRegister register, GameObject gameObject)
    {
        if (!gameObject.TryGetComponent<UnRegisterOnDestoryTrigger>(out var component))
        {
            component = gameObject.AddComponent<UnRegisterOnDestoryTrigger>();
        }
        UnRegisterOnDestoryTrigger trigger = component;
        trigger.AddUnRegister(register);
    }
}


/// <summary>
/// 自动注销事件触发器
/// </summary>
public class UnRegisterOnDestoryTrigger : MonoBehaviour,ITriggerWhenDestroy
{
    /// <summary>
    /// 存储了所有可注销事件
    /// </summary>
    private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

    private HashSet<Action> mActions = new HashSet<Action>();

    public void AddAction(Action action)
    {
        mActions.Add(action);
    }

    /// <summary>
    /// 添加可注销事件
    /// </summary>
    /// <param name="unRegister"></param>
    public void AddUnRegister(IUnRegister unRegister)
    {
        mUnRegisters.Add(unRegister);
    }

    public void WhenDestroy()
    {
        foreach (var unRegister in mUnRegisters)
        {
            unRegister.UnRegister();
        }

        mUnRegisters.Clear();

        foreach (var action in mActions)
        {
            action.Invoke();
        }
        
        mActions.Clear();
    }

}


#endregion

