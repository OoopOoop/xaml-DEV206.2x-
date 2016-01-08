using GalaSoft.MvvmLight.Views;
using RestaurantManager.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RestaurantManager.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private DelegateCommand<string> _navigationCommand;
        protected RestaurantContext Repository { get; private set; }

        public INavigationService _navigationService;
        public DelegateCommand<string> NavigationCommand => _navigationCommand ?? (_navigationCommand = new DelegateCommand<string>(NavigateTo));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NavigateTo(string Pagekey)
        {
            _navigationService.NavigateTo(Pagekey);
        }

        public void OnPropertyChanged([CallerMemberName]string propName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public ViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            this.Repository = await RestaurantContextFactory.GetRestaurantContextAsync();
            OnDataLoaded();
        }

        protected abstract void OnDataLoaded();
    }
}