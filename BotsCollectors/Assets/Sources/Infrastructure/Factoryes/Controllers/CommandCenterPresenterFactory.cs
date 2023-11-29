using System;
using JetBrains.Annotations;
using Sources.Controllers.CommandCenters;
using Sources.Domain.CommandSenters;
using Sources.Infrastructure.Services;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Infrastructure.Factoryes.Controllers
{
    public class CommandCenterPresenterFactory
    {
        private readonly OverlapService _overlapService;

        public CommandCenterPresenterFactory(OverlapService overlapService)
        {
            _overlapService = overlapService ??
                              throw new ArgumentNullException(nameof(overlapService));
        }

        public CommandCenterPresenter Create
        (
            ICommandCenterView commandCenterView,
            CommandCenter commandCenter
        )
        {
            return new CommandCenterPresenter
            (
                commandCenterView,
                commandCenter,
                _overlapService
            );
        }
    }
}