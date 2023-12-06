using System;
using Sources.ControllersInterfaces;
using Sources.Domain.Collectors;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.FiniteStateMachines;
using Sources.Infrastructure.FiniteStateMachines.States;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Controllers.Collectors
{
    public class CollectorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly ICollectorView _collectorView;
        private readonly Collector _collector;

        public CollectorPresenter(FiniteState firstState ,ICollectorView collectorView, Collector collector)
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _collectorView = collectorView ?? throw new ArgumentNullException(nameof(collectorView));
            _collector = collector ?? throw new ArgumentNullException(nameof(collector));
        }

        public void Start() => 
            Start(_firstState);

        public void Enable()
        {
        }

        public void Disable()
        {
        }

        public void SetTarget(ICrystalView target) => 
            _collector.SetTarget(target);

        public void SetCommandCenter(CommandCenter commandCenter) => 
            _collector.SetCommandCenter(commandCenter);

        public void SetFlag(FlagView flagView) => 
            _collector.SetFlag(flagView);
    }
}