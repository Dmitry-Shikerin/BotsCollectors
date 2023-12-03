using UnityEngine;

namespace Sources.PresentationsInterfaces.Views
{
    public interface ICrystalView
    {
        Vector3 Position { get; }
        
        void SetUnavailable();
        void SetParent(Transform parent);
        void RemoveParent();
        void ChangeLayerMask();
        void SetLocalPosition(Vector3 position);
        void Destroy();

    }
}