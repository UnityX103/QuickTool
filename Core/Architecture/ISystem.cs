using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISystem : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanGetUtility, ICanRegisterEvent, ICanSendEvent
{
    void Init();
}

public abstract partial class AbstractSystem : ISystem
{

    private IArchitecture mArchitecture = null;

    public IArchitecture GetArchitecture()
    {
        return mArchitecture;
    }

    public void SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }


    void ISystem.Init()
    {
        Init();
        OnInit();
    }

    partial void Init();

    protected abstract void OnInit();
  
}
