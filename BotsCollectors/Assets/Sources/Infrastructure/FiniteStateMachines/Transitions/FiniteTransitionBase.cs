using System;
using JetBrains.Annotations;
using Sources.Infrastructure.FiniteStateMachines.States;

namespace Sources.Infrastructure.FiniteStateMachines.Transitions
{
    public class FiniteTransitionBase : FiniteTransition
    {
        private readonly Func<bool> _condition;

        public FiniteTransitionBase
        (
            FiniteState nextState,
            [NotNull] Func<bool> condition
        ) : base(nextState)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        protected override bool CanTransit()
        {
            return _condition.Invoke();
        }
    }
}