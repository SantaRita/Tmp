using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tmp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public int CustomerType { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int IdLevel { get; set; }

        public string Gender { get; set; }

        public System.DateTime? BirthDay { get; set; }

        public string IdFacebook { get; set; }

        public string IdGoogle { get; set; }

        public decimal? Balance { get; set; }

        public String Password { get; set; }

        public String Avatar { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public int? SubscriptionType { get; set; } = 0;

        //public DateTime? SubscriptionDateFinished { get; set; } = DateTime.Now.AddDays(7);
        private DateTime subscriptionDateFinished;
        public DateTime? SubscriptionDateFinished
        {
            get
            {
                return subscriptionDateFinished;
            }
            set
            {
                var dt = value;
                if (dt != null)
                    subscriptionDateFinished = (System.DateTime)dt;
            }
        }

        public Boolean? UpdateAvatar { get; set; } = false;


        public Boolean? SubscriptionFinished { get; set; }

        public DateTime? DateIntention { get; set; }


    }
}
