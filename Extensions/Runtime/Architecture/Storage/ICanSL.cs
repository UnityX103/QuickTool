public interface ICanSL : ICanRegisterEvent
{
    public void Save(string fileName);
    public void Load(string fileName);

    bool HasSaveData(string fileName);
}