using Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewAViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Title = "View A";
            UpdateCommand = new DelegateCommand(Update);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Update()
        {
            Title = "View A : " + DateTime.Now.ToString();
        }

        private void Navigate(string navigationPath)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, navigationPath);
        }
    }
}
