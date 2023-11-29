using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Domain;
using Sources.Infrastructure.StateMachines.States;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Controllers.Collectors.States
{
    public class CollectorMoveTowardsCommandCenterState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        public CollectorMoveTowardsCommandCenterState(ICollectorView collectorView, Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }
        
        public override void Enter()
        {
            _collectorView.SetDestination(_collector.ParkingPoint);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}