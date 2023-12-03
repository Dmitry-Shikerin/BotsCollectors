using Sources.Controllers.CommandCenters;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Presentations.Views.CommandCenters
{
    public class CommandCenterView : PresentableView<CommandCenterPresenter>, ICommandCenterView
    {
        [field : SerializeField, Range(20f, 100f)] public float Radius { get; private set; }

        private StopPointView _stopPointView;

        public Vector3 OverlapStartPoint => gameObject.transform.position;
    
        private void Awake()
        {
            _stopPointView = GetComponentInChildren<StopPointView>();
        }

        private void Start()
        {
            Presenter?.SetParkingPoint(_stopPointView.transform.position);
        }
    }
}