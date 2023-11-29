using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface IObjectPool
    {
        T Get<T>() where T : MonoBehaviour;
        void Return(PoolableObject poolableObject);
    }
}