using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tmp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tmp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPaginaMaster : ContentPage
    {
        public ListView ListView;

        public MainPaginaMaster()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Nuevo Plan",
                IconSource = "icon.png",
                Pagina = "NuevoPlanPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Mis Planes",
                IconSource = "icon.png",
                Pagina = "PlanesPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Terminos y Condiciones",
                IconSource = "icon.png",
                Pagina = "TerminosPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Contacto",
                IconSource = "icon.png",
                Pagina = "ContactoPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Acerca de.",
                IconSource = "icon.png",
                Pagina = "AboutPagina",
                TargetType = typeof(AboutPagina)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Cerrar Sesión",
                IconSource = "icon.png",
                Pagina = "LogoutPagina",
                TargetType = typeof(AboutPagina)
            });

            menuItemsListView.ItemsSource = masterPageItems;


        }






    }
}