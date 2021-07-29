using System;
using System.ComponentModel.DataAnnotations;

namespace Companies.data
{
    public class ShippingRequestModel
    {
        [Required]
        public AddressModel ContactAddress { get; set; }

        [Required]
        public AddressModel WarehouseAddress { get; set; }

        public Dimension[] Dimensions { get; set; }
        //public ResponseTypes ResponseType { get; }
        //public string ApiBaseUrl { get; }
        //public ApiCredentials ApiCredentials { get; }

    }
}
