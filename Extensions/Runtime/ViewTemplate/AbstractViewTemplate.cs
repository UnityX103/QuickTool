using System;
using Sirenix.OdinInspector;

/// <summary>
/// 抽象视图层
/// </summary>
public abstract class AbstractViewTemplate : SerializedMonoBehaviour, IView
{
    [LabelText("自动初始化")] public bool autoInit = false;
    [LabelText("初始化后自动隐藏")] public bool autoHideObj = false;

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
    
    public void RegisterAutoDestoryEvent<T>(Action<T> action)
    {
        GetArchitecture().RegisterEvent<T>(e => { action.Invoke(e); }).UnRegisterWhenGameObjectDestory(gameObject);
    }


    private TempEventHelper _tempEventHelper;

    protected TempEventHelper TempEventHelper
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
   
    public void ClearTempEvent()
    {
        TempEventHelper.UnRegisterAllEvent();
    }


    public virtual void WhenDestroy()
    {
        ClearTempEvent();
    }
}

