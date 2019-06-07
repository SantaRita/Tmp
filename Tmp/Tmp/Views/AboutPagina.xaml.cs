using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPagina : ContentPage
	{
		public AboutPagina ()
		{
			InitializeComponent ();

            version.Text = "App version " + DependencyService.Get<IAppVersion>().GetVersion().ToString()
                + " (" + DependencyService.Get<IAppVersion>().GetBuild().ToString() + ")";
        }
	}
}