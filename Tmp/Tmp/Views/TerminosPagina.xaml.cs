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
	public partial class TerminosPagina : ContentPage
	{
		public TerminosPagina ()
		{
			InitializeComponent ();

            if ( MainViewModel.GetInstance().BotonTerminos )
            {
                btAceptar.IsVisible = true;
                return;
            }
            btAceptar.IsVisible = false;
        }

        private async void BtAceptar_Clicked(object sender, EventArgs e)
        {
            NavigationService navigationService = new NavigationService();
            await navigationService.Navigate("RegisterPagina");

        }
    }
}