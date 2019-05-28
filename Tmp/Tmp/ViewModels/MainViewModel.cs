using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

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
