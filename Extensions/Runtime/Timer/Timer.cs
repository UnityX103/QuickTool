using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using FrameworkExtensions.Architecture;

public class Timer
{
    private float mTime;
    private Action action;
    private ICanUpdateApp _app;
    /// <summary>
    /// 创建一个计时器
    /// </summary>
    /// <param name="app">当前APP</param>
    /// <param name="time">等待时长</param>
    /// <param name="action">回掉</param>
    /// <param name="autoRun">自动运行</param>
    public Timer(ICanUpdateApp app, float time = 0, Action action = null, bool autoRun = true)
    {
        _app=app;
        mOriginTime = time;
        Action = action;
        if (autoRun)
        {
            Start();
        }
    }

    ~Timer()
    {
        if (isStart)
        {
            Stop();
        }
    }

    private bool isStart = false;

    public Action Action { get => action; set => action = value; }

    public void Start()
    {
        if (isStart)
        {
            Stop();
        }
        mTime = mOriginTime;
        _app.AddUpdate(StartTimer);
        isStart = true;
    }

    private float mOriginTime;

    private void StartTimer()
    {
        mTime -= Time.deltaTime;
        if (mTime <= 0)
        {
            Stop();
            Action?.Invoke();
        }
    }

    public void Stop()
    {
        isStart = false;
        _app.RemoveUpdate(StartTimer);
    }

    /// <summary>
    ///  等待一段时间
    /// </summary>
    /// <param name="time">多少秒</param>
    /// <returns></returns>
    public static UniTask Wait(float time)
    {
        return UniTask.Delay((int)(time * 1000));    
    }
}



