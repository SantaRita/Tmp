using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Tmp.Helpers;
using Tmp.Models;
using Tmp.Services;
using Xamarin.Forms;

namespace Tmp.ViewModels
{
    public class RegisterViewModel :  INotifyPropertyChanged
    {


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRefreshing;



        public int Terminos = 0;

        bool _isRunning;
        bool _isEnabled;


        #endregion

        #region Properties










        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }

        public String Name { get; set; }

        public String TipoEmpresa { get; set; }

        public String Email { get; set; }

        public string Password { get; set; }

        public string Confirm { get; set; }


        #endregion

        #region Constructors
        public RegisterViewModel()
        {

            IsRunning = false;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            IsEnabled = true;
        }
        #endregion


        #region Commands


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {

            if (string.IsNullOrEmpty(Email))
            {

                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("RegMailRequired"));
                return;
            }

            if (!RegexUtilities.IsValidEmail(Email))
            {


                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("RegMailValid"));
                return;
            }


            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("Password Required"));

                return;
            }

            if (Password.Length < 6)
            {
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("Formato Password incorrecto"));

                return;
            }

            if (string.IsNullOrEmpty(Confirm))
            {
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("Confirm Password required"));

                return;
            }

            if (!Password.Equals(Confirm))
            {
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("Passwordnocoinciden"));

                return;
            }



            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), Lenguages.Literal("ErrorConexion"));

                return;
            }


            // Miramos que no exista el usuario

            int responseMail = await apiService.GetExistsUser((String)Application.Current.Resources["AzureUrl"], Email);


            if (responseMail == 1)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), "El email ya existe. Seleccione otro");

                return;
            }





            var customer = new Customer
            {

                Email = Email,
                Password = Password,
                Name = Name,
                TipoEmpresa = TipoEmpresa

            };

            // Llamamos a la función de crear usuario sin token.

            var response = await apiService.Post(
                (String)Application.Current.Resources["AzureUrl"],
                "/api",
                "/Customers",
                customer
                );

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            // Recuperamos el id de usuario

            // Volvemos a pedir el token del usuario que hemos registrado
            // y cargamos en memoria el token a la mainviewmodel

            var response2 = await apiService.GetToken(
                (String)Application.Current.Resources["AzureUrl"],
                Email,
                Password,
                null);

            if (response2 == null)
            {
                IsRunning = false;
                IsEnabled = true;

                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"), "Server error login");




                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response2.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;


                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"),
                    response2.ErrorDescription);
                Password = null;
                return;
            }

            // Si llego hasta aquí es porque el login es ok y vamos a la página principal
            // Guardamos el token para usarlo en cualquier momento de la app.
            /*bool doCredentialsExist = App.CredentialsService.DoCredentialsExist();
            if (!doCredentialsExist)
            {
                App.CredentialsService.SaveCredentials(Email, Password);
            }*/
            // Ponemos los datos a nivel de sesion del usuario
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.User = Email;
            mainViewModel.Customer = customer;
            mainViewModel.Customer.CustomerId = (int)response2.Customer.CustomerId;
            mainViewModel.Token = response2;


            IsRunning = false;
            IsEnabled = true;

            /*Application.Current.Properties.Remove("heart_" + mainViewModel.Token.Customer.CustomerId);
            Application.Current.Properties.Remove("feel_" + mainViewModel.Token.Customer.CustomerId);
            Application.Current.Properties.Remove("tip_" + mainViewModel.Token.Customer.CustomerId);
            Application.Current.Properties.Add("user_" + mainViewModel.Token.Customer.CustomerId, mainViewModel.Token.Customer.Email);

            Application.Current.Properties.Add("heart_" + mainViewModel.Token.Customer.CustomerId, "1");
            Application.Current.Properties.Add("feel_" + mainViewModel.Token.Customer.CustomerId, "1");
            Application.Current.Properties.Add("tip_" + mainViewModel.Token.Customer.CustomerId, "1");*/

            navigationService.SetMainPage("MasterDetailPagina");


        }




        #endregion

    }
}
