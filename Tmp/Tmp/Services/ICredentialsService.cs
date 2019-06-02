using System;
using System.Collections.Generic;
using System.Text;

namespace Tmp.Services
{
    public interface ICredentialsService
    {
        string UserName { get; }

        string Password { get; }

        string FirstName { get; }

        string LastName { get; }

        DateTime BirthDate { get; }

        string Sex { get; }

        string Email { get; }

        double Balance { get; }

        string Country { get; }

        string Level { get; }



        void SaveCredentials(string userName, string password);

        void SaveFullCredentials(string userName, string password, string firstname, string lastname,
            DateTime birthday, string sex, string email, double balance, string idlevel);

        void DeleteCredentials();

        bool DoCredentialsExist();
    }
}
