using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Models;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPagina : MasterDetailPage
    {

        NavigationService navigationService = new NavigationService();

        public MainPagina()
        {
            InitializeComponent();

            Detail = new NavigationPage(new AboutPagina());


            mainPaginaMaster.menuItemsListView.ItemSelected += OnItemSelected;

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                //Application.Current.MainPage = new NavigationPage(new AboutPagina());
                //Application.Current.MainPage.Navigation.PushAsync(new AboutPagina());


                //MainViewModel.GetInstance().PasswordRecovery = new PasswordRecoveryViewModel();
                //NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);
                //NP.PushAsync(new PasswordRecoveryPagina());

                //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));

                await navigationService.NavigateDetail(item.Pagina);

                mainPaginaMaster.menuItemsListView.SelectedItem = null;
                IsPresented = false;
            }
        }


    }
}