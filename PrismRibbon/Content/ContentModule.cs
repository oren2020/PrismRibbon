using Content.Views;
using Infrastructure;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Content
{
    public class ContentModule : IModule
    {
        readonly IRegionManager _regionManager;

        public ContentModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            var view = containerProvider.Resolve<ContentView>();
            region.Add(view);
            region.Activate(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ContentView>();
        }
    }
}
