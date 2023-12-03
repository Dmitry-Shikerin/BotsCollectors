using Sources.Controllers.CommandCenters;
using Sources.ControllersInterfaces;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Views;
using Sources.Utils;

namespace Sources.Infrastructure.Factoryes.Presentations.UI
{
    public class TextUIFactory
    {
        public ITextUI Create(TextUI textUI, IObservableProperty observableProperty) 
        {
            TextUIPresenter textUIPresenter = new TextUIPresenter(textUI, observableProperty);
            
            textUI.Construct(textUIPresenter);

            return textUI;
        }
    }
}