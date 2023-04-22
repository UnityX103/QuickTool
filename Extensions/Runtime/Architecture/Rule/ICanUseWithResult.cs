public interface ICanUseWithResult 
{
    bool CanUse { get; }

    bool Open();
    bool Close();
}