/// <summary>
/// 允许发送事件
/// </summary>
public interface ICanSendEvent : IBelongToArchitecture
{

}

/// <summary>
/// 允许发送事件的静态拓展
/// </summary>
public static class CanSendEventExtension
{
    public static void SendEvent<T>(this ICanSendEvent self) where T : new()
    {
        self.GetArchitecture().SendEvent<T>();
    }

    public static void SendEvent<T>(this ICanSendEvent self, T e)
    {
        self.GetArchitecture().SendEvent<T>(e);
    }
}
