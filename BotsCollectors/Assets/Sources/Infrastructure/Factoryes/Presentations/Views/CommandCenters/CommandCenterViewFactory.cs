using System;
using Sources.Controllers.CommandCenters;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.Builders;
using Sources.Infrastructure.Factoryes.Controllers.CommandCenters;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factoryes;
using Sources.Presentations.Views;
using Sources.Presentations.Views.CommandCenters;
using Sources.PresentationsInterfaces.Views;

namespace Sources.Infrastructure.Factoryes.Presentations.Views.CommandCenters
{
    public class CommandCenterViewFactory
    {
        private readonly CommandCenterPresenterFactory _commandCenterPresenterFactory;

        public CommandCenterViewFactory(CommandCenterPresenterFactory commandCenterPresenterFactory)
        {
            _commandCenterPresenterFactory = commandCenterPresenterFactory ??
                                             throw new ArgumentNullException(nameof(commandCenterPresenterFactory));
        }

        public ICommandCenterView Create
        (
            CommandCenterView commandCenterView,
            CommandCenter commandCenter,
            ICollectorViewFactory collectorViewFactory,
            CommandCenterUIView commandCenterUIView,
            RayCastService rayCastService,
            FlagView flagViewPrefab,
            CommandCenterBuilder commandCenterBuilder
        )
        {
            if (commandCenterView == null) 
                throw new ArgumentNullException(nameof(commandCenterView));
            
            if (commandCenter == null) 
                throw new ArgumentNullException(nameof(commandCenter));

            CommandCenterPresenter commandCenterPresenter =
                _commandCenterPresenterFactory.Create(
                    commandCenterView, commandCenter, collectorViewFactory,
                    commandCenterUIView.MessageView, rayCastService,
                    flagViewPrefab, commandCenterBuilder);

            commandCenterView.Construct(commandCenterPresenter);

            return commandCenterView;
        }
    }
}