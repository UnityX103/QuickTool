using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel:IBelongToArchitecture,ICanSetArchitecture,ICanGetUtility,ICanSendEvent
{
    void Init();
}

public abstract partial class AbstractModel:IModel
{
    private IArchitecture mArchitecture;

    /// <summary>
    /// 获取架构（阉割）
    /// </summary>
    /// <returns></returns>
    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return mArchitecture;
    }

    /// <summary>
    /// 初始化（接口阉割）
    /// </summary>
    void IModel.Init()
    {
        Init();
        OnInit();
    }

     partial void Init();
  
    protected abstract void OnInit();

    /// <summary>
    /// 设置架构（阉割）
    /// </summary>
    /// <param name="architecture"></param>
    void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }
}
