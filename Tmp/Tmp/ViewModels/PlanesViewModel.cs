using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tmp.Services;

namespace Tmp.ViewModels
{
    public class PlanesViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        //ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Constructors
        public PlanesViewModel()
        {

            //apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

        }
        #endregion
    }
}

