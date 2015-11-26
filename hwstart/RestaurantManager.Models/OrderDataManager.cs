using System.Collections.Generic;

namespace RestaurantManager.Models
{
    public class OrderDataManager : DataManager
    {       
        protected override void OnDataLoaded()
        {
            this.MenuItems = base.Repository.StandardMenuItems;

            //OnPropertyChanged is called from within the Set method
            this.CurrentlySelectedMenuItems = new List<MenuItem>
            {
                this.MenuItems[3],
                this.MenuItems[5]
            };

            //OnPropertyCahnged not added to set method so we call it ourself
            this.OnPropertyChanged(nameof(MenuItems));
        }

        private List<MenuItem> _currentlySelectedMenuItems;
        public List<MenuItem> CurrentlySelectedMenuItems
        {
            get { return _currentlySelectedMenuItems; }
            set
            {
                if (value != _currentlySelectedMenuItems)
                {
                    _currentlySelectedMenuItems = value;
                    OnPropertyChanged();
                }
            }
        }


        public List<MenuItem> MenuItems { get; set; }
    }
}
