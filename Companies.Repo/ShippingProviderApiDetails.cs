using Companies.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Repo
{
    public class ShippingProviderApiDetails:IShippingProviderApiDetails
    {
        public ShippingProviderApiDetails( string apiBaseUrl, ApiCredentials apiCredentials)
        {
          //  ResponseType = responseType;
            ApiBaseUrl = apiBaseUrl;
            ApiCredentials = apiCredentials;
        }

        //public ResponseTypes ResponseType { get; }
        //public string ApiBaseUrl { get; }
        //public ApiCredentials ApiCredentials { get; }
    }
}
