using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;

namespace RestaurantManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {
        private string _request;
        private ObservableCollection<MenuItem> _currentlySelectedMenuItems;

        public ObservableCollection<MenuItem> CurrentlySelectedMenuItems
        {
            get { return _currentlySelectedMenuItems; }
            set
            {
                if (value != _currentlySelectedMenuItems)
                {
                    _currentlySelectedMenuItems = value;
                    OnPropertyChanged(nameof(CurrentlySelectedMenuItems));
                }
            }
        }

        public string Request
        {
            get { return _request; }
            set
            {
                if (value != _request)
                {
                    _request = value;
                    OnPropertyChanged(nameof(Request));
                }
            }
        }

        public List<MenuItem> MenuItems { get; set; }
        public DelegateCommand<MenuItem> AddToOrderCommand { get; private set; }
        public DelegateCommand<object> SubmitOrderCommand { get; private set; }

        protected override void OnDataLoaded()
        {
            this.MenuItems = base.Repository.StandardMenuItems;
            //OnPropertyCahnged not added to set method so we call it ourself
            this.OnPropertyChanged(nameof(MenuItems));
            this.CurrentlySelectedMenuItems = new ObservableCollection<MenuItem>();
        }

        public OrderViewModel()
        {
            AddToOrderCommand = new DelegateCommand<MenuItem>(AddToOrder);
            SubmitOrderCommand = new DelegateCommand<object>(SubmitOrder, CanSubmitOrder);
        }

        // Displaying message when an order was submitted
        private async void DisplaypopupMessage()
        {
            var messageDialog = new MessageDialog("Order Submitted");
            await messageDialog.ShowAsync();
            SubmitOrderCommand.RaiseCanExecuteChanged();
        }

        //Enables/Disables "Sumbit Order" button
        private bool CanSubmitOrder(object obj) => CurrentlySelectedMenuItems.Count != 0 ? true : false;

        private void SubmitOrder(object obj)
        {
            List<MenuItem> selectedItems = CurrentlySelectedMenuItems.ToList();

            var order = new Order();
            order.Complete = false;
            order.SpecialRequests = String.IsNullOrEmpty(Request) ? "N/A" : Request;
            order.Table = new Table { Description = "Corner" };
            order.Items = selectedItems;
            Repository.Orders.Add(order);
            Request = String.Empty;
            CurrentlySelectedMenuItems.Clear();

            DisplaypopupMessage();
        }

        private void AddToOrder(MenuItem menuItem)
        {
            CurrentlySelectedMenuItems.Add(menuItem);
            SubmitOrderCommand.RaiseCanExecuteChanged();
        }
    }
}