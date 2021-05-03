using System;
using System.Collections.Generic;
using FCANoti.ViewModels;
using FCANoti.Views;
using Xamarin.Forms;

namespace FCANoti
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
