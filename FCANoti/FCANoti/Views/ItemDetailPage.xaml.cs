using System.ComponentModel;
using Xamarin.Forms;
using FCANoti.ViewModels;

namespace FCANoti.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}