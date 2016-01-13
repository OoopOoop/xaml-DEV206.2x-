using RestaurantManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RestaurantManager.ViewModels
{
    public class ExpediteViewModel : ViewModel
    {
        private ObservableCollection<Order> _orderItems;
        public DelegateCommand<Order> DeleteOrderCommand { get; private set; }
        public DelegateCommand<object> DeleteAllOrdersCommand { get; private set; }
        public List<Order> SavedOrders { get; set; }

        public ObservableCollection<Order> OrderItems
        {
            get { return _orderItems; }
            set
            {
                if (value != _orderItems)
                {
                    _orderItems = value;
                    OnPropertyChanged(nameof(OrderItems));
                }
            }
        }

        //public List<Order> OrderItems
        //{
        //    get { return base.Repository.Orders; }
        //}

        public ExpediteViewModel()
        {
            DeleteOrderCommand = new DelegateCommand<Order>(DeleteOrder);
            DeleteAllOrdersCommand = new DelegateCommand<object>(DeleteAllOrders);
        }

        protected override void OnDataLoaded()
        {
            SavedOrders = base.Repository.Orders;
            OrderItems = new ObservableCollection<Order>();
            loadOrders();
        }

        private void loadOrders()
        {
            foreach (Order item in SavedOrders)
            {
                OrderItems.Add(item);
            }
        }

        private void DeleteAllOrders(object ob)
        {
            Repository.Orders.Clear();
            OrderItems.Clear();
            DeleteAllOrdersCommand.RaiseCanExecuteChanged();
        }

        private void DeleteOrder(Order order)
        {
            Repository.Orders.Remove(order);
            OrderItems.Remove(order);
        }
    }
}