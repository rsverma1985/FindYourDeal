using System;
using System.Collections.Generic;
using System.Text;
using Companies.data;
namespace Companies.Service
{
   public interface IServiceRequest
    {
        public ResponseTypes ContentType { get; set; }
        public ShippingCostResponse FetchBestDeal(ShippingRequestModel Request);
 
    }
}
