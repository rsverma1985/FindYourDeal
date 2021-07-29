using System.Collections.Generic;

using Companies.data;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Companies.Repo
{
    public class FedExShippingProvider:RequestHandlerBase, IShippingProvider
    {
        private readonly ShippingRequestModel _requestModel;

        public FedExShippingProvider(ShippingRequestModel requestModel)
        {
            _requestModel = requestModel;
        }

        public override IShippingProviderApiDetails ShippingProviderApiDetails =>
            new ShippingProviderApiDetails("https://60c629c319aa1e001769eec7.mockapi.io/api/fexExPrice", new ApiCredentials("consumer__key", "consumer__secert"));

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

        public async Task<ShippingCostResponse> FetchShippingCost(ResponseTypes responseTypes)
        {
            var priceResponse = new ShippingCostResponse { ProviderName = "FedEx" };
            //=====================================================================================
            //  We can update the request, to add API credentials here or any required information

            //  AddRequestHeader("consumer__key", ShippingProviderApiDetails.ApiCredentials.ConsumerKey);
            //=====================================================================================

            await MakeRequest(response =>
            {
                //since each api will send different response, we need to handle that parsing to shipping provider level
                if (response != string.Empty)
                {
                    dynamic parsedJsonObject = ParseToJsonObject(response);
                    priceResponse.IsSuccess = true;
                    priceResponse.Amount = Convert.ToDecimal(parsedJsonObject["amount"]) ?? 0.0;
                }
            }, responseTypes);

            return priceResponse;
        }
    }
}