namespace Tmp.ViewModels
{
    using Tmp.Helpers;
    using Tmp.Models;
    using Services;
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    public class TerminosViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private String _terminos;
        bool _isRefreshing;
        bool _isRunning;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        NavigationService navigationService;
        ApiService apiService;
        DialogService dialogService;

        #endregion

        #region Constructor
        public TerminosViewModel()
        {
            instance = this;

            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();


            LoadTerms();

        }

        public bool IsRunning
        {
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return _isRunning;
            }
        }

        #endregion

        #region Sigleton
        static TerminosViewModel instance;

        public static TerminosViewModel GetInstance()
        {
            if (instance == null)
            {
                return new TerminosViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods

        async void LoadTerms()
        {
            IsRunning = true;
            IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {


            }
            else
            {
                var mainViewModel = MainViewModel.GetInstance();


                // Cargamos los terminos
                var response = await apiService.GetTermAndConditionCurrent(
                    (String)Application.Current.Resources["AzureUrl"],
                    "/api", "/TermAndConditions/GetTermAndConditionCurrent?" + "languaje=" + Lenguages.GetIdioma()   );
                this.Terminos = response.Message;



                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage("Error", response.Message);
                    _isRunning = false;
                    return;
                }


            }

            IsRefreshing = false;
            IsRunning = false;

        }
        #endregion

        #region Properties
        public string Terminos
        {
            get
            {
                return _terminos;
            }
            set
            {
                if (_terminos != value)
                {
                    _terminos = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Terminos)));
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
        #endregion


    }
}
