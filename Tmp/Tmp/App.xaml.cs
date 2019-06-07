using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tmp.Services;
using Tmp.Views;
using Tmp.Interfaces;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Tmp.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tmp
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://localhost:5000";
        public static bool UseMockDataStore = true;

        public static int StatusBarHeight { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Properties
        public static string AppName { get { return "Tmp"; } }
        public static ICredentialsService CredentialsService { get; private set; }
        #endregion
        public App()
        {

            NavigationService navigationService = new NavigationService();

            System.Diagnostics.Debug.WriteLine("====== resource debug info =========");
            var assembly = typeof(App).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            System.Diagnostics.Debug.WriteLine("====================================");



            InitializeComponent();

            // This lookup NOT required for Windows platforms - the Culture will be automatically set
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS || Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
            {
                // determine the correct, supported .NET culture
                CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                //Resx.AppResources.Culture = ci; // set the RESX for resource localization

                Debug.WriteLine("Idimoa:" + CultureInfo.CurrentUICulture.ToString());
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

            /*if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();*/


            CredentialsService = new CredentialsService();


            navigationService.SetMainPage("CargaPagina");



        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
