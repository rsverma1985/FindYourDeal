using Companies.data;

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Companies.Repo
{
    //public class UpscApiDataModel
    //{
    //    public UpscApiDataModel()
    //    {
    //    }

    //    public UpscApiDataModel(ShippingRequestModel shippingRequestModel)
    //    {
    //        if (shippingRequestModel == null) throw new ArgumentNullException(nameof(shippingRequestModel));
    //        Source = shippingRequestModel.ContactAddress;
    //        Destination = shippingRequestModel.WarehouseAddress;
    //        Packages = shippingRequestModel.Dimensions;
    //    }

    //    public AddressModel Source { get; set; }
    //    public AddressModel Destination { get; set; }
    //    public Dimension[] Packages { get; set; }
    //}

    public class UspsShippingProvider : RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;

        public UspsShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;
        }

       
        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails("https://extendsclass.com/mock/rest/6c3b6b9e3fc6929ef81602faddafd252/fetchShippingCost", new ApiCredentials("usps___consumer__key", "usps___consumer__secert"));

        //public override string GetApiAcceptedDataFormat(ResponseTypes responseTypes)
        //{
        //    string result = string.Empty;
        //    switch (responseTypes)
        //    {
        //        case ResponseTypes.Json:
        //            result= ParseToText(_requestModel);
        //            break;
        //        case ResponseTypes.Xml:
        //            result= ParseToXml(_requestModel);
        //            break;
        //        case ResponseTypes.Text:
        //            result= ParseToText(_requestModel);
        //            break;
        //        default:
        //            break;
        //    }

        //    // UpscApiDataModel xmlObject = new UpscApiDataModel(_requestModel);
        //    return result;
        //}

        public async Task<ShippingCostResponse> FetchShippingCost(ResponseTypes responseTypes)
        {
            var priceResponse = new ShippingCostResponse { ProviderName = "USPS" };
            // we can write some common serilizers to handle the XML | JSON
            await MakeRequest(xmlResponse =>
            {
                if (xmlResponse != string.Empty)
                {
                    var xdoc = XDocument.Parse(xmlResponse);
                    priceResponse.Amount = Convert.ToDecimal(((XElement)(xdoc.FirstNode)).Value);
                    priceResponse.IsSuccess = true;
                }
            }, responseTypes);
            return priceResponse;
        }
    }
}