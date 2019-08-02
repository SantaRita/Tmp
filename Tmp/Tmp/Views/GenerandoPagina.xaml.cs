using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GenerandoPagina : ContentPage
	{
		public GenerandoPagina ()
		{
			InitializeComponent();
		}

        private async void BtGenerando_Clicked(object sender, EventArgs e)
        {

            NavigationService navigationService = new NavigationService();
            await navigationService.NavigateDetail("CulminadoPagina");


        }
    }
}