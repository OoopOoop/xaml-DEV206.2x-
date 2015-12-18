using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.Extensions
{
    public class RightClickMessageDialogBehavior : DependencyObject, IBehavior
    {
        public string Message { get; set; }
        public string Title { get; set; }

        public DependencyObject AssociatedObject { get; private set;}
     

        public void Attach(DependencyObject associatedObject)
        {
           if(associatedObject is Page)
            {
                AssociatedObject = associatedObject;
                (AssociatedObject as Page).RightTapped += Page_RightTapped;
            }
        }

        public void Detach()
        {
           if(this.AssociatedObject!=null&&this.AssociatedObject is Page)
            {
                AssociatedObject = null;
                (AssociatedObject as Page).RightTapped -= Page_RightTapped;
            }
        }

        private async void Page_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            await new MessageDialog(Message, Title).ShowAsync();
        }
    }
}
