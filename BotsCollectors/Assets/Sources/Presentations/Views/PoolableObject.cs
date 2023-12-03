using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Presentations.Views
{
    public class PoolableObject : MonoBehaviour
    {
        private IObjectPool _pool;
        
        public void SetPool(IObjectPool pool) => 
            _pool = pool;

        public void ReturnTooPool() => 
            _pool.Return(this);
    }
}