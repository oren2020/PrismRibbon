using Prism.Mvvm;

namespace Content.ViewModels
{
    public class ContentViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ContentViewModel()
        {
            Message = "Hello";
        }
    }
}
