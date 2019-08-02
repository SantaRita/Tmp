using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Tmp.Models;

namespace Tmp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Properties
        public LoginViewModel Login { get; set; }
        public PlanesViewModel Planes { get; set; }
        public RegisterViewModel Register { get; set; }
        public PasswordRecoveryViewModel PasswordRecovery { get; set; }
        public PasswordConfirmViewModel PasswordConfirm { get; set; }
        public TerminosViewModel Terminos { get; set; }
        public MyPlanViewModel MyPlan { get; set; }
        public String TypePlan { get; set; }
        public MyPlan PlanActual { get; set; }
        public String EstadoRecPlan { get; set; }
        public String KeyPlan { get; set; }
        public String IdMyPlan { get; set; }
        public Boolean BotonTerminos { get; set; }
        public WelcomePlanViewModel WelcomePlan { get; set; }
        public DiagnosticoViewModel Diagnostico { get; set; }
        public ValoracionViewModel Valoracion { get; set; }
        public GenerandoViewModel Generando { get; set; }
        public CulminadoViewModel Culminado { get; set; }
        public AccionViewModel Accion { get; set; }
        public String AccionEnvio { get; set; }



        public TokenResponse Token { get; set; }
        public String MailRecovery { get; set; }
        public String User { get; set; }
        public Customer Customer { get; set; }
        public Boolean Ayuda { get; set; }


        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;

            Login = new LoginViewModel();
            //LoadMenu();
        }
        #endregion

        #region Methods

        /*public void RegisterDevice()
        {
            var register = DependencyService.Get<IRegisterDevice>();
            register.RegisterDevice();
        }

        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<MenuMaster>();

            MyMenu.Add(new MenuMaster
            {
                Icon = "username",
                PageName = "UserProfilePagina",
                Title = "User Profile",
            });

            MyMenu.Add(new MenuMaster
            {
                Icon = "username",
                PageName = "PackPagina",
                Title = "Packs",
            });

            MyMenu.Add(new MenuMaster
            {
                Icon = "username",
                PageName = "BalancePagina",
                Title = "Balance",
            });

            MyMenu.Add(new MenuMaster
            {
                Icon = "username",
                PageName = "SocialMediaPagina",
                Title = "Social Media",
            });

            MyMenu.Add(new MenuMaster
            {
                Icon = "logut",
                PageName = "LoginPagina",
                Title = "Logout",
            });


        }*/


        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }


        #endregion


    }
}
