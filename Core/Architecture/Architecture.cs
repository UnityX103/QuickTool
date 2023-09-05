using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;
using Framework.Extensions.Runtime.Architecture;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable CS0693 // 类型参数与外部类型中的类型参数同名
public interface IArchitecture
{
    /// <summary>
    /// 注册系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    void RegisterSystem<T>(T instance) where T : ISystem;

    /// <summary>
    /// 注册model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    void RegisterModel<T>(T instance) where T : IModel;

    /// <summary>
    /// 注册工具
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    void RegisterUtility<T>(T instance) where T : IUtility;

    T GetSystem<T>() where T : class, ISystem;

    ISystem GetSystem(Type system);
    
    /// <summary>
    /// 获取model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetModel<T>() where T : class, IModel;

    IModel GetModel(Type model);

    /// <summary>
    /// 获取工具
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    T GetUtility<T>() where T : class, IUtility;

    /// <summary>
    /// 发送命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    T GetCommand<T>() where T : BaseCommand, new();

    
    TResult SendQuery<TResult>(IQuery<TResult> query);
    
    void SendCommand(ICommand command);

    
    void ReleaseCommand<T>(T command) where T : BaseCommand, new();

    
    
    /// <summary>
    /// 发送事件
    /// </summary>
    void SendEvent<T>() where T : new();

    /// <summary>
    /// 发送事件
    /// </summary>
    void SendEvent<T>(T e);

    /// <summary>
    /// 注册事件
    /// </summary>
    IUnRegister RegisterEvent<T>(Action<T> onEvent);

    /// <summary>
    /// 注销事件
    /// </summary>
    void UnRegisterEvent<T>(Action<T> onEvent);
}

/// <summary>
/// 架构
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
{
    private static T mArchitecture = null;
    private Dictionary<Type, ICommandPool> mCommandPools;

    public Dictionary<Type, ICommandPool> CommandPools => mCommandPools ??= new Dictionary<Type, ICommandPool>();

    public static IArchitecture Interface
    {
        get
        {
            if (mArchitecture == null)
            {
                MakeSureArchitecture();
            }

            return mArchitecture;
        }
    }

    public static void Dispose()
    {
        mArchitecture = null;
    }

    /// <summary>
    /// 增加注册的委托
    /// </summary>
    public static Action<T> OnRegisterPatch = architecture => { };

    /// <summary>
    /// 是否初始化完成
    /// </summary>
    private bool mInited = false;

    /// <summary>
    /// 缓存初始化的system
    /// </summary>
    private List<ISystem> mSystems = new List<ISystem>();

    /// <summary>
    /// 缓存初始化的model
    /// </summary>
    private List<IModel> mModels = new List<IModel>();

    private static void MakeSureArchitecture()
    {
        mArchitecture = new T();
        mArchitecture.Init();

        OnRegisterPatch?.Invoke(mArchitecture);

        //初始化model
        foreach (var model in mArchitecture.mModels)
        {
            model.Init();
        }

        //清空缓存
        mArchitecture.mModels.Clear();

        //初始化system
        foreach (var system in mArchitecture.mSystems)
        {
            system.Init();
        }

        //清空缓存
        mArchitecture.mModels.Clear();

        mArchitecture.mInited = true;
    }


    protected abstract void Init();

    IOCContainer mContainer = new IOCContainer();

    /// <summary>
    /// 注册模块
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="instance"></param>
    public virtual void RegisterModel<T>(T instance) where T : IModel
    {
        //复制model的架构
        instance.SetArchitecture(this);
        mContainer.Register(instance);

        //判断是否初始化过
        if (!mInited)
        {
            mModels.Add(instance);
        }
        else
        {
            instance.Init();
        }
    }

    public void RegisterUtility<T>(T instance) where T: IUtility
    {
        mContainer.Register(instance);
    }


    public virtual void RegisterSystem<T>(T instance) where T : ISystem
    {
        instance.SetArchitecture(this);
        mContainer.Register(instance);

        if (mInited)
        {
            instance.Init();
        }
        else
        {
            mSystems.Add(instance);
        }
    }

    public ISystem GetSystem(Type system)
    {
        return mContainer.Get(system) as ISystem;
    }

    public T GetModel<T>() where T : class, IModel
    {
        return mContainer.Get<T>();
    }

    public IModel GetModel(Type model)
    {
        return mContainer.Get(model) as IModel;
    }

    public T GetSystem<T>() where T : class, ISystem
    {
        return mContainer.Get<T>();
    }

    public T GetUtility<T>() where T : class, IUtility
    {
        return mContainer.Get<T>();
    }

    
    public T GetCommand<T>() where T : BaseCommand, new()
    {
        if (!CommandPools.TryGetValue(typeof(T), out var pool))
        {
            pool = new CommandPool<T>();
            CommandPools.Add(typeof(T), pool);
        }

        var command = pool.Get<T>();

        ((ICommand)command).SetArchitecture(this);
        return command;
    }

    public TResult SendQuery<TResult>(IQuery<TResult> query)
    {
        return DoQuery<TResult>(query);
    }
    protected virtual TResult DoQuery<TResult>(IQuery<TResult> query)
    {
        query.SetArchitecture(this);
        return query.Do();
    }

    public void SendCommand(ICommand command)
    {
        command.Execute();
    }

    public void ReleaseCommand<T>(T command) where  T : BaseCommand ,new()
    {
        if (CommandPools.TryGetValue(command.GetType(), out var pool))
        {
            pool.Release(command);
        }
    }


    /// <summary>
    /// 事件系统
    /// </summary>
    private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();

    public void SendEvent<T>() where T : new()

    {
        mTypeEventSystem.Send<T>();
    }

    public void SendEvent<T>(T e)
    {
        mTypeEventSystem.Send(e);
    }


    public IUnRegister RegisterEvent<T>(Action<T> onEvent)
    {
        return mTypeEventSystem.Register(onEvent);
    }

    public void UnRegisterEvent<T>(Action<T> onEvent)
    {
        mTypeEventSystem.UnRegister(onEvent);
    }
}
#pragma warning restore CS0693 // 类型参数与外部类型中的类型参数同名