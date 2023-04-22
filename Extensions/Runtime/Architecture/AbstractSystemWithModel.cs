namespace Framework.Architecture
{
    public abstract class AbstractSystemWithModel<T> : AbstractSystem where T : class, IModel
    {
        protected T Model { get; private set; }

        protected override void OnInit()
        {
            Model = this.GetModel<T>();
        }
    }
}