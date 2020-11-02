using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Unity;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        //IUnityContainer _container;

        //public ModuleAModule(IUnityContainer container)
        //{
        //    _container = container;
        //}

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
        }
    }
}
