using System;
using Sources.Domain.CommandСenters;
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

namespace Sources.Infrastructure.Builders
{
    public class CommandCenterBuilder
    {
        private readonly OverlapService _overlapService;
        private readonly InputService _inputService;
        private readonly RayCastService _rayCastService;
        private readonly CommandCenterUIView _commandCenterUIView;
        private readonly CommandCenterView _commandCenterViewPrefab;
        private readonly FlagView _flagViewPrefab;

        public CommandCenterBuilder
        (
            OverlapService overlapService,
            InputService inputService,
            RayCastService rayCastService,
            FlagView flagViewPrefab,
            CommandCenterView commandCenterViewPrefab,
            CommandCenterUIView commandCenterUIView
        )
        {
            _overlapService = overlapService ??
                              throw new ArgumentNullException(nameof(overlapService));
            _inputService = inputService ? inputService : 
                throw new ArgumentNullException(nameof(inputService));
            _rayCastService = rayCastService ??
                              throw new ArgumentNullException(nameof(rayCastService));
            _commandCenterUIView = commandCenterUIView ? commandCenterUIView : 
                throw new ArgumentNullException(nameof(commandCenterUIView));
            _commandCenterViewPrefab = commandCenterViewPrefab ? commandCenterViewPrefab :
                throw new ArgumentNullException(nameof(commandCenterViewPrefab));
            _flagViewPrefab = flagViewPrefab ? flagViewPrefab : 
                throw new ArgumentNullException(nameof(flagViewPrefab));
        }

        public CommandCenter Create(Vector3 spawnPoint)
        {
            CommandCenterView commandCenterView = Object.Instantiate(_commandCenterViewPrefab, 
                spawnPoint, Quaternion.LookRotation(Vector3.back));
            
            CommandCenter commandCenter = new CommandCenter();
            CommandCenterPresenterFactory commandCenterPresenterFactory =
                new CommandCenterPresenterFactory(_overlapService, _inputService);
            CommandCenterViewFactory commandCenterViewFactory =
                new CommandCenterViewFactory(commandCenterPresenterFactory);
            ICollectorViewFactory collectorViewFactory = new CollectorViewFactory();
            commandCenterViewFactory.Create(commandCenterView, commandCenter,
                collectorViewFactory, _commandCenterUIView, _rayCastService,
                _flagViewPrefab, this);

            TextUIFactory textUIFactory = new TextUIFactory();
            CommandCenterUIViewFactory commandCenterUIViewFactory =
                new CommandCenterUIViewFactory(textUIFactory);
            commandCenterUIViewFactory.Create(_commandCenterUIView, commandCenter);

            return commandCenter;
        }
    }
}