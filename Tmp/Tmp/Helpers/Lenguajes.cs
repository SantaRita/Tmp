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
            static Lenguages()
            {
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.Culture = ci;
                DependencyService.Get<ILocalize>().SetLocale(ci);
            }

            public static string Literal(string cadena)
            {

                Type type = typeof(AppResources);
                MethodInfo method = type.GetMethod(cadena);
                AppResources c = new AppResources();
                string result = (string)method.Invoke(c, null);
                return result;

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
