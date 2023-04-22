

public class TEvent<T>
{
    public T mValue;

    public TEvent(T value)
    {
        mValue = value;
        SetValue(value);
    }

    public void SetValue(T value)
    {
        mValue = value;
    }
}

public class TEvent<T1, T2>
{
    public T1 t1;
    public T2 t2;

    public TEvent(T1 _t1, T2 _t2)
    {
        t1 = _t1;
        t2 = _t2;
    }
}