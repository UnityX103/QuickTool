using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 命令接口（顶层向底层）
/// </summary>
public interface ICommand : IBelongToArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanGetUtility,
    ICanSendEvent, ICanSendCommand
{
    void Execute();
}


public class BaseCommand : ICommand
{
    
    public bool Available;
    private IArchitecture mArchitecture;
    protected bool mAutoRelease = true;

    void ICommand.Execute()
    {
        Execute();
    }

    protected void TurnOffAutoRelease()
    {
        mAutoRelease = false;
    }

    protected virtual void Execute()
    {
    }

    public virtual void OnRelease()
    {
        Available = true;
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
            this.Release();
    }

    protected abstract void OnExecute();

    public override void OnRelease()
    {
        base.OnRelease();
        OnDispose();
    }

    protected abstract void OnDispose();
}