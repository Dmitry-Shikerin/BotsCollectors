using System;
using System.Collections.Generic;
using Sources.Domain.CommandСenters;
using Sources.Domain.Constants;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.Presentations.Views;
using Sources.Presentations.Views.CommandCenters;
using Sources.PresentationsInterfaces.Views;
using UnityEngine;

namespace Sources.Controllers.CommandCenters
{
    public class CommandCenterPresenter : PresenterBase
    {
        private readonly ICommandCenterView _commandCenterView;
        private readonly CommandCenter _commandCenter;
        private readonly OverlapService _overlapService;
        private readonly ICollectorViewFactory _collectorViewFactory;
        private readonly ITextUI _textUI;
        private readonly RayCastService _rayCastService;
        private readonly CommandCenterBuilder _commandCenterBuilder;
        private readonly FlagView _flagViewPrefab;
        private readonly InputService _inputService;

        private Queue<CrystalView> _crystalViewsQueue;

        public CommandCenterPresenter
        (
            ICommandCenterView commandCenterView,
            CommandCenter commandCenter,
            OverlapService overlapService,
            ICollectorViewFactory collectorViewFactory,
            ITextUI textUI,
            InputService inputService,
            RayCastService rayCastService,
            FlagView flagViewPrefab,
            CommandCenterBuilder commandCenterBuilder
        )
        {
            _commandCenterView = commandCenterView ??
                                 throw new ArgumentNullException(nameof(commandCenterView));
            _commandCenter = commandCenter ??
                             throw new ArgumentNullException(nameof(commandCenter));
            _overlapService = overlapService ?? throw new ArgumentNullException(nameof(overlapService));
            _collectorViewFactory = collectorViewFactory ??
                                    throw new ArgumentNullException(nameof(collectorViewFactory));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _rayCastService = rayCastService ?? throw new ArgumentNullException(nameof(rayCastService));
            _commandCenterBuilder = commandCenterBuilder ?? 
                                    throw new ArgumentNullException(nameof(commandCenterBuilder));
            _flagViewPrefab = flagViewPrefab ? flagViewPrefab : 
                throw new ArgumentNullException(nameof(flagViewPrefab));
            _inputService = inputService ? inputService : 
                throw new ArgumentNullException(nameof(inputService));
        }

        public override void Enable()
        {
            _inputService.Scanning += OnScan;
            _inputService.SendCollector += OnSendCollector;
            _inputService.CreatureCollector += OnCreateCollector;
            _inputService.ChooseCommandCenter += OnChooseCommandCenter;
            _inputService.SettingFlagPosition += OnSettingFlagPosition;
        }

        public override void Disable()
        {
            _inputService.Scanning -= OnScan;
            _inputService.SendCollector -= OnSendCollector;
            _inputService.CreatureCollector -= OnCreateCollector;
            _inputService.ChooseCommandCenter -= OnChooseCommandCenter;
            _inputService.SettingFlagPosition -= OnSettingFlagPosition;
        }

        private void OnSettingFlagPosition()
        {
            if (_commandCenter.CanBuild)
            {
                CreateFlag();
            }
        }

        private void OnChooseCommandCenter()
        {
            _rayCastService.RayCast(out RaycastHit hit);

            if (hit.collider.gameObject.TryGetComponent(out CommandCenterView commandCenterView))
            {
                _commandCenter.SetCanBuild(true);
            }
        }

        public void Update()
        {
            if (_commandCenter.TryBuildCommandCenter())
            {
                _commandCenter.SendCollectorBuild();
            }
        }
        
        private void OnCreateCollector()
        {
            if(_commandCenter.CanBuildState)
                return;
            
            _commandCenter.RemoveExtractedResources(_commandCenter.PricePerCollector);
            
            ICollectorView collector = _collectorViewFactory.Create(
                _commandCenterView, _commandCenter.ParkingPoint);

            _commandCenter.AddCollector(collector);
        }

        public void SetParkingPoint(Vector3 parkingPoint) =>
            _commandCenter.SetParkingPoint(parkingPoint);

        private void OnScan()
        {
            IReadOnlyList<CrystalView> newCrystals
                = _overlapService.OverlapSphere<CrystalView>
                (_commandCenterView.OverlapStartPoint, _commandCenterView.Radius,
                    LayerMaskConstants.Touchable, LayerMaskConstants.Obstacle,
                    _commandCenter.Colliders);

            _crystalViewsQueue = new Queue<CrystalView>(newCrystals);

            _commandCenter.SetFoundedResources(newCrystals.Count);
        }

        private void CreateFlag()
        {
            _rayCastService.RayCast(out RaycastHit hit);

            if (hit.collider.gameObject.TryGetComponent(out TerrainView terrainView))
            {
                if (_commandCenter.CanBuildState)
                {
                    _commandCenter.FlagView.SetPosition(hit.point);

                    _commandCenter.SetCanBuild(false);

                    return;
                }

                FlagView flagView = _commandCenterView.InstantiateFlag(_flagViewPrefab, hit.point);
                _commandCenter.SetFlag(flagView);
                _commandCenter.SetCanBuild(false);
            }
        }

        private void OnSendCollector()
        {
            try
            {
                ICrystalView crystal = _crystalViewsQueue.Dequeue();

                _commandCenter.SendCollector(crystal);
            }
            catch (InvalidOperationException)
            {
                _textUI.SetText("Недостаточно коллекторов");
            }
            catch (NullReferenceException)
            {
                _textUI.SetText("Кристалл не найден");
            }
            catch (ArgumentOutOfRangeException)
            {
                _textUI.SetText("Очередь ккристаллов пуста");
            }
        }

        public CommandCenter BuildCommandCenter()
        {
            _commandCenter.RemoveExtractedResources(_commandCenter.PricePerCommandCenter);
            return _commandCenterBuilder.Create(_commandCenter.FlagView.transform.position);
        }

        public void DestroyFlag() => 
            GameObject.Destroy(_commandCenter.FlagView.gameObject);
    }
}