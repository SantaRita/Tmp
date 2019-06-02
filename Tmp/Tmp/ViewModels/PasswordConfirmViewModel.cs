namespace Tmp.ViewModels
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Services;
    using Tmp.Helpers;
    using Tmp.Models;
    using Xamarin.Forms;

    public class PasswordConfirmViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        private const string V = "\", ";
        #endregion

        #region Services
        ApiService apiService;
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

        async void Save()
        {
            if (string.IsNullOrEmpty(EntryMailPassword))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter the current password.");
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();



            if (string.IsNullOrEmpty(EntryPassword))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a new password.");
                return;
            }

            if (EntryPassword.Length < 6)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "The new password must have at least 6 characters length.");
                return;
            }

            if (string.IsNullOrEmpty(EntryRepeatPassword))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You must enter a new password confirm.");
                return;
            }

            if (!EntryPassword.Equals(EntryRepeatPassword))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "The new password and confirm, does not match.");
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

            var changePasswordRequest = new ChangePasswordRequest
            {
                CurrentPassword = EntryMailPassword,
                Email = mainViewModel.MailRecovery,
                NewPassword = EntryPassword,
            };

            var urlAPI = Application.Current.Resources["AzureUrl"].ToString();

            var response = await apiService.ChangePassword(
                urlAPI,
                "/api",
                "/Customers/ChangePassword",
             /*   mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,*/
                changePasswordRequest);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response.Message);
                return;
            }

            //mainViewModel.Token.Password = EntryPassword;
            //dataService.Update(mainViewModel.Token);

            await dialogService.ShowMessage(
                "Confirm",
                "The password was changed successfully");
            await App.Current.MainPage.Navigation.PopToRootAsync();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
