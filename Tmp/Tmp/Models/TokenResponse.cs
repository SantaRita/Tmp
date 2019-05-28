using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tmp.Models
{
    public class TokenResponse
    {

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error_description")]
        public String ErrorDescription { get; set; }

        public String Password
        {
            get;
            set;
        }


        public Customer Customer { get; set; }

        public int IdCurrentPack { get; set; }

        public int IdCurrentObject { get; set; }




    }
}
