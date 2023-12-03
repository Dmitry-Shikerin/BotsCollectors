using Sources.Controllers.CommandCenters;
using Sources.ControllersInterfaces;
using Sources.PresentationsInterfaces.Views;
using TMPro;
using UnityEngine;

namespace Sources.Presentations.Views
{
    public class TextUI : PresentableView<TextUIPresenter>, ITextUI
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public virtual void SetText(string text)
        {
            _text.text = text;
        }
    }
}