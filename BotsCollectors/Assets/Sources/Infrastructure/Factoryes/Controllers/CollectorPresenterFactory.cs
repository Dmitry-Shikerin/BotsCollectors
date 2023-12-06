using Sources.Controllers.Collectors;
using Sources.Controllers.Collectors.States;
using Sources.Domain.Collectors;
using Sources.Infrastructure.FiniteStateMachines.Transitions;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Infrastructure.Factoryes.Controllers
{
    public class CollectorPresenterFactory
    {
        public CollectorPresenter Create(ICollectorView collectorView, 
            Collector collector)
        {
            CollectorIdleState idleState = new CollectorIdleState(collectorView, collector);
            CollectorMoveTowardsCrystalState moveTowardsCrystalState =
                new CollectorMoveTowardsCrystalState(collectorView, collector);
            CollectorTakeState takeState = new CollectorTakeState(collectorView, collector);
            CollectorMoveTowardsCommandCenterState moveTowardsCommandCenterState =
                new CollectorMoveTowardsCommandCenterState(collectorView, collector);
            CollectorGiveAwayCrystalState giveAwayCrystalState =
                new CollectorGiveAwayCrystalState(collectorView, collector);

            CollectorMoveTowardsFlagState moveTowardsFlagState =
                new CollectorMoveTowardsFlagState(collectorView, collector);
            CollectorBuildCommandCenterState collectorBuildCommandCenterState =
                new CollectorBuildCommandCenterState(collectorView, collector);

            FiniteTransitionBase toMoveTowardsCrystalTransition =
                new FiniteTransitionBase(moveTowardsCrystalState,
                    () => collector.TargetCrystalView != null);
            idleState.AddTransition(toMoveTowardsCrystalTransition);

            FiniteTransitionBase toMoveTowardsFlagTransition =
                new FiniteTransitionBase(moveTowardsFlagState,
                    () => collector.FlagView != null);
            idleState.AddTransition(toMoveTowardsFlagTransition);

            FiniteTransitionBase toBuildCommandCenterTransition =
                new FiniteTransitionBase(collectorBuildCommandCenterState,
                    () => Vector3.Distance(collectorView.Position,
                              collector.CommandCenter.FlagView.transform.position) <=
                          collectorView.NavMeshAgent.stoppingDistance);
            moveTowardsFlagState.AddTransition(toBuildCommandCenterTransition);

            FiniteTransitionBase fromBuildCommandCenterToIdleTransition =
                new FiniteTransitionBase(idleState,
                    () => collector.FlagView == null);
            collectorBuildCommandCenterState.AddTransition(
                fromBuildCommandCenterToIdleTransition);

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
                idleState, () => collector.TargetCrystalView == null);
            giveAwayCrystalState.AddTransition(toIdleTransition);

            CollectorPresenter collectorStateMachine = new CollectorPresenter(
                idleState, collectorView, collector);

            return collectorStateMachine;
        }
    }
}