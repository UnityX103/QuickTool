using UnityEngine;

public static class GameTool
{
    public static void LogError(object a, object b)
    {
        LogError(a.ToString()+"："+b.ToString());
    }

    public static void LogError(string str)
    {
        Debug.LogWarning($"<color=red>Error__{str}</color>");
    }

    public static void LogEvent(string str)
    {
        Debug.LogWarning($"<color=green>Event__{str}</color>");
    }
    
    public static void LogAudio(string str)
    {
        Debug.LogWarning($"<color=white>Audio__{str}</color>");
    }
    
    public static void Log(object a, object b)
    {
        Log(a.ToString() + "：" + b.ToString());
    }
    
    public static bool CanLog = true;

    public static void Log(object str)
    {
        if (CanLog)
        {
            Debug.Log(str);
        }
    }
    public static void LogPositive(object str)
    {
        Debug.Log(string.Format($"<color=yellow>{str}</color>"));
    }
  
}

