using System;
using System.Collections.Generic;
using Sources.InfrastructureInterfaces.Services;
using Sources.Presentations.Views;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class ObjectPool<T> : IObjectPool where T : MonoBehaviour
    {
        private readonly Queue<T> _objects = new Queue<T>();
        private readonly Transform _parent;

        public event Action<int> ObjectCountChanged;

        public ObjectPool()
        {
            _parent = new GameObject($"Pool of {typeof(T).Name}").transform;
        }
        
        public TType Get<TType>() where TType : MonoBehaviour
        {
            //TODO нужно ли здесь кидать эксепшены?
            if (_objects.Count == 0)
                return null;

            if (_objects.Dequeue() is not TType @object)
                return null;

            @object.transform.SetParent(null);
            ObjectCountChanged?.Invoke(_objects.Count);
            return @object;
        }

        public void Return(PoolableObject poolableObject)
        {
            if(poolableObject.TryGetComponent(out T @object) == false)
                return;
            
            poolableObject.transform.SetParent(_parent);
            _objects.Enqueue(@object);
            ObjectCountChanged?.Invoke(_objects.Count);
        }
    }
}