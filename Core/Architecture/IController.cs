/// <summary>
/// 视图接口
/// </summary>
public interface IController : IBelongToArchitecture, ICanGetSystem, ICanGetModel, ICanSendCommand, ICanRegisterEvent
{

}