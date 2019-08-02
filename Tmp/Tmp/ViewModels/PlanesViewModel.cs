using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tmp.Helpers;
using Tmp.Models;
using Tmp.Services;
using Xamarin.Forms;

namespace Tmp.ViewModels
{
    public class PlanesViewModel : INotifyPropertyChanged
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
        ObservableCollection<MyPlan> _myplan;
        public List<MyPlan> myplans;
        private bool isRunning;

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

        Object _name;
        public object Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }


        private MyPlan _objectseleccionado;
        public MyPlan ObjectSeleccionado
        {
            get
            {
                return _objectseleccionado;
            }

            set
            {
                try
                {
                    if (_objectseleccionado != value && value != null)
                    {

                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var stack = App.Current.MainPage.Navigation.NavigationStack;
                                NavigationService navigationService = new NavigationService();
                                MainViewModel.GetInstance().PlanActual = value;
                                MainViewModel.GetInstance().TypePlan = value.KeyPlan;
                                MainViewModel.GetInstance().KeyPlan = value.KeyPlan;
                                await navigationService.NavigateDetail("MyPlanPagina");
                            });
                        




                    }
                }
                catch { }

            }
        }



        public ObservableCollection<MyPlan> MyPlans
        {
            get
            {
                return _myplan;
            }
            set
            {
                if (_myplan != value)
                {
                    _myplan = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MyPlans)));
                }
            }
        }

        Object _userAvatar;
        public Object UserAvatar
        {
            get
            {
                return _userAvatar;
            }
            set
            {
                if (_userAvatar != value)
                {
                    _userAvatar = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(UserAvatar)));
                }
            }
        }

        #endregion

        #region Constructor
        public PlanesViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            mainViewModel = MainViewModel.GetInstance();


            LoadPlans();


        }


        #endregion

        #region Methods

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadPlans();

                    IsRefreshing = false;
                });
            }
        }

        public string GetFormattedTime(int value)
        {
            var span = TimeSpan.FromSeconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }

        async Task LoadPlans()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                await dialogService.ShowMessage(
                    "Error", Lenguages.Literal("CheckConnection"));
                return;

            }
            else
            {



                var response = await apiService.GetMyPlans<MyPlan>(

                (String)Application.Current.Resources["AzureUrl"],
                "/api",
                    "/MyPlans/GetMyPlans" +
                    "?idCustomer=" + mainViewModel.Token.Customer.CustomerId


                );


                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }


                myplans = (List<MyPlan>)response.Result;

                MyPlans = new ObservableCollection<MyPlan>(myplans);


            }

        }





        
        public ICommand PageAppearingCommand { get { return new RelayCommand(PageAppearing); } }

        async private void PageAppearing()
        {

            IsRefreshing = true;

            await LoadPlans();

            IsRefreshing = false;

        }

   




        #endregion

    }
}

