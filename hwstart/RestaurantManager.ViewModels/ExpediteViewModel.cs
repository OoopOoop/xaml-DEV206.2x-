using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using RestaurantManager.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace RestaurantManager.ViewModels
{
    public class ExpediteViewModel : ViewModel
    {
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public ICommand DeleteAllOrdersCommand { get; private set; }
        public List<Order> OrderItems { get; set; }

        //public List<Order> OrderItems
        //{
        //    get { return base.Repository.Orders; }
        //}

        public ExpediteViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            DeleteAllOrdersCommand = new RelayCommand(DeleteAllOrders);
        }

        protected override void OnDataLoaded()
        {
            OrderItems = base.Repository.Orders;
            OnPropertyChanged(nameof(OrderItems));
        }

        private void DeleteAllOrders()
        {
            Repository.Orders.Clear();
            NavigateTo("ExpeditePage");
        }

        private void DeleteOrder(Order order)
        {
            Repository.Orders.Remove(order);
            //Refresh page to see orders "removed"
            NavigateTo("ExpeditePage");
        }
    }
}