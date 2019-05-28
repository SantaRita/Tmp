namespace Tmp.ViewModels
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Services;
    using Xamarin.Forms;

    public class PasswordConfirmViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        private const string V = "\", ";
        #endregion

        #region Services
        //ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
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

        //private string title = Resx.AppResources.Literal("CreateNewPassword");         /*public string Title
        {
            set
            {                 title = value;                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
            get
            {
                return title;
            }
        }*/

        public string EntryMailPassword
        {
            get;
            set;
        }

        public string EntryPassword
        {
            get;
            set;
        }

        public string EntryRepeatPassword
        {
            get;
            set;
        }

        public class ClasePwd
        {
            [JsonProperty("Email")]
            public string Email { get; set; }
            [JsonProperty("CurrentPassword")]
            public string CurrentPassword { get; set; }
            [JsonProperty("NewPassword")]
            public string NewPassword { get; set; }
        }
        #endregion

        #region Constructors
        public PasswordConfirmViewModel()
        {

            //apiService = new ApiService();
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

        async void Save()
        {
            /*if (string.IsNullOrEmpty(EntryMailPassword))
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("PasswordFill"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());

                return;
            }

            if (string.IsNullOrEmpty(EntryPassword))
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegPasswordRequiered"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
                //await dialogService.ShowMessage(
                //    Lenguages.Literal("Error"), Lenguages.Literal("RegPasswordRequiered"));
                return;
            }

            if (EntryPassword.Length < 6)
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegCharPasswordRequired"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
                //await dialogService.ShowMessage(
                //    Lenguages.Literal("Error"), Lenguages.Literal("RegCharPasswordRequired"));
                return;
            }

            if (string.IsNullOrEmpty(EntryRepeatPassword))
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegConfirmRequired"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
                //await dialogService.ShowMessage(
                //    Lenguages.Literal("Error"), Lenguages.Literal("RegConfirmRequired"));
                return;
            }

            if (!EntryPassword.Equals(EntryRepeatPassword))
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegMatchRequired"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
                //await dialogService.ShowMessage(
                //    Lenguages.Literal("Error"), Lenguages.Literal("RegMatchRequired"));
                return;
            }


            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var urlAPI = Application.Current.Resources["AzureUrl"].ToString();


            ChangePasswordRequest cadena = new ChangePasswordRequest();
            cadena.Email = MainViewModel.GetInstance().mailRecovery;
            cadena.CurrentPassword = EntryMailPassword;
            cadena.NewPassword = EntryPassword;

            //var json = JsonConvert.SerializeObject(cadena);

            var response = await apiService.PasswordConfirm(urlAPI, "/api", "/Customers/ChangePassword", cadena);



            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    Lenguages.Literal("Error"),
                    Lenguages.Literal("Recovery"));
                return;
            }

            MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
            Lenguages.Literal("NewPasswordCreated"),
            Lenguages.Literal("NewPasswordCreatedTitle"),
            "OK", null, "nulo");
            await PopupNavigation.Instance.PushAsync(new MessagePopPagina());

            //await dialogService.ShowMessage(
            //    Lenguages.Literal("NewPasswordCreated"),
            //    Lenguages.Literal("NewPasswordCreatedTitle"));

            Debug.WriteLine("0 poproot");

            // Hacemos logout y eleminamos los datos del usuario conectado
            App.CredentialsService.DeleteCredentials();
            await Application.Current.SavePropertiesAsync();

            Debug.WriteLine("1 poproot");

            // Log de Usuario de sesion
            //var data = new { IdUser = MainViewModel.GetInstance().Token.Customer.CustomerId, IdAction = 2 };
            //Util.enviarUserActionLog(data, MainViewModel.GetInstance().Token.Customer.CustomerId);

            //MainViewModel.GetInstance().Login = new LoginViewModel();
            //navigationService.SetMainPage("LoginPagina");

            Debug.WriteLine("Antes poproot");
            await App.Current.MainPage.Navigation.PopToRootAsync();
            Debug.WriteLine("Despues poproot");

            IsRunning = false;
            IsEnabled = true;
            */
        }


        #endregion
    }
}
