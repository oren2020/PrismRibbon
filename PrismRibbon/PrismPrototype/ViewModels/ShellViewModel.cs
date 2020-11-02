using Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismRibbon.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public DelegateCommand<string> NavigateCommand { get; set; }

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        void Navigate(string navigationPath)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
