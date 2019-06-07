namespace Tmp.ViewModels
{
    using Tmp.Helpers;
    using Tmp.Models;
    using Services;
    using System;
    using System.ComponentModel;
    using Xamarin.Forms;

    public class WelcomePlanViewModel : INotifyPropertyChanged
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
        public WelcomePlanViewModel()
        {
            instance = this;

            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();



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
        static WelcomePlanViewModel instance;

        public static WelcomePlanViewModel GetInstance()
        {
            if (instance == null)
            {
                return new WelcomePlanViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods


        #endregion

        #region Properties


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
