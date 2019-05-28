using System;
using System.Collections.Generic;
using System.Text;

namespace Tmp.Models
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public object IdCustomer { get; set; }

    }
}
}
