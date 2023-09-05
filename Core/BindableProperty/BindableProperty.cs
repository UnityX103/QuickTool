using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Object = UnityEngine.Object;

public interface IBindableProperty
{
    void Set(object value);
    IUnRegister RegisterOnValueChanged(Action<object> onValueChangedOrg, out object data);
}

/// <summary>
/// 可绑定委托的属性
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class BindableProperty<T> : IBindableProperty
{
    public BindableProperty(T value = default(T))
    {
        mValue = value;
    }

    [LabelText("值")] [SerializeField] protected T mValue;

    public virtual T Value
    {
        get => mValue;
        set
        {
            if (mValue == null || !mValue.Equals(value))
            {
                mValue = value;
                mOnValueChanged?.Invoke(mValue);
                mOnValueChangedOrg?.Invoke(mValue);
            }
        }
    }

    protected Action<T> mOnValueChanged;
    protected Action<object> mOnValueChangedOrg;

#if UNITY_EDITOR
    
    [Button("编辑器测试手动更新")]
    private void Update()
    {
        mOnValueChanged?.Invoke(mValue);
        mOnValueChangedOrg?.Invoke(mValue);
    }
#endif


    public void Set(object value)
    {
        Value = (T) value;
    }

    public IUnRegister RegisterOnValueChanged(Action<object> onValueChangedOrg, out object data)
    {
        mOnValueChangedOrg += onValueChangedOrg;
        data = mValue;
        return new BindablePropertyUnRegister<T>
        {
            BindableProperty = this,
            OnValueChangedOrg = onValueChangedOrg
        };
    }

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
    /// 注册事件
    /// </summary>
    /// <param name="onValueChanged"></param>
    /// <returns></returns>
    public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged,out T data)
    {
        mOnValueChanged += onValueChanged;
        data = mValue;
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

    public void UnRegisterOnValueChanged(Action<object> onValueChangedOrg)
    {
        mOnValueChangedOrg -= onValueChangedOrg;
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
    public Action<object> OnValueChangedOrg;

    public void UnRegister()
    {
        if (OnValueChanged != null)
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);
        }

        if (OnValueChangedOrg != null)
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChangedOrg);
        }

        BindableProperty = null;
        OnValueChanged = null;
    }
}