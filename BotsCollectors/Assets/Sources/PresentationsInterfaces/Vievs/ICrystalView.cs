using UnityEngine;

namespace Sources.PresentationsInterfaces.Vievs
{
    public interface ICrystalView
    {
        Vector3 Position { get; }
        
        void SetUnavailable();
        void SetParent(Transform parent);
        void RemoveParent();
        void Hide();

    }
}