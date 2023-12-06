using Sources.Controllers.Collectors;
using Sources.Domain.CommandСenters;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentations.Views
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CollectorView : PresentableView<CollectorPresenter>, ICollectorView
    {
        private CrystalTrunkPointView _crystalTrunkPointView;

        public Transform CrystalTrunkPoint => _crystalTrunkPointView.transform;
        public NavMeshAgent NavMeshAgent { get; private set; }

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _crystalTrunkPointView = GetComponentInChildren<CrystalTrunkPointView>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start() => 
            Presenter?.Start();

        private void Update() => 
            Presenter?.Update();

        public void SetDestination(Vector3 destination) => 
            NavMeshAgent.destination = destination;

        public CrystalView GetCrystal() => 
            GetComponentInChildren<CrystalView>();

        public void SetCrystal(ICrystalView target) => 
            Presenter.SetTarget(target);

        public void SetCommandCenter(CommandCenter commandCenter) => 
            Presenter.SetCommandCenter(commandCenter);

        public void SetFlag(FlagView flagView) => 
            Presenter.SetFlag(flagView);

        public void SetPosition(Vector3 position) => 
            transform.position = position;
    }
}