using System;
using Sources.Domain.Collectors;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Controllers.Collectors.States
{
    public class CollectorMoveTowardsCrystalState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        public CollectorMoveTowardsCrystalState(ICollectorView collectorView, Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }
        
        public override void Enter() => 
            _collectorView.SetDestination(_collector.TargetPosition);
    }
}