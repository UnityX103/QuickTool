using System.Threading;
using UnityEngine;

public class AutoToken
{
    CancellationTokenSource source = new CancellationTokenSource();
    public CancellationToken Token => Source.Token;
    public CancellationTokenSource Source
    {
        get
        {
            if (source == null)
            {
                source = new CancellationTokenSource();
            }
            return source;
        }
        private set => source = value;
    }

    public void Cancel()
    {
        
        Source.Cancel();
        Source = new CancellationTokenSource();
    }
}
