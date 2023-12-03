using Sources.Domain.CameraMovements;
using Sources.Domain.CameraMovements.CameraMovementCharacteristics;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.Infrastructure.Factoryes.Controllers.CommandCenters;
using Sources.Infrastructure.Factoryes.Presentations.UI;
using Sources.Infrastructure.Factoryes.Presentations.Views;
using Sources.Infrastructure.Factoryes.Presentations.Views.CommandCenters;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.Presentations.Views;
using Sources.Presentations.Views.CommandCenters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.App.Bootstrapper
{
    [DefaultExecutionOrder(-1)]
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CommandCenterView _commandCenterView;
        [SerializeField] private CommandCenterUIView _commandCenterUIView; 
    
        private const string InputServicePath = "Prefabs/InputService";
        private const string CameraMovementCharacteristicPath = "Configs/CameraMovementCharacteristic";

        private void Awake()
        {
            LinecastService linecastService = new LinecastService();
        
            OverlapService overlapService = new OverlapService(linecastService);

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
        
            CommandCenter commandCenter = new CommandCenter();
            CommandCenterPresenterFactory commandCenterPresenterFactory = 
                new CommandCenterPresenterFactory(overlapService, inputService);
            CommandCenterViewFactory commandCenterViewFactory = 
                new CommandCenterViewFactory(commandCenterPresenterFactory);
            ICollectorViewFactory collectorViewFactory = new CollectorViewFactory();
            commandCenterViewFactory.Create(_commandCenterView, commandCenter, 
                collectorViewFactory, _commandCenterUIView);
            
            
            TextUIFactory textUIFactory = new TextUIFactory();
            CommandCenterUIViewFactory commandCenterUIViewFactory =
                new CommandCenterUIViewFactory(textUIFactory);
            commandCenterUIViewFactory.Create(_commandCenterUIView, commandCenter);
        }
    }
}