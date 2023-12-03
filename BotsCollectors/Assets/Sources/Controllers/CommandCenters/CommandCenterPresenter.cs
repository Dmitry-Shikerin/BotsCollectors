using System;
using System.Collections.Generic;
using Sources.Domain.CommandСenters;
using Sources.Domain.Constants;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.Presentations.Views;
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
        private readonly InputService _inputService;

        private Queue<CrystalView> _crystalViewsQueue;

        public CommandCenterPresenter
        (
            ICommandCenterView commandCenterView,
            CommandCenter commandCenter,
            OverlapService overlapService,
            ICollectorViewFactory collectorViewFactory,
            ITextUI textUI,
            InputService inputService
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
            _inputService = inputService ? inputService : 
                throw new ArgumentNullException(nameof(inputService));
        }

        public override void Enable()
        {
            _inputService.Scanning += OnScan;
            _inputService.SendCollector += OnSendCollector;
            _inputService.CreatureCollector += OnCreateCollector;
        }

        public override void Disable()
        {
            _inputService.Scanning -= OnScan;
            _inputService.SendCollector -= OnSendCollector;
            _inputService.CreatureCollector -= OnCreateCollector;
        }
        
        private void OnCreateCollector()
        {
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
            catch(ArgumentOutOfRangeException)
            {
                _textUI.SetText("Очередь ккристаллов пуста");
            }
        }
    }
}