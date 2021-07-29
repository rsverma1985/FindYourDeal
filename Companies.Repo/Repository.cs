using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Companies.data;
namespace Companies.Repo
{
    public class Repository<T> : IRepository<T> where T : ShippingRequestModel
    {
        public ResponseTypes CotentType { get ; set ; }

        public ShippingCostResponse ExtractMinShippingCost(ShippingCostResponse[] shippingCosts)
        {
            if (shippingCosts == null || shippingCosts.Length == 0) return null;
            // this could be find with multiple ways e.g. Min() | OrderBy().FirstOrDefault()
            // USING MIN => shippingCosts.Min(x=> x.Amount);
            var minShippingCost = shippingCosts.OrderBy(x => x.Amount).FirstOrDefault();
            return minShippingCost;
        }

        public ShippingCostResponse FetchBestDeal(T Request)
        {
            try
            {
                if (Request == null) throw new ArgumentNullException(nameof(Request));
                IShippingProvider[] shippingProviders =
                {
                   new FedExShippingProvider(Request),
                   new UpsShippingProvider(Request),
                    new UspsShippingProvider(Request),
                };
                var requests = shippingProviders.Select(x => x.FetchShippingCost(CotentType));
                var allResponses = Task.WhenAll(requests).Result.Where(x => x.IsSuccess).ToArray();
                return ExtractMinShippingCost(allResponses);
            }
            catch (Exception e)
            {
                throw;
            }
        }

       
    }
}
