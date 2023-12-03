using System;
using Sources.Controllers.CommandCenters;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Infrastructure.Factoryes.Controllers.CommandCenters
{
    public class CommandCenterPresenterFactory
    {
        private readonly OverlapService _overlapService;
        private readonly InputService _inputService;

        public CommandCenterPresenterFactory(OverlapService overlapService, InputService inputService)
        {
            _overlapService = overlapService ??
                              throw new ArgumentNullException(nameof(overlapService));
            _inputService = inputService ? 
                inputService : throw new ArgumentNullException(nameof(inputService));
        }

        public CommandCenterPresenter Create
        (
            ICommandCenterView commandCenterView,
            CommandCenter commandCenter,
            ICollectorViewFactory collectorViewFactory,
            ITextUI textUI
        )
        {
            return new CommandCenterPresenter
            (
                commandCenterView,
                commandCenter,
                _overlapService,
                collectorViewFactory,
                textUI,
                _inputService
            );
        }
    }
}