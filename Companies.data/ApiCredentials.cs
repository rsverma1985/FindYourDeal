using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.data
{
   public class ApiCredentials
    {
        public ApiCredentials(string consumerKey, string consumerSecert)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecert = consumerSecert;
        }
        public string ConsumerKey { get; set; }
        public string ConsumerSecert { get; set; }
    }
}
