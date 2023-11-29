﻿using System;
using JetBrains.Annotations;
using Sources.Domain;
using Sources.Infrastructure.StateMachines;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Controllers.Collectors
{
    public class CollectorPresenter : FiniteStateMachine
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

        public void Start()
        {
            Start(_firstState);
        }
        
        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }

        public void SetTarget(ICrystalView target)
        {
            _collector.SetTarget(target);
        }
    }
}