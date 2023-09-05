using System;
using App.View;
using FrameworkExtensions.Mono.GameObject;
using Sirenix.OdinInspector;

/// <summary>
/// 抽象视图层
/// </summary>
public abstract class AbstractViewTemplate : SerializedMonoBehaviour, IView
{
    [LabelText("自动初始化")] public bool autoInit = false;
    [ShowIf(nameof(autoInit))]
    [LabelText("初始化后自动隐藏")] public bool autoHideObj = false;

    [ReadOnly]
    [ShowInInspector]
    private bool _isInit = false;

    private void Start()
    {
        if (autoInit)
        {
            Init();
        }
    }

    public void Init()
    {
        if (!_isInit)
        {
            _isInit = true;
            OnInit();
            if (autoHideObj)
            {
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnInit()
    {
    }

    public abstract IArchitecture GetArchitecture();
    
  

    private TempEventHelper _tempEventHelper;

    public TempEventHelper TempEventHelper
    {
        get
        {
            if (_tempEventHelper == null)
            {
                _tempEventHelper = new TempEventHelper(GetArchitecture());
            }

            return _tempEventHelper;
        }
    }

    public void RegisterTempEvent<T>(Action<T> action)
    {
        TempEventHelper.RegisterTempEvent(action);
    }
    
    protected T RegisterTempDestroyBindable<T>(BindableProperty<T> bindableProperty , Action<T> callback)
    {
        TempEventHelper.AddUnRegister(this.RegisterAutoDestroyBindable<T>(bindableProperty,callback));
        return bindableProperty.Value;
    }
   
    public void ClearTempEvent()
    {
        TempEventHelper.Clear();
    }
    
    
    public virtual void WhenDestroy()
    {
        ClearTempEvent();
    }
}

