using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tmp.Services;

namespace Tmp.ViewModels
{
    public class AccionViewModel : INotifyPropertyChanged
    {


        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        NavigationService navigationService;
        ApiService apiService;
        DialogService dialogService;
        MainViewModel mainViewModel;

        #endregion



        #region Attributes 


        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                //OnPropertyChanged(nameof(IsRefreshing));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));

            }
        }



        #endregion

        #region Constructor
        public AccionViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            mainViewModel = MainViewModel.GetInstance();





        }


        #endregion

        #region Methods



        #endregion

    }
}

