using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactoPagina : ContentPage
	{
        Models.Response resFacebook;
        Models.Response resLinkedin;
        Models.Response resSkype;

        public ContactoPagina ()
		{
            Device.BeginInvokeOnMainThread(async () =>
            {
                resFacebook = await SetObjectAction.GetParametro("LINKEDIN");
                resLinkedin = await SetObjectAction.GetParametro("FACEBOOK");
                resSkype = await SetObjectAction.GetParametro("SKYPE");
            } );

            InitializeComponent ();
		}

        private void Linkedin_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(resLinkedin.Message));

        }

        private void Skype_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(resSkype.Message));

        }

        private void Facebook_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(resFacebook.Message));

        }


    }
}