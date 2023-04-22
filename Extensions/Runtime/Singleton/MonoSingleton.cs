using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class MonoSingleton<T> : AbstractViewTemplate where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (Application.isPlaying)
            {
                return _instance;
            }
            else
            {
                return GameObject.Find(typeof(T).ToString()).GetComponent<T>();
            }
        }
    }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }
}

public abstract class MonoSingleton_New<T> : AbstractViewTemplate   where T : MonoBehaviour  
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject temp = new GameObject($"{typeof(T)}");
                _instance = temp.AddComponent<T>();
            }
            return _instance;
        }
    }

    public virtual UniTask Release()
    {
        Destroy(gameObject);        
        _instance = null;
        return  UniTask.CompletedTask;
    }

}

