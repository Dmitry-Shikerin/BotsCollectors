using Sources.Infrastructure.FiniteStateMachines.States;

namespace Sources.Infrastructure.FiniteStateMachines.Transitions
{
    public interface IFiniteTransition
    {
        bool CanMoveNextState(out FiniteState state);
    }
}