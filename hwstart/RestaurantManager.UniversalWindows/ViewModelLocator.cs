using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using RestaurantManager.ViewModels;

namespace RestaurantManager.UniversalWindows
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<OrderViewModel>();
            SimpleIoc.Default.Register<ExpediteViewModel>();
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("ExpeditePage", typeof(ExpeditePage));
            navigationService.Configure("OrderPage", typeof(OrderPage));
            navigationService.Configure("MainPage", typeof(MainPage));
            return navigationService;
        }

        public OrderViewModel OrderViewModel => SimpleIoc.Default.GetInstance<OrderViewModel>();

        public ExpediteViewModel ExpediteViewModel => SimpleIoc.Default.GetInstance<ExpediteViewModel>();
    }
}