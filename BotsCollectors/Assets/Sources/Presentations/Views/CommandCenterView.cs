using System;
using System.Collections.Generic;
 using Sources.Controllers.CommandCenters;
using Sources.Presentations.Views;
using Sources.PresentationsInterfaces.Vievs;
using UnityEngine;
using UnityEngine.UI;

public class CommandCenterView : MonoBehaviour, ICommandCenterView
{
    //TODO это не отсюда?
    [SerializeField] private Button _scanButton;
    [SerializeField] private Transform _overlapStartPoint;
    [SerializeField] private List<CollectorView> _collectors = new List<CollectorView>();
    [field: SerializeField] public Transform ParkingPoint { get; private set; }
    [field : SerializeField, Range(20f, 100f)] public float Radius { get; private set; }

    public Vector3 OverlapStartPoint => _overlapStartPoint.position;
    public IReadOnlyList<CollectorView> Collectors => _collectors;

    private CommandCenterPresenter _commandCenterPresenter;

    private void Awake()
    {
        
    }

    private void Update()
    {
        Debug.Log(_collectors.Count);
        _commandCenterPresenter.Update();
    }

    public void Construct(CommandCenterPresenter commandCenterPresenter)
    {
        if (commandCenterPresenter == null) 
            throw new ArgumentNullException(nameof(commandCenterPresenter));

        _commandCenterPresenter = commandCenterPresenter;
    }

    public void TakeCrystal(ICrystalView crystalView)
    {
        crystalView.RemoveParent();
        //TODO в модели как то нужно добавить количество
    }

    public void AddCollector(CollectorView collectorView)
    {
        _collectors.Add(collectorView);
    }
}