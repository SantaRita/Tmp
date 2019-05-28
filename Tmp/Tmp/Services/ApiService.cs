namespace Tmp.Services
{

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Tmp.Models;
    using Tmp.ViewModels;

    public class ApiService
    {

        public async Task<Response> GetParam(string urlBase,
     string servicePrefix,
     string controller
     )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(resultJSON);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),

                    };
                }


                JObject s = JObject.Parse(resultJSON);
                string parametro = (string)s["Value"];
                return new Response
                {
                    IsSuccess = true,
                    Message = parametro,


                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> SaveUserTest(
            string urlBase,
            string servicePrefix,
            string controller,
            string test

            //tokenType,
            ////ChangePasswordRequest changePasswordRequest
            )
        {
            try
            {
                var request = test;
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetCities<T>(string urlBase,
        string servicePrefix,
        string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);


                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        // get histories de UserObjectMovements
        public async Task<Response> GetHistory<T>(string urlBase,
        string servicePrefix,
        string controller

        )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        // UserObjectStatus/LastStatus
        //*************************************************************
        public async Task<Response> LastStatus<T>(string urlBase,
                string servicePrefix,
                string controller

            )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }



        // get objects by pack
        //*************************************************************
        public async Task<Response> GetObjectsByPack<T>(string urlBase,
                string servicePrefix,
                string controller

            )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> GetTips<T>(string urlBase,
        string servicePrefix,
        string controller)
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> GetPacksByLevel<T>(string urlBase,
                string servicePrefix,
                string controller

            )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> GetQuestionsTest<T>(string urlBase,
                string servicePrefix,
                string controller

            )
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetServerDate(string urlBase, string servicePrefix, string controller)
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(resultJSON);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),

                    };
                }


                JObject s = JObject.Parse(resultJSON);
                string fecha = s["Fecha"].ToString();
                return new Response
                {
                    IsSuccess = true,
                    Message = fecha,


                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetTermAndConditionCurrent(string urlBase,
             string servicePrefix,
             string controller)
        {

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject(resultJSON);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),

                    };
                }


                JObject s = JObject.Parse(resultJSON);
                string condiciones = (string)s["Condiciones"];
                return new Response
                {
                    IsSuccess = true,
                    Message = condiciones,


                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }




        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
            };

        }

        public async Task<Response> SendMail(
             string urlBase,
             string servicePrefix,
             string controller,
             MailRequest mail)
        {
            try
            {
                var request = JsonConvert.SerializeObject(
                                mail);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> PasswordConfirm(
             string urlBase,
             string servicePrefix,
             string controller,
             ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var request = JsonConvert.SerializeObject(
                                changePasswordRequest);
                var content = new StringContent(
                request,
                Encoding.UTF8,
                "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> PasswordRecovery(
             string urlBase,
             string servicePrefix,
             string controller,
             string email)
        {
            try
            {
                var userRequest = new UserRequest { Email = email, };
                var request = JsonConvert.SerializeObject(userRequest);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> ChangePassword(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                var request = JsonConvert.SerializeObject(
                    changePasswordRequest);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                  MainViewModel.GetInstance().Token.AccessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);


                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<TokenResponse> LoginFacebook(string urlBase, string servicePrefix, string controller, FacebookUser profile)
        {
            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                // Error
                if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                // Ok pero no encuentra el usuario
                if (!response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MainViewModel.GetInstance().ExisteUsuario = false;
                }
                else
                {
                    MainViewModel.GetInstance().ExisteUsuario = true;
                }

                var tokenResponse = await GetToken(urlBase, profile.Id, profile.Id, profile.Email);
                return tokenResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred loginfacebook: '{0}'", e);
                return null;
            }
        }

        public async Task<TokenResponse> LoginGoogle(
            string urlBase,
            string servicePrefix,
            string controller,
            GoogleUser profile)
        {

            try
            {
                var request = JsonConvert.SerializeObject(profile);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                // Error
                if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                // Ok pero no encuentra el usuario
                if (!response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MainViewModel.GetInstance().ExisteUsuario = false;
                }
                else
                {
                    MainViewModel.GetInstance().ExisteUsuario = true;
                }

                var tokenResponse = await GetToken(urlBase, profile.Id, profile.Id, profile.Email);
                return tokenResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred logingoogle: '{0}'", e);
                return null;
            }
        }




        public async Task<TokenResponse> GetToken(
            string urlBase,
            string username,
            string password,
            string mail)
        {
            var saveusername = username;

            try
            {


                if (mail != null)
                {
                    username = mail;
                    saveusername = mail;

                }

                // Antes de llamar obtenemos el id en función del usuario:
                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                var responseUser = await client.PostAsync("api/Customers/GetCustomerIdbyMail?mail=" + username, null);
                var resultJSON2 = await responseUser.Content.ReadAsStringAsync();
                var result2 = JsonConvert.DeserializeObject<Customer>(resultJSON2);
                username = result2.CustomerId.ToString();



                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);

                // Error al obtener el token
                if (result.AccessToken == null)
                {
                    return result;
                }

                // OBTENEMOS LOS DATOS DEL CUSTOMER
                // Incluimos datos adicionales:
                //    -- Siguiente objeto por visualizar

                //client.BaseAddress = new Uri(urlBase);
                //var response2 = await client.PostAsync("api/Customers/GetCustomerIdbyMail?mail=" + saveusername,
                //    null);
                //var resultJSON2 = await response2.Content.ReadAsStringAsync();
                result2 = JsonConvert.DeserializeObject<Customer>(resultJSON2);

                // COMO HEMOS OBTENIDO EL CUSTOMER CORRECTAMENTE GUARDAMOS EL LOGIN EN EL "USERACTIONHISTORIES"

                var data = new { IdUser = result2.CustomerId, IdAction = 1 };
                Util.enviarUserActionLog(data, result2.CustomerId);

                // Una vez obtenido el token, buscamos también el idcustomer asociado
                // al mail del usuario
                result.Customer = new Customer();
                result.Customer.CustomerId = result2.CustomerId;
                result.Customer.FirstName = result2.FirstName;
                result.Customer.LastName = result2.LastName;
                result.Customer.Gender = result2.Gender;
                result.Customer.Email = result2.Email;
                result.Customer.DateCreated = result2.DateCreated;
                result.Customer.IdLevel = result2.IdLevel;
                result.Customer.IdFacebook = result2.IdFacebook;
                result.Customer.IdGoogle = result2.IdGoogle;
                result.Customer.CustomerType = result2.CustomerType;
                result.Customer.Country = result2.Country;
                result.Customer.City = result2.City;
                result.Customer.Balance = result2.Balance;
                result.Customer.Avatar = result2.Avatar;
                result.Customer.BirthDay = result2.BirthDay;
                result.Customer.SubscriptionDateFinished = result2.SubscriptionDateFinished;
                result.Customer.SubscriptionFinished = result2.SubscriptionFinished;
                result.Customer.SubscriptionType = result2.SubscriptionType;
                result.Customer.DateIntention = result2.DateIntention;
                //result.IdCurrentPack
                //result.IdCurrentPack

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        // Borrar Bookmakrs delete
        public async Task<Response> DeleteBookmarksPost<UserBookMark>(
            string urlBase,
            string servicePrefix,
            string controller,
            List<UserBookMark> model
            )
        {



            try
            {

                var request = JsonConvert.SerializeObject(model);

                //HttpContent stringContent = new StringContent(request);
                //HttpContent fileStreamContent = new StreamContent(imagen);


                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");


                //var content = new MultipartFormDataContent();
                //content.Add(stringContent, "example", "example");
                //content.Add(fileStreamContent, "stream", "stream");

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<Customer>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record deleted OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }



        // Activar REgalos
        public async Task<Response> ActiveGiftPost<Customer>(
            string urlBase,
            string servicePrefix,
            string controller,
            BankMovement model
            )
        {



            try
            {

                var request = JsonConvert.SerializeObject(model);

                //HttpContent stringContent = new StringContent(request);
                //HttpContent fileStreamContent = new StreamContent(imagen);


                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");


                //var content = new MultipartFormDataContent();
                //content.Add(stringContent, "example", "example");
                //content.Add(fileStreamContent, "stream", "stream");

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<Customer>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }



        public async Task<bool> PayWithStripe(string urlBase, StripePaymentRequest model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync(urlBase, content);

                if (!response.IsSuccessStatusCode)
                    return false;
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var res = JsonConvert.DeserializeObject(result);
                        if (res.Equals(true) || res.Equals(false))
                            return (bool)res;
                        else
                            return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        // Activar REgalos
        public async Task<Response> BuyComprar<Customer>(
            string urlBase,
            string servicePrefix,
            string controller,
            BankMovement model
            )
        {



            try
            {

                var request = JsonConvert.SerializeObject(model);

                //HttpContent stringContent = new StringContent(request);
                //HttpContent fileStreamContent = new StreamContent(imagen);


                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");


                //var content = new MultipartFormDataContent();
                //content.Add(stringContent, "example", "example");
                //content.Add(fileStreamContent, "stream", "stream");

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<Customer>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<int> GetUserName(string urlBase, string username, int customerID)
        {

            try
            {


                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                var response2 = await client.PostAsync("api/Customers/GetUserName?username=" + username
                                                       + "&id=" + customerID,
                    null);
                var resultJSON2 = await response2.Content.ReadAsStringAsync();
                var result2 = JsonConvert.DeserializeObject<Customer>(resultJSON2);

                if (result2 == null) return 0;
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 1;
            }
        }

        public async Task<int> GetExistsUser(string urlBase, string mail)
        {

            try
            {

                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                var response2 = await client.PostAsync("api/Customers/GetCustomerIdbyMail?mail=" + mail,
                    null);
                var resultJSON2 = await response2.Content.ReadAsStringAsync();
                var result2 = JsonConvert.DeserializeObject<Customer>(resultJSON2);

                if (result2 == null) return 0;
                return 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return 1;
            }
        }



        public async Task<DayData> GetResponceforDays(string urlBase, string userId)
        {
            DayData dayData = null;
            try
            {

                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                // http://innermapapi20180719064945.azurewebsites.net/api/Customers/GetProfileQuestions?idUser=<iduser>
                var response2 = await client.PostAsync("api/Customers/GetProfileQuestions?idUser=" + userId, null);
                var resultJSON2 = await response2.Content.ReadAsStringAsync();
                dayData = JsonConvert.DeserializeObject<DayData>(resultJSON2);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
            return dayData;
        }

        public async Task<Response> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType, MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}/{2}", servicePrefix, controller, id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = model,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType = null,
            string accessToken = null)
        {
            try
            {
                var client = new HttpClient();
                if (tokenType != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(tokenType, accessToken);
                }
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> PostList<T>(
                string urlBase,
                string servicePrefix,
                string controller,
                //T model,
                string tokenType = null,
                string accessToken = null
                )
        {
            try
            {

                var request = JsonConvert.SerializeObject("");
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                /*if (tokenType != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(tokenType, accessToken);
                }*/
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(string urlBase, string servicePrefix, string controller, string tokenType = null, string accessToken = null)
        {
            try
            {
                var request = JsonConvert.SerializeObject("");
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                /*if (tokenType != null)
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(tokenType, accessToken);
                }*/
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        // Entre otras cosas por aquí entramos para dar de alta un usuario normal:
        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model
            )
        {



            try
            {

                var request = JsonConvert.SerializeObject(model);

                //HttpContent stringContent = new StringContent(request);
                //HttpContent fileStreamContent = new StreamContent(imagen);


                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");


                //var content = new MultipartFormDataContent();
                //content.Add(stringContent, "example", "example");
                //content.Add(fileStreamContent, "stream", "stream");

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                if (!controller.Equals("/Customers"))                 {                     client.DefaultRequestHeaders.Authorization =                             new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,                                           MainViewModel.GetInstance().Token.AccessToken);                 }
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<Response> UpdateUser<T>(
                string urlBase,
                string servicePrefix,
                string controller,
                T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();

                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format(
                    "{0}{1}",
                    servicePrefix,
                    controller
                    );
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                //var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = null,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(MainViewModel.GetInstance().Token.TokenType,
                                                      MainViewModel.GetInstance().Token.AccessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}