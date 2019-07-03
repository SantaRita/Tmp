using System;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePlanPagina : ContentPage
	{
		public WelcomePlanPagina ()
		{
			InitializeComponent ();
		}

        private async void BtBien_Clicked(object sender, EventArgs e)
        {
            NavigationService navigationService = new NavigationService();
            MainViewModel.GetInstance().TypePlan = "BIENES_01";
            await navigationService.NavigateDetail("MyPlanPagina");

        }

        private async void BtServicio_Clicked(object sender, EventArgs e)
        {
            NavigationService navigationService = new NavigationService();
            MainViewModel.GetInstance().TypePlan = "SERVICIOS_01";
            await navigationService.NavigateDetail("MyPlanPagina");

        }
    }
}