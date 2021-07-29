using Companies.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.Repo
{
    public class IShippingProviderApiDetails
    {
     
       public string ApiBaseUrl { get; set; }
      public  ApiCredentials ApiCredentials { get; set; }
    }
}
