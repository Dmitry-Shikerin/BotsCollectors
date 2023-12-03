using UnityEngine;

namespace Sources.PresentationsInterfaces.Views
{
    public interface ICameraMovementView
    {
        void Move(Vector3 direction);
        void Zoom(Vector3 direction);
        void Rotate(float direction);

    }
}