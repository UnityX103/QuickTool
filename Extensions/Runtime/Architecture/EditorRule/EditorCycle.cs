namespace FrameworkExtensions.Architecture.EditorRule
{
    
    public interface IEditorUpdateSimulate
    {
        void EditorUpdateSimulate(float deltaTime);
    }
    public interface IEditorStartSimulate : IEditorStopSimulate
    {
        void EditorStartSimulate();
    }
    public interface IEditorStopSimulate
    {
        void EditorStopSimulate();
    }
}