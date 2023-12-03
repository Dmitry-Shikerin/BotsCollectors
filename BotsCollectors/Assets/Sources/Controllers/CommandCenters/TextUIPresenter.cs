using System;
using Sources.ControllersInterfaces;
using Sources.PresentationsInterfaces.Views;
using Sources.Utils;

namespace Sources.Controllers.CommandCenters
{
    public class TextUIPresenter : IPresenter
    {
        private readonly ITextUI _textUI;
        private readonly IObservableProperty _observableProperty;

        public TextUIPresenter(ITextUI textUI, IObservableProperty observableProperty)
        {
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _observableProperty = observableProperty ?? 
                                  throw new ArgumentNullException(nameof(observableProperty));
        }

        public void Enable() => 
            _observableProperty.Changed += OnPropertyChanged;

        public void Disable() => 
            _observableProperty.Changed -= OnPropertyChanged;

        private void OnPropertyChanged() => 
            _textUI.SetText(_observableProperty.StringValue);
    }
}