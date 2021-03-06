﻿using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Tmp.Helpers;
using Tmp.Services;
using Xamarin.Auth;
using Xamarin.Forms;

namespace Tmp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {


        #region Services
        ApiService apiService;
        private DialogService dialogService;
        private NavigationService navigationService;


        #endregion

        #region Attributes
        private String email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email
        {
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Email"));
                }
            }
            get
            {
                return email;
            }
        }

        public string Password
        {
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
                }
            }
            get
            {
                return password;
            }
        }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnabled
        {
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnabled"));
                }
            }
            get
            {
                return isEnabled;
            }
        }


        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            // incializamos valores. Por defecto los boolean son falso
            isEnabled = true;




        }
        #endregion

        #region Commands


        // *******************   LOGIN   ***********************
        public ICommand LoginCommand => new  RelayCommand(Login);
        private async void Login()
        {


            try
            {

                if (string.IsNullOrEmpty(Email))
                {
                    await dialogService.ShowMessage("Error", Lenguages.Literal("RegMailRequired"));
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    await dialogService.ShowMessage("Error", Lenguages.Literal("PasswordRequired"));
                    return;
                }

                IsRunning = true;
                IsEnabled = false;

                if (!CrossConnectivity.Current.IsConnected)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", Lenguages.Literal("CheckConnection"));
                    return;
                }

                var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
                if (!isReachable)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", Lenguages.Literal("CheckConnection"));
                    return;
                }

                // Llamada al api para el login de usuario
                var response = await apiService.GetToken((String)Application.Current.Resources["AzureUrl"], Email, Password, null);

                if (response == null)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", Lenguages.Literal("ServerErrorLogin"));
                    Password = null;
                    // Borramos las credencial al no poder entrar.
                    /*var account = AccountStore.Create().FindAccountsForService(App.AppName);
                    if (account != null)
                    {
                        AccountStore.Create().Delete(account., App.AppName);

                    }*/
                    return;
                }

                if (string.IsNullOrEmpty(response.AccessToken))
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage("Error", response.ErrorDescription);
                    Password = null;
                    return;
                }

                // Si llego hasta aquí es porque el login es ok y vamos a la página principal
                // Guardamos el token para usarlo en cualquier momento de la app.

                bool doCredentialsExist = App.CredentialsService.DoCredentialsExist();
                
                if (!doCredentialsExist)
                {
                    App.CredentialsService.SaveCredentials(Email, Password);
                }

                // Ponemos los datos a nivel de sesion del usuario
                // Device Registation for push notification //
                //DependencyService.Get<IPushNotification>().DeviceRegisterforPushNotification(Email);
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = response;
                mainViewModel.User = Email;
                mainViewModel.Customer = response.Customer;
                Email = null;
                Password = null;
                IsRunning = false;
                IsEnabled = true;
                mainViewModel.Token = response;


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

                    await Application.Current.SavePropertiesAsync();

                    navigationService.SetMainPage("MasterDetailPagina");

                }

                //navigationService.SetMainPage("MasterDetailPagina");
            }
            catch (Exception ex)
            {
                var ss = ex.Message;
            }
        }


        public ICommand RegisterNewUserCommand => new RelayCommand(Register);
        private async void Register()
        {
            Debug.WriteLine("Entramos Register");
            //await navigationService.Navigate("RegisterPagina");
            await navigationService.Navigate("TerminosPagina");
        }

        

        /* private async void LoginCopy()
         {
             if (string.IsNullOrEmpty(Email))
             {
                 await dialogService.ShowMessage("Error", "You must enter the user email.");
                 return;
             }

             if (string.IsNullOrEmpty(Password))
             {
                 await dialogService.ShowMessage("Error", "You must enter a password.");
                 return;
             }

             IsRunning = true;
             IsEnabled = false;

             if (!CrossConnectivity.Current.IsConnected)
             {
                 IsRunning = false;
                 IsEnabled = true;
                 await dialogService.ShowMessage("Error", "Check your internet connection.");
                 return;
             }

             var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
             if (!isReachable)
             {
                 IsRunning = false;
                 IsEnabled = true;
                 await dialogService.ShowMessage("Error", "Check your internet connection.");
                 return;
             }

             // Llamada al api para el login de usuario
             var response = await apiService.GetToken((String)Application.Current.Resources["AzureUrl"], Email, Password);

             if (response == null)
             {
                 IsRunning = false;
                 IsEnabled = true;
                 await dialogService.ShowMessage("Error", "The servie is not available, please try latter.");
                 Password = null;
                 return;
             }

             if (string.IsNullOrEmpty(response.AccessToken))
             {
                 IsRunning = false;
                 IsEnabled = true;
                 await dialogService.ShowMessage("Error", response.ErrorDescription);
                 Password = null;
                 return;
             }

             // Si llego hasta aquí es porque el login es ok y vamos a la página principal
             // Guardamos el token para usarlo en cualquier momento de la app.
             bool doCredentialsExist = App.CredentialsService.DoCredentialsExist();
             if (!doCredentialsExist)
             {
                 App.CredentialsService.SaveCredentials(Email, Password);
             }

             // Ponemos los datos a nivel de sesion del usuario

             var mainViewModel = MainViewModel.GetInstance();
             mainViewModel.Token = response;
             mainViewModel.User = Email;
             mainViewModel.Customer = response.Customer;

             navigationService.SetMainPage("MasterPagina");


             Email = null;
             Password = null;
             IsRunning = false;
             IsEnabled = true;



         }*/



        /*public ICommand RegisterNewUserCommand
        {
            get
            {
                return new RelayCommand(RegisterNewUser);
            }
        }

        async void RegisterNewUser()
        {


            MainViewModel.GetInstance().NewCustomer = new NewCustomerViewModel();
            await navigationService.NavigateOnLogin("NewCustomerPagina");
        }*/

        public ICommand RecoverPasswordCommand
        {
            get
            {
                return new RelayCommand(RecoverPassword);
            }
        }

        async void RecoverPassword()
        {
            MainViewModel.GetInstance().PasswordRecovery = new PasswordRecoveryViewModel();
            await navigationService.Navigate("PasswordRecovery");
        }





        #endregion


    }
}
