using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.Domain;
using Sources.Infrastructure.StateMachines.States;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Controllers.Collectors
{
    public class CollectorTakeState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        public CollectorTakeState(ICollectorView collectorView, Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }
        public override void Enter()
        {
            //TODO из вьюшки кристалла
            _collector.TargetCrystalView.SetUnavailable();
        
            //TODO это тоже вьюшка кристалла но друггой метод
            _collector.TargetCrystalView.SetParent(_collectorView.CrystalTrunkPoint);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}