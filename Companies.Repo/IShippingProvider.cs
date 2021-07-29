using Companies.data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Repo
{
    interface IShippingProvider
    {
        Task<ShippingCostResponse> FetchShippingCost(ResponseTypes responseTypes);
    }
}
