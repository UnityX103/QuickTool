public interface ICanSendQuery : IBelongToArchitecture
{
}


public static class CanSendQueryExtension
{
    public static TResult SendQuery<TResult>(this ICanSendQuery self, IQuery<TResult> query)
    {
        return self.GetArchitecture().SendQuery(query);
    }


}
public interface IQuery<TResult> : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem,
    ICanSendQuery
{
    TResult Do();
}

public abstract class TQuery<R, T> : AbstractQuery<R>
{
    protected T Param;
    
    public TQuery(T t)
    {
        Param = t;
    }

    protected sealed override R OnDo()
    {
        return OnQuery(Param);
    }
    
    protected abstract R OnQuery(T t);
}

public abstract class TQuery<R, T, T2> : AbstractQuery<R>
{
    protected T Param;
    protected T2 Param2;
    
    public TQuery(T t, T2 t2)
    {
        Param = t;
        Param2 = t2;
    }

    protected sealed override R OnDo()
    {
        return OnQuery(Param, Param2);
    }
    
    protected abstract R OnQuery(T t, T2 t2);
}

public abstract class AbstractQuery<T> : IQuery<T>
{
    public T Do()
    {
        return OnDo();
    }

    protected abstract T OnDo();


    private IArchitecture mArchitecture;

    public IArchitecture GetArchitecture()
    {
        return mArchitecture;
    }

    public void SetArchitecture(IArchitecture architecture)
    {
        mArchitecture = architecture;
    }
}

