using System;

namespace Sources.Utils
{
    public interface IObservableProperty
    {
        event Action Changed;
        
        string StringValue { get; }
    }
}