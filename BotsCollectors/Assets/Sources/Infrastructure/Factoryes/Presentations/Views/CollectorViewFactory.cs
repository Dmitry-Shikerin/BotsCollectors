using System;
using JetBrains.Annotations;
using Sources.Controllers.Collectors;
using Sources.Domain;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.Infrastructure.StateMachines;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Infrastructure.Factoryes.Presentations.Views
{
    public class CollectorViewFactory
    {
        private readonly CollectorPresenterFactory _collectorPresenterFactory;

        public CollectorViewFactory(CollectorPresenterFactory collectorPresenterFactory)
        {
            _collectorPresenterFactory = collectorPresenterFactory ?? 
                                         throw new ArgumentNullException(nameof(collectorPresenterFactory));
        }

        //TODO покашто так, потом перенесу в презентер базы
        public ICollectorView Create(CollectorView collectorView, Collector collector)
        {
            CollectorPresenter collectorPresenter =
                _collectorPresenterFactory.Create(collectorView, collector);
            
            collectorView.Construct(collectorPresenter);

            return collectorView;
        }
    }
}