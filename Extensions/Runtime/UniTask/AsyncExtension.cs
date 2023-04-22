using System;
using Cysharp.Threading.Tasks;

public static class AsyncExtension
{
    public static UniTask WaitUntil(Func<bool> func, AutoToken token = null,
        PlayerLoopTiming time = PlayerLoopTiming.Update)
    {
        var t = token?.Token ?? default;
        return UniTask.WaitUntil(func, time, t);
    }
    
    public static UniTask<T> AddAutoToken<T>(this UniTask<T> task, AutoToken autoToken)
    {
        return task.AttachExternalCancellation(autoToken.Token);
    }

    public static UniTask AddAutoToken(this UniTask task, AutoToken autoToken)
    {
        return task.AttachExternalCancellation(autoToken.Token);
    }


}