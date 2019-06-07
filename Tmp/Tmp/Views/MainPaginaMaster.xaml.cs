using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tmp.Helpers;
using Tmp.Models;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPaginaMaster : ContentPage
    {
        public ListView ListView;

        public MainPaginaMaster()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = Lenguages.Literal("NewPlan"),
                IconSource = "icon.png",
                Pagina = "WelcomePlanPagina",
                TargetType = typeof(AboutPagina)
            });
            /*masterPageItems.Add(new MasterPageItem
            {
                Title = "Mis Planes",
                IconSource = "icon.png",
                Pagina = "PlanesPagina",
                TargetType = typeof(AboutPagina)
            });*/
            masterPageItems.Add(new MasterPageItem
            {
                Title = Lenguages.Literal("Terms"),
                IconSource = "icon.png",
                Pagina = "TerminosPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = Lenguages.Literal("Contact"),
                IconSource = "icon.png",
                Pagina = "ContactoPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = Lenguages.Literal("About"),
                IconSource = "icon.png",
                Pagina = "AboutPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = Lenguages.Literal("CloseSession"),
                IconSource = "icon.png",
                Pagina = "LogoutPagina",
                TargetType = typeof(AboutPagina)
            });

            menuItemsListView.ItemsSource = masterPageItems;

            UserName.Text = MainViewModel.GetInstance().Token.Customer.Name;
            Mail.Text = MainViewModel.GetInstance().Token.Customer.Email;

        }







    }
}