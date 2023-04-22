

public class E_Save : TEvent<string>
{
    public  E_Save(string value) : base(value)
    {
    }
}

public class E_Load : TEvent<string>
{
    public E_Load(string value) : base(value)
    {
    }
}