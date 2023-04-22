namespace UnityEngine.Timeline
{
    public interface IDynamicSelectionTargets
    {
        public string ObjKey { get; }
        public bool NeedDynamicSelection { get; }
    }
}