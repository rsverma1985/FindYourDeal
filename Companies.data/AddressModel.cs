using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Companies.data
{
   public class AddressModel
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(10), EmailAddress]
        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
