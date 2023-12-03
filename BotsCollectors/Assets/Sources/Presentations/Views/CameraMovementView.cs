using Sources.Controllers.CameraMovements;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Presentations.Views
{
    public class CameraMovementView : PresentableView<CameraMovementPresenter>, ICameraMovementView
    {
        [SerializeField] private Transform _camera;

        private void Update() => 
            Presenter.Update();

        public void Move(Vector3 direction) => 
            transform.Translate(direction);

        public void Zoom(Vector3 direction) => 
            _camera.Translate(direction, Space.Self);

        public void Rotate(float angle) => 
            transform.rotation = Quaternion.Euler(0,angle,0);
    }
}
