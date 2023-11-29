using Sources.InfrastructureInterfaces.Services;
using UnityEngine;

namespace Sources.Presentations.Views
{
    //TODO для чего этот класс?
    public class PoolableObject : MonoBehaviour
    {
        private IObjectPool _pool;
        
        public PoolableObject SetPool(IObjectPool pool)
        {
            _pool = pool;

            //TODO нормально ли тут все сделано?
            return this;
        }

        public void ReturnTooPool() => 
            _pool.Return(this);
    }
}