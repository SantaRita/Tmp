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

        public String Name { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public string TipoEmpresa { get; set; }



    }
}
