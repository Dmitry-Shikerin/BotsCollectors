using System;

namespace Sources.Utils
{
    public class ObservableProperty<T> : IObservableProperty
    {
        private T _value;
        
        public ObservableProperty(T value = default)
        {
            _value = value;
        }
        
        public event Action Changed;

        public string StringValue => Value.ToString();

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed?.Invoke();
            }
        }
    }
}