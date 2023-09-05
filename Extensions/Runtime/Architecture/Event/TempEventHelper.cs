using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEventHelper : ICanSendEvent
{
    readonly HashSet<IUnRegister> _unRegisters = new HashSet<IUnRegister>();

    public void RemoveUnRegister(IUnRegister unRegister)
    {
        _unRegisters.Remove(unRegister);
    }

    public void AddUnRegister(IUnRegister unRegister)
    {
        _unRegisters.Add(unRegister);
    }
    
    public void RegisterTempEvent<T>(Action<T> e)
    {
        var unRegister = _architecture.RegisterEvent(e);
        _unRegisters.Add(unRegister);
    }

    public void RegisterBindableProperty<T>(BindableProperty<T> bindableProperty, Action<T> action)
    {
        var ur = bindableProperty.RegisterOnValueChanged(action);
        _unRegisters.Add(ur);
    }

    public void SendEvent<T>(T e)
    {
        _architecture.SendEvent(e);
    }


    public void Clear()
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

    public IArchitecture GetArchitecture()
    {
        return _architecture;
    }
}
