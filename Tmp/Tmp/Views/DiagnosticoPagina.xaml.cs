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
	public partial class DiagnosticoPagina : ContentPage
	{
		public DiagnosticoPagina ()
		{
			InitializeComponent ();
		}

        private async void BtDiagnostico_Clicked(object sender, EventArgs e)
        {
            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("ValoracionPagina");

        }
    }
}