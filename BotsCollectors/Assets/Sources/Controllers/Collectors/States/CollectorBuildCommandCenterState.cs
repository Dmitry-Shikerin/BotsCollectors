using System;
using Sources.Domain.Collectors;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Controllers.Collectors.States
{
    public class CollectorBuildCommandCenterState : FiniteState
    {
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        private CommandCenter _commandCenter;
        
        public CollectorBuildCommandCenterState(ICollectorView collectorView, 
            Collector collector)
        {
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public override void Enter()
        {
            _commandCenter = _collector.CommandCenterView.BuildCommandCenter();
            _collector.CommandCenterView.DestroyFlag();
            _collector.SetFlag(null);
            _collector.SetIdle(true);
            _collectorView.NavMeshAgent.stoppingDistance = 4;
        }

        public override void Exit()
        {
            _collector.CommandCenter.SetIsCollectorOnWay(false);
            _collector.SetCommandCenter(_commandCenter);
        }
    }
}