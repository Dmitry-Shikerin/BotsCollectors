using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CollectorViewOld : MonoBehaviour
{
    [SerializeField] private Transform _parkingPoint;
    [SerializeField] private CommandCenterView _commandCenterView;

    private CrystalView _targetCrystal;
    
    private NavMeshAgent _navMeshAgent;

    private float distanse = 4;
    private Rigidbody _rigidbody;

    private CrystalTrunkPointView _crystalTrunkPoint;

    public bool IsIdle => _targetCrystal == null;
    private bool _move = true;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _crystalTrunkPoint = GetComponentInChildren<CrystalTrunkPointView>();
    }

    void Update()
    {
        if (IsIdle == false)
            MoveToCrystal();

        if (IsIdle == false && Vector3.Distance
                (_navMeshAgent.transform.position, _targetCrystal.transform.position) < 4)
        {
            TakeCrystal();
        }

        if (gameObject.GetComponentInChildren<CrystalView>() != null)
        {
            MoveToCommandCenter();
            // _isIdle = true;
        }

        if (Vector3.Distance(_navMeshAgent.transform.position, _parkingPoint.transform.position) <= 4 &&
            IsIdle == false && gameObject.GetComponentInChildren<CrystalView>() != null)
        {
            GiveAwayCrystal();
            //TODO покашто хардкодом
            //TODO это тоже должна решать база
            // _commandCenterView.AddExtractedResources(1);
            Debug.Log("Отдал кристалл");
        }
    }

    public void MoveToCrystal()
    {
        //добавил
        _navMeshAgent.destination = _targetCrystal.transform.position;
    }

    //TODO переименовать
    public void TakeCrystal()
    {
        //добавил
        //TODO из вьюшки кристалла
        _targetCrystal.SetUnavailable();
        
        //TODO это тоже вьюшка кристалла но друггой метод
        _targetCrystal.SetParent(_crystalTrunkPoint.transform);
    }

    //TODO это презентер
    private void MoveToCommandCenter()
    {
        //TODO два метода вьюшка и модель
        _navMeshAgent.destination = _parkingPoint.transform.position;
    }

    public void GiveAwayCrystal()
    {
        //TODO правильно ли что я здесь этот метод вызываю
        _commandCenterView.TakeCrystal(_targetCrystal);
        
        //TODO это модель
        SetTarget(null);
    }

    public void SetTarget(CrystalView targetCrystal)
    {
        if (IsIdle == false)
        {
            Debug.Log("Коллектор занят");
            return;
        }

        _targetCrystal = targetCrystal;
        
        if(targetCrystal == null)
            return;
        
        _targetCrystal.Hide();
    }

    // private FiniteStateMachine Create()
    // {
    //     //TODO как делать DI этой стейт машины
    //     CollectorIdleState idleState = new CollectorIdleState();
    //     CollectorMoveTowardsCrystalState moveTowardsCrystalState = new CollectorMoveTowardsCrystalState();
    //     CollectorTakeState takeState = new CollectorTakeState();
    //     CollectorMoveTowardsCommandCenterState moveTowardsCommandCenterState =
    //         new CollectorMoveTowardsCommandCenterState();
    //
    //     FromIdleToMoveTowardsCrystalTransition fromIdleToMoveTowardsCrystalTransition
    //         = new FromIdleToMoveTowardsCrystalTransition(moveTowardsCrystalState);
    //     idleState.AddTransition(fromIdleToMoveTowardsCrystalTransition);
    //
    //     FromMoveTowardsToTakeTransition fromMoveTowardsToTakeTransition
    //         = new FromMoveTowardsToTakeTransition(takeState);
    //     moveTowardsCrystalState.AddTransition(fromMoveTowardsToTakeTransition);
    //
    //     FromTakeStateToMoveTowardsCommandCenterTransition fromTakeStateToMoveTowardsCommandCenterTransition
    //         = new FromTakeStateToMoveTowardsCommandCenterTransition(moveTowardsCommandCenterState);
    //     takeState.AddTransition(fromTakeStateToMoveTowardsCommandCenterTransition);
    //
    //     FromMoveTowardsCommandCenterToIdleTransition fromMoveTowardsCommandCenterToIdleTransition
    //         = new FromMoveTowardsCommandCenterToIdleTransition(idleState);
    //     moveTowardsCommandCenterState.AddTransition(fromMoveTowardsCommandCenterToIdleTransition);
    //
    //     FiniteStateMachine collectorStateMachine = new FiniteStateMachine();
    //     return collectorStateMachine;
    // }
}