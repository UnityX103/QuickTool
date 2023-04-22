using Cysharp.Threading.Tasks;

/// <summary>
/// 可以切换进入和退出的组件
/// </summary>
public interface ICanSwitchInOut
{
        bool IsIn { get; }


        UniTask In();

        UniTask Out();
}

public static class ICanSwitchInOutStaticExtension
{
        public static UniTask AutoInOut(this ICanSwitchInOut self,bool isIn)
        {
                return isIn ? self.In() : self.Out();
        }
}