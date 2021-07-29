using Companies.data;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Companies.Repo
{
    public class UpsShippingProvider : RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;

        public UpsShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;
        }

        //public override string GetApiAcceptedDataFormat(ResponseTypes responseTypes)
        //{
        //    string result = string.Empty;
        //    switch (responseTypes)
        //    {
        //        case ResponseTypes.Json:
        //            result = ParseToText(_requestModel);
        //            break;
        //        case ResponseTypes.Xml:
        //            result = ParseToXml(_requestModel);
        //            break;
        //        case ResponseTypes.Text:
        //            result = ParseToText(_requestModel);
        //            break;
        //        default:
        //            break;
        //    }

        //    // UpscApiDataModel xmlObject = new UpscApiDataModel(_requestModel);
        //    return result;
        //}
        public override IShippingProviderApiDetails ShippingProviderApiDetails => new ShippingProviderApiDetails( "https://60c629c319aa1e001769eec7.mockapi.io/api/upsPrice", new ApiCredentials("ups___consumer__key", "ups___consumer__secert"));

        public async Task<ShippingCostResponse> FetchShippingCost(ResponseTypes responseTypes)
        {
            var priceResponse = new ShippingCostResponse { ProviderName = "UPS" };
            await MakeRequest(response =>
            {
                if (response != string.Empty)
                {
                    dynamic parsedJsonObject = ParseToJsonObject(response);
                    priceResponse.IsSuccess = true;
                    priceResponse.Amount = Convert.ToDecimal(parsedJsonObject["total"]) ?? 0.0;
                }
            }, responseTypes);
            return priceResponse;
        }
    }
}