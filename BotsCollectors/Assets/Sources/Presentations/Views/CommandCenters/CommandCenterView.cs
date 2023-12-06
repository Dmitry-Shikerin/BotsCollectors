using Sources.Controllers.CommandCenters;
using Sources.Domain.CommandСenters;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Presentations.Views.CommandCenters
{
    public class CommandCenterView : PresentableView<CommandCenterPresenter>, ICommandCenterView
    {
        [field: SerializeField, Range(20f, 100f)]
        public float Radius { get; private set; } = 70f;

        private StopPointView _stopPointView;

        public Vector3 OverlapStartPoint => gameObject.transform.position;
    
        private void Awake() => 
            _stopPointView = GetComponentInChildren<StopPointView>();

        private void Start() => 
            Presenter?.SetParkingPoint(_stopPointView.transform.position);

        public CommandCenter BuildCommandCenter() => 
            Presenter?.BuildCommandCenter();

        public void Update() => 
            Presenter?.Update();

        public FlagView InstantiateFlag(FlagView gameObject, Vector3 position) => 
            Instantiate(gameObject, position, Quaternion.identity);

        public void DestroyFlag() => 
            Presenter?.DestroyFlag();
    }
}