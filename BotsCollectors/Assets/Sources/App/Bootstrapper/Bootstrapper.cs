using Sources.Domain.CameraMovements;
using Sources.Domain.CameraMovements.CameraMovementCharacteristics;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.Infrastructure.Factoryes.Controllers.CommandCenters;
using Sources.Infrastructure.Factoryes.Presentations.UI;
using Sources.Infrastructure.Factoryes.Presentations.Views;
using Sources.Infrastructure.Factoryes.Presentations.Views.CommandCenters;
using Sources.Infrastructure.Services;
using Sources.Presentations.Views;
using Sources.Presentations.Views.CommandCenters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.App.Bootstrapper
{
    [DefaultExecutionOrder(-1)]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CommandCenterUIView _commandCenterUIView;
        [SerializeField] private Camera _cameraMain;
        [SerializeField] private Transform _commandCenterSpawnPoint;
    
        private const string InputServicePath = "Prefabs/InputService";
        private const string CameraMovementCharacteristicPath = "Configs/CameraMovementCharacteristic";
        private const string CommandCenterPrefabPath = "Prefabs/Base";
        private const string FlagViewPrefabPath = "Prefabs/Flag";

        private void Awake()
        {
            LinecastService linecastService = new LinecastService();
        
            OverlapService overlapService = new OverlapService(linecastService);

            RayCastService rayCastService = new RayCastService(_cameraMain);

            InputService inputServicePrefab = Resources.Load<InputService>(InputServicePath);
            InputService inputService = Object.Instantiate<InputService>(inputServicePrefab);

            CameraMovementCharacteristic cameraMovementCharacteristic =
                Resources.Load<CameraMovementCharacteristic>(CameraMovementCharacteristicPath);
            CameraMovement cameraMovement = new CameraMovement(cameraMovementCharacteristic);
            CameraMovementPresenterFactory cameraMovementPresenterFactory =
                new CameraMovementPresenterFactory(inputService);
            CameraMovementViewFactory cameraMovementViewFactory = 
                new CameraMovementViewFactory(cameraMovementPresenterFactory);
            CameraMovementView cameraMovementView = FindObjectOfType<CameraMovementView>();
            cameraMovementViewFactory.Create(cameraMovementView, cameraMovement);

            CommandCenterView commandCenterView = Resources.Load<CommandCenterView>(
                CommandCenterPrefabPath);
            FlagView flagView = Resources.Load<FlagView>(FlagViewPrefabPath);
            CommandCenterBuilder commandCenterBuilder = 
                new CommandCenterBuilder(overlapService, inputService, 
                    rayCastService, flagView, commandCenterView, _commandCenterUIView);
            commandCenterBuilder.Create(_commandCenterSpawnPoint.transform.position);
        }
    }
}