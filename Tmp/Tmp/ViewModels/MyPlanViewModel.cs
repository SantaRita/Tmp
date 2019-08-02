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
    public class MyPlanViewModel : INotifyPropertyChanged
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
        int PaginaActual = 1;

        ObservableCollection<MyPlan> _myplan;
        ObservableCollection<QuestionClass> _myQuestion;
        public MyFullPlan myFullPlan;
        public List<MyPlan> myplans;
        private bool isRunning;

        private bool _ayudaVisible = false;
        public bool AyudaVisible
        {
            get { return _ayudaVisible; }
            set
            {
                _ayudaVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AyudaVisible)));

            }
        }

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

        String _nombreplan;
        public String NombrePlan
        {
            get { return _nombreplan; }
            set
            {
                if (_nombreplan != value)
                {
                    _nombreplan = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombrePlan)));
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
                            var mainViewModel = MainViewModel.GetInstance();
                            await navigationService.Navigate("PlayPagina");
                        });





                    }
                }
                catch { }

            }
        }



        public ObservableCollection<QuestionClass> MyQuestions
        {
            get
            {
                return _myQuestion;
            }
            set
            {
                if (_myQuestion != value)
                {
                    _myQuestion = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MyQuestions)));
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
        public MyPlanViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            mainViewModel = MainViewModel.GetInstance();


            LoadPlan();


        }


        #endregion

        #region Methods

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

        async Task LoadPlan()
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



                var response = await apiService.GetMyFullPlans<MyFullPlan>(

                (String)Application.Current.Resources["AzureUrl"],
                "/api",
                    "/MyPlans/GetMyPlanQuestions" +
                    "?idCustomer=" + mainViewModel.Token.Customer.CustomerId +
                    "&language=ES&idplan=1&idmyplan=1&keyplan=" + MainViewModel.GetInstance().TypePlan


                );


                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }


                myFullPlan = (MyFullPlan)response.Result;

                NombrePlan = myFullPlan.PlanHead.Name;

                //MyQuestions = new ObservableCollection<QuestionClass>(myFullPlan.PlanQuestions.FindAll(a => a.Page == PaginaActual));
                FiltrarPreguntas();


            }

        }


        private void FiltrarPreguntas ()
        {
            MyQuestions = new ObservableCollection<QuestionClass>(myFullPlan.PlanQuestions.FindAll(a => a.Page == PaginaActual));

        }



        public ICommand BtAnteriorCommand
        {
            get
            {
                return new RelayCommand(BtAnterior);
            }
        }

        private async void BtAnterior()
        {
            PaginaActual--;
            FiltrarPreguntas();
        }

        public ICommand BtSiguienteCommand
        {
            get
            {
                return new RelayCommand(BtSiguiente);

            }
        }

        private async void BtSiguiente()
        {
            PaginaActual++;
            FiltrarPreguntas();
        }




        #endregion

    }
}

