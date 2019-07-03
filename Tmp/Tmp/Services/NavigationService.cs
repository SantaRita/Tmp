using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Tmp.ViewModels;
using Tmp.Views;
using Xamarin.Forms;

namespace Tmp.Services
{
    public class NavigationService
    {

        #region Methods
        Object _lockObject = new object();
        public void SetMainPage(string pageName)
        {



            switch (pageName)
            {

                case "LoginPagina":

                    Application.Current.MainPage = new NavigationPage(new LoginPagina());

                    break;

                case "CargaPagina":

                    Application.Current.MainPage = new NavigationPage(new CargaPagina());
                    break;

                case "MasterDetailPagina":

                    Application.Current.MainPage = new MainPagina();
                    break;
            }
        }


        public async Task Navigate(String pageName)

        {

            var stack = App.Current.MainPage.Navigation.NavigationStack;



            switch (pageName)
            {
                case "RegisterPagina":
                    await App.Current.MainPage.Navigation.PushAsync(new RegisterPagina());
                    break;
                case "TerminosPagina":
                    MainViewModel.GetInstance().BotonTerminos = true;
                    MainViewModel.GetInstance().Terminos = new TerminosViewModel();
                    await App.Current.MainPage.Navigation.PushAsync(new TerminosPagina());
                    break;
                case "PasswordConfirmPagina":
                    await App.Current.MainPage.Navigation.PushAsync(new PasswordConfirmPagina());
                    break;
                case "PasswordRecovery":
                    await App.Current.MainPage.Navigation.PushAsync(new PasswordRecoveryPagina());
                    break;
            }

            
        }


        public async Task NavigateDetail(String pageName)

        {

            var stack = App.Current.MainPage.Navigation.NavigationStack;
            NavigationPage NP = ((NavigationPage)((MasterDetailPage)App.Current.MainPage).Detail);


            switch (pageName)
            {
                case "AboutPagina":
                    
                    await NP.PushAsync(new AboutPagina());
                    break;


                case "WelcomePlanPagina":
                    MainViewModel.GetInstance().WelcomePlan = new WelcomePlanViewModel();
                    await NP.PushAsync(new WelcomePlanPagina());
                    break;

                case "MyPlanPagina":
                    MainViewModel.GetInstance().MyPlan = new MyPlanViewModel();
                    await NP.PushAsync(new MyPlanPagina());
                    break;

                case "TerminosPagina":
                    MainViewModel.GetInstance().BotonTerminos = false;
                    MainViewModel.GetInstance().Terminos = new TerminosViewModel();
                    await NP.PushAsync(new TerminosPagina());
                    break;

                case "ContactoPagina":

                    await NP.PushAsync(new ContactoPagina());
                    break;

                case "PlanesPagina":
                    MainViewModel.GetInstance().Planes = new PlanesViewModel();
                    await NP.PushAsync(new PlanesPagina());
                    break;

                case "LogoutPagina":

                    var res = await App.Current.MainPage.DisplayAlert("Cerrar sesión", "¿Desea abandonar la sesión?", "Si", "No");

                    if (res)
                    {


                        // Hacemos logout y eleminamos los datos del usuario conectado
                        App.CredentialsService.DeleteCredentials();
                        await Application.Current.SavePropertiesAsync();

                        // Unregister device for push notification //
                        App.Current.Properties.Remove("Token");

                        Application.Current.MainPage = new NavigationPage(new LoginPagina());

                    }
                    break;






            }


        }




        /*public async Task PaginaPrincipal()
        {
            var mainViewModel = MainViewModel.GetInstance();

            // Como entramos por aquí leemos las sharedpreferences// Leer preferencias





            if (Application.Current.Properties.ContainsKey("user_" + mainViewModel.Token.Customer.CustomerId))
            {
                if ((String)Application.Current.Properties["user_" + mainViewModel.Token.Customer.CustomerId] == mainViewModel.Token.Customer.Email)
                {
                    if (Application.Current.Properties["tip_" + mainViewModel.Token.Customer.CustomerId].Equals("1"))
                    {

                        // Miramos si ya hemos mostrado hoy el daily pagina para no mostrarlo mas
                        if (Xamarin.Forms.Application.Current.Properties.ContainsKey("dayling_" + MainViewModel.GetInstance().Token.Customer.CustomerId))
                        {
                            DateTime fecha = (System.DateTime)Application.Current.Properties["dayling_" + MainViewModel.GetInstance().Token.Customer.CustomerId];

                            Debug.WriteLine("Dayfecha: " + fecha.ToShortDateString());
                            Debug.WriteLine("_Dayfecha: " + DateTime.Now.ToShortDateString());
                            if (fecha.ToShortDateString() == DateTime.Now.ToShortDateString())
                            {


                                if ((Device.RuntimePlatform == Device.iOS))
                                    Application.Current.MainPage = new NavigationPage(new TabPagina());
                                else
                                    Application.Current.MainPage = new NavigationPage(new TabPagina1());
                                return;
                            }

                            // Date are diferent, swow daily
                            if ((Device.RuntimePlatform == Device.iOS))
                                Application.Current.MainPage = new NavigationPage(new TabPagina());
                            else
                                Application.Current.MainPage = new NavigationPage(new TabPagina1());

                            await Navigate("DailyPagina");

                            return;

                        }
                        else
                        {
                            if ((Device.RuntimePlatform == Device.iOS))
                                Application.Current.MainPage = new NavigationPage(new TabPagina());
                            else
                                Application.Current.MainPage = new NavigationPage(new TabPagina1());
                            await Navigate("DailyPagina");
                            return;
                        }
                    }
                    else
                    {

                        if ((Device.RuntimePlatform == Device.iOS))
                            Application.Current.MainPage = new NavigationPage(new TabPagina());
                        else
                            Application.Current.MainPage = new NavigationPage(new TabPagina1());
                        return;
                    }

                }

                if ((Device.RuntimePlatform == Device.iOS))
                    Application.Current.MainPage = new NavigationPage(new TabPagina());
                else
                    Application.Current.MainPage = new NavigationPage(new TabPagina1());
                return;
            }
            else
            {
                Application.Current.Properties.Remove("tip_" + mainViewModel.Token.Customer.CustomerId);
                Application.Current.Properties.Add("user_" + mainViewModel.Token.Customer.CustomerId, mainViewModel.Token.Customer.Email);
                Application.Current.Properties.Add("tip_" + mainViewModel.Token.Customer.CustomerId, "1");
                await Application.Current.SavePropertiesAsync();

                if ((Device.RuntimePlatform == Device.iOS))
                    Application.Current.MainPage = new NavigationPage(new TabPagina());
                else
                    Application.Current.MainPage = new NavigationPage(new TabPagina1());

                await Navigate("DailyPagina");

            }
        }



        public async Task BackOnMaster()
        {
            await App.Navigator.PopAsync();
        }

        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }*/
        #endregion
    }
}