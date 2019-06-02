using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;

namespace Tmp.Services
{
    public class CredentialsService : ICredentialsService
    {
        public string UserName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Username : null;
            }
        }

        public string Password
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Password"] : null;
            }
        }

        public string FirstName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["FirstName"] : null;
            }
        }

        public string LastName
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["LastName"] : null;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                DateTime valor = Convert.ToDateTime(account.Properties["BirthDate"].ToString());
                return valor;
            }
        }

        public string Sex
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Sex"] : null;
            }
        }

        public string Email
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Email"] : null;
            }
        }



        public string Country
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Country"] : null;
            }
        }

        public string Level
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                return (account != null) ? account.Properties["Level"] : null;
            }
        }

        public double Balance
        {
            get
            {
                var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
                double valor = Convert.ToDouble(account.Properties["Balance"].ToString());
                if (account != null) return valor; else return 0;
                //return (account != null) ? valor : null;
            }
        }

        public void SaveCredentials(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account
                {
                    Username = userName
                };

                char[] pwd = password.ToCharArray();
                account.Properties.Add("Password", password);

                AccountStore.Create().Save(account, App.AppName);

                Debug.WriteLine("guardamos credenciales;");
                if (DoCredentialsExist())
                {
                    Debug.WriteLine("SIIIIIIIIIIIIIIIIIIII credenciales;");

                }
                else
                {
                    Debug.WriteLine("NOOOOOOOOOOOOOOOOOOOOOO credenciales;");

                }
            }



        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create().FindAccountsForService(App.AppName).FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, App.AppName);

            }
        }

        public bool DoCredentialsExist()
        {
            try
            {
                return AccountStore.Create().FindAccountsForService(App.AppName).Any() ? true : false;
            }
            catch (Exception ex)
            {
                return false;
                // This part is not invoked anymore once I use the suggested password.
            }
        }

        public void SaveFullCredentials(string userName, string password, string firstname, string lastname,
            DateTime birthday, string sex, string email, double balance, string idlevel)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account
                {
                    Username = userName
                };
                account.Properties.Add("Password", password);
                account.Properties.Add("FirstName", firstname);
                account.Properties.Add("LastName", lastname);
                account.Properties.Add("Birthdate", birthday.ToString());
                account.Properties.Add("Sex", sex);
                account.Properties.Add("Email", email);
                account.Properties.Add("Balance", balance.ToString());
                account.Properties.Add("Idlevel", idlevel);



                AccountStore.Create().Save(account, App.AppName);
            }
        }
    }
}