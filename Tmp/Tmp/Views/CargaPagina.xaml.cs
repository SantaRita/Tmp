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
	public partial class CargaPagina : ContentPage
	{

        NavigationService navigationService;

        public CargaPagina ()
		{
			InitializeComponent ();
		}


        protected override void OnAppearing()
        {


            navigationService = new NavigationService();
            // Inicialización de credenciales

            Device.BeginInvokeOnMainThread(async () =>
            {


                navigationService.SetMainPage("LoginPagina");


                /*if (CredentialsService.DoCredentialsExist())

                {
                    // Como ya existen las credencionales vamos a recuperar los datos
                    // del usuario de la base de datos de Azure mediante el WS.
                    try
                    {

                        var response = await LoginCredentialsTokenAsync(CredentialsService.UserName, CredentialsService.Password);

                        if (response == null)
                        {
                            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                            if (account != null)
                            {
                                AccountStore.Create().Delete(account, App.AppName);

                            }
                        }



                        if (response != null)
                        {

                            var mainViewModel = MainViewModel.GetInstance();
                            //mainViewModel.Home = new HomeViewModel();
                            mainViewModel.Token = response;
                            //mainViewModel.MenuHamburguer = new MenuHamburguerViewModel();

                            // Como entramos por aquí leemos las sharedpreferences// Leer preferencias
                            //navigationService.SetMainPage("TabPagina");

                            if (Application.Current.Properties.ContainsKey("user_" + mainViewModel.Token.Customer.CustomerId))
                            {
                                if ((String)Application.Current.Properties["user_" + mainViewModel.Token.Customer.CustomerId] == mainViewModel.Token.Customer.Email)
                                {
                                    if (!Application.Current.Properties.ContainsKey("heart_" + mainViewModel.Token.Customer.CustomerId))
                                    {
                                        Application.Current.Properties.Add("heart_" + mainViewModel.Token.Customer.CustomerId, "1");
                                    }
                                    if (!Application.Current.Properties.ContainsKey("tip_" + mainViewModel.Token.Customer.CustomerId))
                                    {
                                        Application.Current.Properties.Add("tip_" + mainViewModel.Token.Customer.CustomerId, "1");
                                    }
                                    if (!Application.Current.Properties.ContainsKey("feel_" + mainViewModel.Token.Customer.CustomerId))
                                    {
                                        Application.Current.Properties.Add("feel_" + mainViewModel.Token.Customer.CustomerId, "1");
                                    }
                                    await Application.Current.SavePropertiesAsync();

                                    //navigationService.SetMainPage("TabPagina");
                                    await navigationService.PaginaPrincipal();

                                }


                            }
                            else
                            {
                                Application.Current.Properties.Remove("heart_" + mainViewModel.Token.Customer.CustomerId);
                                Application.Current.Properties.Remove("tip_" + mainViewModel.Token.Customer.CustomerId);
                                Application.Current.Properties.Remove("feel_" + mainViewModel.Token.Customer.CustomerId);

                                Application.Current.Properties.Add("user_" + mainViewModel.Token.Customer.CustomerId, mainViewModel.Token.Customer.Email);
                                Application.Current.Properties.Add("heart_" + mainViewModel.Token.Customer.CustomerId, "1");
                                Application.Current.Properties.Add("tip_" + mainViewModel.Token.Customer.CustomerId, "1");
                                Application.Current.Properties.Add("feel_" + mainViewModel.Token.Customer.CustomerId, "1");

                                await Application.Current.SavePropertiesAsync();
                                //navigationService.SetMainPage("HeartRatePagina");
                                // if (Device.RuntimePlatform == Device.iOS)
                                //navigationService.SetMainPage("TabPagina");
                                await navigationService.PaginaPrincipal();

                                //  else
                                //  navigationService.SetMainPage("TabPagina1");

                            }


                        }
                        else
                        {
                            navigationService.SetMainPage("LoginPagina");

                        }

                        //await Imagen.FadeTo(0, 800);


                    }
                    catch (Exception ex)
                    {
                        CredentialsService.DeleteCredentials();
                        navigationService.SetMainPage("LoginPagina");
                    }



                }
                else
                {
                    //await Imagen.FadeTo(0, 800);
                    navigationService.SetMainPage("LoginPagina");
                }
                */
            });



        }
    }
}