using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 可绑定委托的属性
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class BindableProperty<T>
{
    public BindableProperty(T value = default(T))
    {
        mValue = value;
    }

    [LabelText("")]
    [SerializeField]
    private T mValue;

    public T Value
    {
        get => mValue;
        set
        {
            if (mValue == null || !mValue.Equals(value))
            {
                mValue = value;
                mOnValueChanged?.Invoke(mValue);
            }
        }
    }

    private Action<T> mOnValueChanged;

    /// <summary>
    /// 注册事件
    /// </summary>
    /// <param name="onValueChanged"></param>
    /// <returns></returns>
    public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged)
    {
        mOnValueChanged += onValueChanged;
        return new BindablePropertyUnRegister<T>
        {
            BindableProperty = this,
            OnValueChanged = onValueChanged
        };
    }
    
    /// <summary>
    /// 触发并注册事件
    /// </summary>
    /// <param name="onValueChanged"></param>
    /// <returns></returns>
    public IUnRegister TriggerOrRegisterOnValueChanged(Action<T> onValueChanged)
    {
        onValueChanged(Value);
        mOnValueChanged += onValueChanged;
        return new BindablePropertyUnRegister<T>
        {
            BindableProperty = this,
            OnValueChanged = onValueChanged
        };
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="onValueChanged"></param>
    public void UnRegisterOnValueChanged(Action<T> onValueChanged)
    {
        mOnValueChanged -= onValueChanged;
    }
}

/// <summary>
/// 可绑定委托的属性_的事件注销器
/// </summary>
/// <typeparam name="T"></typeparam>
public class BindablePropertyUnRegister<T> : IUnRegister 
{
    public BindableProperty<T> BindableProperty;
    public Action<T> OnValueChanged;

    public void UnRegister()
    {
        BindableProperty.UnRegisterOnValueChanged(OnValueChanged);
        BindableProperty = null;
        OnValueChanged = null;
    }
}
