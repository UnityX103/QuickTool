using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
/// <summary>
/// 可绑定委托的集合
/// </summary>
/// <typeparam name="T"></typeparam>
public class BindableList<T>
{
    [SerializeField]
    /// <summary>
    /// 封装集合
    /// </summary>
    private List<T> mList = new List<T>();

    public List<T> SetList
    {
        set
        {
            mListChangedAction?.Invoke(value);
            mList= value;
        }
    }
    
    public void Foreach(Action<T> action)
    {
        foreach (var item in List)
        {
            action?.Invoke(item);
        }
    }
    
    /// <summary>
    /// 集合数量
    /// </summary>
    public int Count => List.Count;

    /// <summary>
    /// 修改元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T this[int index]
    {
        get => List[index];
        set
        {
            List[index] = value;
            mReplaceAction?.Invoke(List[index]);
        }
    }

    /// <summary>
    /// 集合
    /// </summary>
    public List<T> List
    {
        get => mList;
    }

   
    /// <summary>
    /// 添加元素
    /// </summary>
    /// <param name="element"></param>
    public void Add(T element)
    {
        List.Add(element);
        mAddAction?.Invoke(element);
        ChangeCountAction();
    }
    
    /// <summary>
    /// 长度改变事件
    /// </summary>
    private void ChangeCountAction()
    {
        mCountChangeAction?.Invoke(Count);
    }

    /// <summary>
    /// 移除元素
    /// </summary>
    /// <param name="element"></param>
    public void Remove(T element)
    {
        List.Remove(element);
        mRemoveAction?.Invoke(element);
        ChangeCountAction();
    }

    /// <summary>
    /// 清空集合
    /// </summary>
    public void Clear()
    {
        List.Clear();
        mClearAction?.Invoke();
        ChangeCountAction();
    }

    #region Action
    /// <summary>
    /// 添加元素的委托
    /// </summary>
    private Action<T> mAddAction;
    /// <summary>
    /// 移除元素的委托
    /// </summary>
    private Action<T> mRemoveAction;
    /// <summary>
    /// 替换集合元素委托
    /// </summary>
    private Action<T> mReplaceAction;
    /// <summary>
    /// 集合长度变更委托
    /// </summary>
    private Action<int> mCountChangeAction;
    /// <summary>
    /// 清空集合委托
    /// </summary>
    private Action mClearAction;

    /// <summary>
    /// 清空集合委托
    /// </summary>
    private Action<List<T>> mListChangedAction;
    public IUnRegister RegisterAddAction(Action<T> action)
    {
        mAddAction += action;

        return new BLAddUnRegister<T>(this, action);
    }

    public void UnRegisterAddAction(Action<T> action)
    {
        mAddAction -= action;
    }

    public IUnRegister RegisterRemoveAction(Action<T> action)
    {
        mRemoveAction += action;

        return new BLRemoveUnRegister<T>(this, action);
    }

    public void UnRegisterRemoveAction(Action<T> action)
    {
        mRemoveAction -= action;
    }

    public IUnRegister RegisterReplaceAction(Action<T> action)
    {
        mReplaceAction += action;

        return new BLReplaceUnRegister<T>(this, action);
    }
    public void UnRegisterReplaceAction(Action<T> action)
    {
        mReplaceAction -= action;
    }
    
    
    
    public IUnRegister RegisterClearAction(Action action)
    {
        mClearAction += action;

        return new BLClearUnRegister<T>(this, action);
    }
    public void UnRegisterClearAction(Action action)
    {
        mClearAction -= action;
    }

    public IUnRegister RegisterListChangedAction(Action<List<T>> action)
    {
        mListChangedAction += action;
        return new BLListChangedUnRegister<T>(this, action);
    }
    public IUnRegister RegisterAndInitListChangedAction(Action<List<T>> action)
    {
        mListChangedAction += action;
        action?.Invoke(List);
        return new BLListChangedUnRegister<T>(this, action);
    }
    public void UnRegisterListChangedAction(Action<List<T>> action)
    {
        mListChangedAction -= action;
    }
    #endregion
}

#region UnRegister
public class BindableListUnRegister<T,T1> : IUnRegister
{
    public BindableListUnRegister(BindableList<T> bindableList,Action<T1> action)
    {
        mAction = action;
        mBindableList = bindableList;
    }

    public BindableList<T> mBindableList;
    public Action<T1> mAction;

    public virtual void UnRegister()
    {
    }
}

public class BindableListUnRegister<T> : IUnRegister
{
    public BindableListUnRegister(BindableList<T> bindableList,Action<T> action)
    {
        mAction = action;
        mBindableList = bindableList;
    }

    public BindableList<T> mBindableList;
    public Action<T> mAction;


    public virtual void UnRegister()
    {
        
    }
}


public class BindableListUnRegisterNoParam<T>: IUnRegister
{
    public BindableListUnRegisterNoParam(BindableList<T> bindableList,Action action)
    {
        mAction = action;
        mBindableList = bindableList;
    }

    public BindableList<T> mBindableList;
    public Action mAction;


    public virtual void UnRegister()
    {
        
    }
}
public class BLAddUnRegister<T> : BindableListUnRegister<T>
{
    public BLAddUnRegister(BindableList<T> bindableList, Action<T> action):base(bindableList,action)
    {

    }

    public override void UnRegister()
    {
        mBindableList.UnRegisterAddAction(mAction);
    }
}

public class BLRemoveUnRegister<T> : BindableListUnRegister<T>
{
    public BLRemoveUnRegister(BindableList<T> bindableList, Action<T> action) : base(bindableList, action)
    {

    }

    public override void UnRegister()
    {
        mBindableList.UnRegisterRemoveAction(mAction);
    }
}

public class BLReplaceUnRegister<T> : BindableListUnRegister<T>
{
    public BLReplaceUnRegister(BindableList<T> bindableList, Action<T> action) : base(bindableList, action)
    {

    }

    public override void UnRegister()
    {
        mBindableList.UnRegisterReplaceAction(mAction);
    }
}

public class BLClearUnRegister<T> : BindableListUnRegisterNoParam<T>
{
    public BLClearUnRegister(BindableList<T> bindableList, Action action) : base(bindableList, action)
    {

    }

    public override void UnRegister()
    {
        mBindableList.UnRegisterClearAction(mAction);
    }
}

public class BLListChangedUnRegister<T> : BindableListUnRegister<T,List<T>>
{
    public BLListChangedUnRegister(BindableList<T> bindableList, Action<List<T>> action) : base(bindableList, action)
    {

    }

    public override void UnRegister()
    {
        mBindableList.UnRegisterListChangedAction(mAction);
    }
}
#endregion

