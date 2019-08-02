using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Helpers;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccionPagina : ContentPage
	{

        private DialogService dialogService;


        public AccionPagina ()
		{
			InitializeComponent ();

            DialogService dialogService = new DialogService();

            if ( MainViewModel.GetInstance().AccionEnvio.Equals("GUARDAR"))
            {
                Titulo1.Text = Lenguages.Literal("GuardarInforme");
                Titulo2.Text = Lenguages.Literal("PorFavorGuardar");
                btCampo.Keyboard = Keyboard.Text;
                
            }
            else if (MainViewModel.GetInstance().AccionEnvio.Equals("ENVIAR"))
            {
                Titulo1.Text = Lenguages.Literal("EnviarInformePlan");
                Titulo2.Text = Lenguages.Literal("PorFavorEnviar");
                btCampo.Keyboard = Keyboard.Email;

            }
        }

        private async void BtOk(object sender, EventArgs e)
        {
            if (MainViewModel.GetInstance().AccionEnvio.Equals("GUARDAR")) {
                await Application.Current.MainPage.DisplayAlert("Tmp", Lenguages.Literal("SaveReportOk"), Lenguages.Literal("Ok"));
                //await dialogService.ShowMessage("Error", Lenguages.Literal("SaveReportOk"));
                
            }
            else if (MainViewModel.GetInstance().AccionEnvio.Equals("ENVIAR"))
            {
                await Application.Current.MainPage.DisplayAlert("Tmp", Lenguages.Literal("SendReportOk"), Lenguages.Literal("Ok"));
                
            }
            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("VolverMenu");


        }

        private async void BtCancelar(object sender, EventArgs e)
        {
            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("VolverMenu");
        }
    }
}