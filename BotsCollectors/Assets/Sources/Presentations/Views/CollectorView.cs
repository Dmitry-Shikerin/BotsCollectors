using System;
using Sources.Controllers.Collectors;
using Sources.Infrastructure.StateMachines;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentations.Views
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CollectorView : MonoBehaviour, ICollectorView
    {
        private CollectorPresenter _presenter;

        private CrystalTrunkPointView _crystalTrunkPointView;

        public Transform CrystalTrunkPoint => _crystalTrunkPointView.transform;
        public NavMeshAgent NavMeshAgent { get; private set; }

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _crystalTrunkPointView = GetComponentInChildren<CrystalTrunkPointView>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _presenter?.Start();
        }

        private void Update()
        {
            _presenter?.Update();
        }

        public void Construct(CollectorPresenter presenter)
        {
            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void SetDestination(Vector3 destination)
        {
            NavMeshAgent.destination = destination;
        }

        public CrystalView GetCrystal()
        {
            return GetComponentInChildren<CrystalView>();
        }

        public void SetTarget(ICrystalView target)
        {
            _presenter.SetTarget(target);
        }
    }
}