using System;
using System.Reflection;
using System.Resources;
using Tmp.Interfaces;
using Tmp.Resx;
using Xamarin.Forms;

namespace Tmp.Helpers
{


    public static class Lenguages
        {

        private static global::System.Globalization.CultureInfo resourceCulture;

        static Lenguages()
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci);
            }

        public static string Literal(string cadena)         {
            global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Tmp.Resx.AppResources", typeof(AppResources).Assembly);
            return temp.GetString(cadena, resourceCulture);
            //return AppResources.Literal(cadena);
        }

        public static string GetIdioma()
            {
                var idioma = AppResources.Culture.CompareInfo.Name.ToString().ToUpper().Substring(0, 2);

                if (idioma.Equals("ES") || idioma.Equals("EN"))
                {
                    return idioma;
                }

                idioma = AppResources.Culture.Parent.Name.ToString().ToUpper().Substring(0, 2);

                if (idioma.Equals("ES") || idioma.Equals("EN"))
                {
                    return idioma;
                }

                return "EN";
            }
        }


}
