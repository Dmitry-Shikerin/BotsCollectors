using Sources.Controllers.Collectors;
using Sources.Controllers.Collectors.States;
using Sources.Domain;
using Sources.Infrastructure.StateMachines.Transitions;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;

namespace Sources.Infrastructure.Factoryes.Controllers
{
    public class CollectorPresenterFactory
    {
        public CollectorPresenter Create(ICollectorView collectorView, Collector collector)
        {
            CollectorIdleState idleState = new CollectorIdleState(collectorView, collector);
            CollectorMoveTowardsCrystalState moveTowardsCrystalState =
                new CollectorMoveTowardsCrystalState(collectorView, collector);
            CollectorTakeState takeState = new CollectorTakeState(collectorView, collector);
            CollectorMoveTowardsCommandCenterState moveTowardsCommandCenterState =
                new CollectorMoveTowardsCommandCenterState(collectorView, collector);
            CollectorGiveAwayCrystalState giveAwayCrystalState =
                new CollectorGiveAwayCrystalState(collectorView, collector);

            //TODO можно ли фанк через люмбду или создать метод?
            FiniteTransitionBase toMoveTowardsCrystalTransition =
                new FiniteTransitionBase(moveTowardsCrystalState,
                    () => collector.IsIdle == false);

            idleState.AddTransition(toMoveTowardsCrystalTransition);

            FiniteTransitionBase toTakeTransition =
                new FiniteTransitionBase(takeState, () =>
                    Vector3.Distance(collectorView.Position,
                        collector.TargetPosition) <= collectorView.NavMeshAgent.stoppingDistance);
            moveTowardsCrystalState.AddTransition(toTakeTransition);

            FiniteTransitionBase toMoveTowardsCommandCenterTransition =
                new FiniteTransitionBase(moveTowardsCommandCenterState,
                    () => collectorView.GetCrystal() != null);
            takeState.AddTransition(toMoveTowardsCommandCenterTransition);

            FiniteTransitionBase toGiveAwayCrystalTransition =
                new FiniteTransitionBase(giveAwayCrystalState,
                    () => Vector3.Distance(collectorView.Position,
                        collector.ParkingPoint) <= collectorView.NavMeshAgent.stoppingDistance);
            moveTowardsCommandCenterState.AddTransition(toGiveAwayCrystalTransition);

            FiniteTransitionBase toIdleTransition = new FiniteTransitionBase(
                idleState, () => collector.IsIdle);
            giveAwayCrystalState.AddTransition(toIdleTransition);

            CollectorPresenter collectorStateMachine = new CollectorPresenter(
                idleState, collectorView, collector);

            return collectorStateMachine;
        }
    }
}