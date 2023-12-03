using System;
using Sources.Domain.CommandСenters;
using Sources.Infrastructure.Factoryes.Presentations.UI;
using Sources.Presentations.Views.CommandCenters;

namespace Sources.Infrastructure.Factoryes.Presentations.Views.CommandCenters
{
    public class CommandCenterUIViewFactory
    {
        private readonly TextUIFactory _textUIFactory;

        public CommandCenterUIViewFactory(TextUIFactory textUIFactory)
        {
            _textUIFactory = textUIFactory ?? throw new ArgumentNullException(nameof(textUIFactory));
        }
        
        public CommandCenterUIView Create(CommandCenterUIView commandCenterUIView, CommandCenter commandCenter)
        {
            _textUIFactory.Create(commandCenterUIView.ExtractedResourcesView, 
                commandCenter.ExtractedResources);
            _textUIFactory.Create(commandCenterUIView.FoundedResourcesView, 
                commandCenter.FoundedResources);
            _textUIFactory.Create(commandCenterUIView.CollectorsCountView, 
                commandCenter.CollectorsCount);

            return commandCenterUIView;
        }
    }
}