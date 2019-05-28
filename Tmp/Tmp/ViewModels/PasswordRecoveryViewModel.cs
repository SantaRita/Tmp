namespace Tmp.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class PasswordRecoveryViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
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

        public string Email
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public PasswordRecoveryViewModel()
        {
           // apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            // Title = Resx.AppResources.Literal("ResetPassword");
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
            /*if (string.IsNullOrEmpty(Email))
            {

                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegMailValid"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());

                return;
            }

            if (!RegexUtilities.IsValidEmail(Email))
            {
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("RegMailValid"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());

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

            var response = await apiService.PasswordRecovery(urlAPI, "/api", "/Customers/PasswordRecovery", Email);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
                "Error",
                Lenguages.Literal("Recovery"),
                "OK", null, "nulo");
                await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
                //await dialogService.ShowMessage( Lenguages.Literal("Error"),Lenguages.Literal("Recovery"));
                return;
            }

            MainViewModel.GetInstance().MessagePop = new MessagePopViewModel(
            Lenguages.Literal("WeSend"),
            Lenguages.Literal("RecoveryMail"),
            "OK", null, "nulo");
            await PopupNavigation.Instance.PushAsync(new MessagePopPagina());
            //await dialogService.ShowMessage( Lenguages.Literal("WeSend"),  Lenguages.Literal("RecoveryMail"));


            //await navigationService.BackOnLogin();

            MainViewModel.GetInstance().mailRecovery = Email;
            */


            await navigationService.Navigate("PasswordConfirmPagina");

            IsRunning = false;
            IsEnabled = true;
        }

        /*public string Title
        {
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
            get
            {
                return title;
            }
        }*/

        public ICommand VolverCommand
        {
            get
            {
                return new RelayCommand(Volver);
            }
        }

        async void Volver()
        {

            await App.Current.MainPage.Navigation.PopAsync();

        }
        #endregion
    }
}
