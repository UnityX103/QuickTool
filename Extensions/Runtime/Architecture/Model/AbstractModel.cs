public  abstract  partial class AbstractModel 
{
      protected IStorage storage;
      partial void Init()
      {
            storage = this.GetUtility<IStorage>();
      }

}