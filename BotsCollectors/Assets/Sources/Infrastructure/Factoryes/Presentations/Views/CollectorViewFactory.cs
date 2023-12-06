using System;
using JetBrains.Annotations;
using Sources.Controllers.Collectors;
using Sources.Domain;
using Sources.Domain.Collectors;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factoryes.Presentations.Views
{
    public class CollectorViewFactory : ICollectorViewFactory
    {
        private const string PrefabCollectorPath = "Prefabs/Collector";
        
        private readonly CollectorPresenterFactory _collectorPresenterFactory;

        public ICollectorView Create(ICommandCenterView commandCenterView, Vector3 spawnPosition)
        {
            CollectorView prefab = Resources.Load<CollectorView>(PrefabCollectorPath);
            CollectorView collectorView = Object.Instantiate(prefab);

            Collector collector = new Collector();
            collector.SetCommandCenterView(commandCenterView);
            CollectorPresenterFactory collectorPresenterFactory =
                new CollectorPresenterFactory();
            CollectorPresenter collectorPresenter = collectorPresenterFactory.Create(
                collectorView, collector);
            collectorView.Construct(collectorPresenter);
            collectorView.SetPosition(spawnPosition);

            return collectorView;
        }
    }
}