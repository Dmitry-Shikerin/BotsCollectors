using System;
using Sources.Domain.Collectors;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Controllers.Collectors.States
{
    public class CollectorIdleState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;
        
        public CollectorIdleState(ICollectorView collectorView, Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public override void Enter() => 
            _collector.SetIdle(true);

        public override void Exit() => 
            _collector.SetIdle(false);
    }
}