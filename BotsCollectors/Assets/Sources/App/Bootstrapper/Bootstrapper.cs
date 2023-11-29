using System;
using System.Collections;
using System.Collections.Generic;
using Sources.App.Core;
using Sources.Domain;
using Sources.Domain.CameraMovements;
using Sources.Domain.CameraMovements.CameraMovementCharacteristics;
using Sources.Domain.CollectorCharacteristics;
using Sources.Domain.CommandSenters;
using Sources.Infrastructure.Factoryes.AppCores;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.Infrastructure.Factoryes.Presentations.Views;
using Sources.Infrastructure.Services;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;
using Object = UnityEngine.Object;

[DefaultExecutionOrder(-1)]
public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private CommandCenterView _commandCenterView;
    
    private AppCore _appCore;

    private void Awake()
    {
        _appCore = FindObjectOfType<AppCore>() ?? new AppCoreFactory().Create();
        
        //TODO покашто здесь
        LinecastService linecastService = new LinecastService();
        
        OverlapService overlapService = new OverlapService(linecastService);

        InputService inputServicePrefab = Resources.Load<InputService>("Prefabs/InputService");
        InputService inputService = Object.Instantiate<InputService>(inputServicePrefab);

        CameraMovementCharacteristic cameraMovementCharacteristic =
            Resources.Load<CameraMovementCharacteristic>("Configs/CameraMovementCharacteristic");
        CameraMovement cameraMovement = new CameraMovement(cameraMovementCharacteristic);
        CameraMovementPresenterFactory cameraMovementPresenterFactory =
            new CameraMovementPresenterFactory(inputService);
        CameraMovementViewFactory cameraMovementViewFactory = 
            new CameraMovementViewFactory(cameraMovementPresenterFactory);
        CameraMovementView cameraMovementView = FindObjectOfType<CameraMovementView>();
        cameraMovementViewFactory.Create(cameraMovementView, cameraMovement);
        
        CommandCenter commandCenter = new CommandCenter();
        //TODO заменить
        // CommandCenterView commandCenterView = FindObjectOfType<CommandCenterView>();
        CommandCenterPresenterFactory commandCenterPresenterFactory = 
            new CommandCenterPresenterFactory(overlapService);
        CommandCenterViewFactory commandCenterViewFactory = 
            new CommandCenterViewFactory(commandCenterPresenterFactory);
        commandCenterViewFactory.Create(_commandCenterView, commandCenter);

        StopPointView stopPointView = FindObjectOfType<StopPointView>();
        CollectorView collectorViewPrefab = Resources.Load<CollectorView>("Prefabs/Collector");
        CollectorView collectorView = Object.Instantiate(
            collectorViewPrefab, stopPointView.transform.position, Quaternion.identity);
        // CollectorCharacteristic collectorCharacteristic =
        //     Resources.Load<CollectorCharacteristic>();
        Collector collector = new Collector();
        //TODO покашто так но потом заменю
        collector.SetCommandCenter(_commandCenterView);
        _commandCenterView.AddCollector(collectorView);
        
        CollectorPresenterFactory collectorPresenterFactory = new CollectorPresenterFactory();
        CollectorViewFactory collectorViewFactory = new CollectorViewFactory(
            collectorPresenterFactory);
        collectorViewFactory.Create(collectorView, collector);
        
        

        //TODO в презентер базы закидываю фабрику коллекторов
    }
}