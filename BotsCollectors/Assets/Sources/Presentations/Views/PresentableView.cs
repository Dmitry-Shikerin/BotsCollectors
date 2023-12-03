using System;
using Sources.ControllersInterfaces;
using UnityEngine;

namespace Sources.Presentations.Views
{
    public class PresentableView<T> : MonoBehaviour where T : IPresenter
    {
        protected T Presenter { get; private set; }

        public void OnEnable() => 
            Presenter?.Enable();

        public void OnDisable() =>
            Presenter?.Disable();

        public virtual void Construct(T presenter)
        {
            Hide();
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            Show();
        }

        private void Hide() => 
            gameObject.SetActive(false);

        private void Show() => 
            gameObject.SetActive(true);
    }
}