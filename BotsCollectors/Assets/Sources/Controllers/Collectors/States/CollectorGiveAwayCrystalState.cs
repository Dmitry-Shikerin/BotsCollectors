using System;
using Sources.Domain.Collectors;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Controllers.Collectors.States
{
    public class CollectorGiveAwayCrystalState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        public CollectorGiveAwayCrystalState(ICollectorView collectorView, Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public override void Enter()
        {
            _collector.CommandCenter.AddExtractedResources(_collector.TargetCrystalView);
            _collector.SetIdle(true);
            _collector.SetTarget(null);
        }

        public override void Exit() => 
            _collector.CommandCenter.AddCollector(_collectorView);
    }
}