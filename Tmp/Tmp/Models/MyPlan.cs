using System;
using System.Collections.Generic;
using System.Text;

namespace Tmp.Models
{
    public class MyPlan
    {

        public int IdMyPlan { get; set; }

        public int CustomerId { get; set; }

        public int Type { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public string KeyPlan { get; set; }

        public string Formulario { get; set; }
    }
}
