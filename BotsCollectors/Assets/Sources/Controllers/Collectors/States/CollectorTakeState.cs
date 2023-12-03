using System;
using Sources.Domain.Collectors;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.Collectors.States
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
            _collector.TargetCrystalView.SetUnavailable();
            _collector.TargetCrystalView.SetParent(_collectorView.CrystalTrunkPoint);
            _collector.TargetCrystalView.SetLocalPosition(Vector3.zero);
        }
    }
}