using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Models;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Tmp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CargaPagina : ContentPage
    {

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;

        public static ICredentialsService CredentialsService { get; private set; }

        #endregion

        public CargaPagina()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {


            navigationService = new NavigationService();
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            CredentialsService = new CredentialsService();
            // Inicialización de credenciales

            Device.BeginInvokeOnMainThread(async () =>
            {



                if (CredentialsService.DoCredentialsExist())

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

                                    await Application.Current.SavePropertiesAsync();

                                    navigationService.SetMainPage("MasterDetailPagina");


                                }


                            }
                            else
                            {
                                navigationService.SetMainPage("MasterDetailPagina");


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
                    navigationService.SetMainPage("LoginPagina");
                }

            });



        }




        public async Task<TokenResponse> LoginCredentialsTokenAsync(String pUsu, String pPwd)
        {
            // Llamada al api para el login de usuario
            var response = await apiService.GetToken((String)Application.Current.Resources["AzureUrl"], pUsu, pPwd, null);

            if (response == null)
            {
                await dialogService.ShowMessage("Error", "The service is not available, please try latter.");
                return response;
            }

            /*if (string.IsNullOrEmpty(response.AccessToken))
            {
                await dialogService.ShowMessage("Error", response.ErrorDescription);
                return response;
            }*/

            return response;
        }

    }
}