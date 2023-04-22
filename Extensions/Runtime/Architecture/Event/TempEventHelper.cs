using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEventHelper
{
    readonly HashSet<IUnRegister> _unRegisters = new HashSet<IUnRegister>();
    public void RegisterTempEvent<T>(Action<T> e)
    {
        var unRegister = _architecture.RegisterEvent(e);
        _unRegisters.Add(unRegister);
    }

    public void SendEvent<T>(T e)
    {
        _architecture.SendEvent(e);
    }
    
 
    public void UnRegisterAllEvent()
    {

        foreach (var item in _unRegisters)
        {
            item.UnRegister();
        }

        _unRegisters.Clear();
    }

    IArchitecture _architecture;

    public TempEventHelper(IArchitecture architecture)
    {
        _architecture = architecture;
    }
 
}
