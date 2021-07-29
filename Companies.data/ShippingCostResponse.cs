using System;
using System.Collections.Generic;
using System.Text;

namespace Companies.data
{
    public class ShippingCostResponse
    {
        public bool IsSuccess { get; set; }
        public string ProviderName { get; set; }
        public decimal Amount { get; set; }
    }
}
