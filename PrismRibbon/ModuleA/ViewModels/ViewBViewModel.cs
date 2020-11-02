using Prism.Commands;
using Prism.Mvvm;
using System;

namespace ModuleA.ViewModels
{
    public class ViewBViewModel : BindableBase
    {
        public DelegateCommand UpdateCommand { get; set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewBViewModel()
        {
            Title = "View B";
            UpdateCommand = new DelegateCommand(Update);
        }

        private void Update()
        {
            Title = "View B : " + DateTime.Now.ToString();
        }
    }
}
