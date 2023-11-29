using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Domain;
using Sources.Infrastructure.StateMachines.States;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.PresentationsInterfaces.Vievs;

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
            //TODO покашто здесь
            _collector.CurrentCommandCenterView.TakeCrystal(_collector.TargetCrystalView);
        
            _collector.SetTarget(null);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}