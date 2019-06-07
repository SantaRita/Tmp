using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tmp.Models;
using Tmp.Services;
using Tmp.ViewModels;
using Xamarin.Forms;

namespace Tmp.Helpers
{
    public static class SetObjectAction
    {
               
        public static async Task<Response> GetParametro(string par)
        {

            ApiService apiService = new ApiService();

            var mainViewModel = MainViewModel.GetInstance();


            // Cargamos los terminos

            var response = await apiService.GetParam((String)Application.Current.Resources["AzureUrl"], "/api", "/Params?id=" + par);

            return response;

        }


        public static string GetFormattedTime(int value)
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

  
        public static async Task<DateTime?> GetServerDate()
        {
            ApiService apiService = new ApiService();
            DialogService dialogService = new DialogService();

            try
            {

                // Cargamos los terminos

                var response = await apiService.GetServerDate(
                    (String)Application.Current.Resources["AzureUrl"],
                    "/api",
                    "/Customers/GetServerDate");

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);

                    return null;
                }
                return Convert.ToDateTime(response.Message);

            }
            catch
            {
                return null;
            }


        }



       


    }
}