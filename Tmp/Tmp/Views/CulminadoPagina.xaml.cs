using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CulminadoPagina : ContentPage
	{
		public CulminadoPagina ()
		{
			InitializeComponent ();
		}

        private async void BtGuardar(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().AccionEnvio = "GUARDAR";
            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("AccionPagina");

        }

        private async void BtEnviar(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().AccionEnvio = "ENVIAR";
            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("AccionPagina");
        }
    }
}