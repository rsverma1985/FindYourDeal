using Companies.data;
using Companies.Repo;
using System;

namespace Companies.Service
{
    public class ServiceRequest : IServiceRequest
    {
        private IRepository<ShippingRequestModel> _bestDealfinder;
        public ResponseTypes ContentType { get; set; }
        public ServiceRequest(IRepository<ShippingRequestModel> _bestDealfinder)
        {
            this._bestDealfinder = _bestDealfinder;
        }
        public ShippingCostResponse FetchBestDeal(ShippingRequestModel Request)
        {
            _bestDealfinder.CotentType = this.ContentType;
           ShippingCostResponse _shippingcostresponse= _bestDealfinder.FetchBestDeal(Request);
            return _shippingcostresponse;
        }
    }
}
