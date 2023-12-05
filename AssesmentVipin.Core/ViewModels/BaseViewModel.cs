using CommunityToolkit.Mvvm.ComponentModel;

namespace AssessmentVipin.Core.ViewModels
{

    public abstract class BaseViewModel : ObservableObject
    {
        private string pageTitle = string.Empty;
        private bool isBusy;

        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }
        public BaseViewModel() { }

        public virtual Task PageLoaded()
        {
            return Task.CompletedTask;
        }

        public virtual Task PageUnLoaded()
        {
            return Task.CompletedTask;
        }
    }
}
