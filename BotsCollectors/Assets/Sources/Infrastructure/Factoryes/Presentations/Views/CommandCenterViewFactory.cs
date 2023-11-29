using System;
using JetBrains.Annotations;
using Sources.Controllers.CommandCenters;
using Sources.Domain.CommandSenters;
using Sources.Infrastructure.Factoryes.Controllers;
using Sources.PresentationsInterfaces.Vievs;

namespace Sources.Infrastructure.Factoryes.Presentations.Views
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
            CommandCenter commandCenter
        )
        {
            if (commandCenterView == null) 
                throw new ArgumentNullException(nameof(commandCenterView));
            
            if (commandCenter == null) 
                throw new ArgumentNullException(nameof(commandCenter));

            CommandCenterPresenter commandCenterPresenter =
                _commandCenterPresenterFactory.Create(commandCenterView, commandCenter);

            commandCenterView.Construct(commandCenterPresenter);

            return commandCenterView;
        }
    }
}