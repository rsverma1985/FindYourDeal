using Companies.data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Repo
{
    public interface IRepository<T> where T : ShippingRequestModel
    {
        public ResponseTypes CotentType { get; set; }
        public ShippingCostResponse FetchBestDeal(T Request);
        public  ShippingCostResponse ExtractMinShippingCost(ShippingCostResponse[] shippingCosts);
    }
}
