using System;
using Sources.InfrastructureInterfaces.Services;
using Sources.Presentations.Views;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factoryes.Presentations.Views
{
    public class CrystalViewFactory
    {
        private readonly CrystalView _prefab;
        private readonly IObjectPool _objectPool;

        public CrystalViewFactory(CrystalView prefab, IObjectPool objectPool)
        {
            _prefab = prefab ? prefab : throw new ArgumentNullException(nameof(prefab));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public CrystalView Create()
        {
            var view = Object.Instantiate(_prefab);
            view.AddComponent<PoolableObject>().SetPool(_objectPool);

            return view;
        }
    }
}