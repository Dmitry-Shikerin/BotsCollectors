using System;
using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface IObjectPool
    {
        event Action<int> ObjectCountChanged;
        T Get<T>() where T : MonoBehaviour;
        void Return(PoolableObject poolableObject);
    }
}