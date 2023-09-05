using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 命令接口（顶层向底层）
/// </summary>
public interface ICommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanGetUtility,
    ICanSendEvent, ICanSendCommand,ICanSendQuery
{
    void Execute();
}
public interface ICommand<TResult> : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel,
    ICanGetUtility,
    ICanSendEvent, ICanSendCommand, ICanSendQuery
{
    TResult Execute();
}


[System.Serializable]
public class BaseCommand : ICommand
{
    public uint Results { get; protected set; } = 1;

    public bool Available;
    private IArchitecture mArchitecture;
    protected bool mAutoRelease ;

    public BaseCommand()
    {
        bool mAutoRelease = true;
    }

    void ICommand.Execute()
    {
        Execute();
    }

    public virtual void TurnOffAutoRelease()
    {
        mAutoRelease = false;
    }

    protected virtual void Execute()
    {
    }

    public virtual void Release()
    {
        mAutoRelease = true;
    }

    public IArchitecture GetArchitecture()
    {
        return mArchitecture;
    }

    void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }
}


public abstract class AbstractCommand : BaseCommand
{
    protected override void Execute()
    {
        base.Execute();
        OnExecute();
        if (mAutoRelease)
            this.DoRelease();
    }

    protected abstract void OnExecute();

    public sealed override void Release()
    {
        base.Release();
        OnDispose();
    }

    protected virtual void OnDispose()
    {
    }
}