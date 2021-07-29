using Companies.data;
using Companies.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FindYourDeal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestDealController : ControllerBase
    {
        private readonly IServiceRequest _bestDealService;
        
        
        public BestDealController(IServiceRequest bestdealservice)
        {
            this._bestDealService = bestdealservice;
            
        }

        [HttpGet]
        
        public  IActionResult Get(string ResponseType="Json")
        {
            string providerName = string.Empty;
            decimal amount = 0;

            //here we can pass the shipping information
            _bestDealService.ContentType = ResponseType.ToLower()=="xml" ? ResponseTypes.Xml : ResponseTypes.Json;//   responseTypes;
            var bestDeal =  _bestDealService.FetchBestDeal(new ShippingRequestModel());
            if (bestDeal != null)
            {
                providerName = bestDeal.ProviderName;
                amount = bestDeal.Amount;
            }
            var result = new SampleResult
            {
                Message = "Best shipping deal provided",
                ProviderName = providerName,
                Amount = Convert.ToDouble(amount)
            };
            string finalResult = string.Empty;
            switch(_bestDealService.ContentType)
            {
                case ResponseTypes.Json:
                    finalResult = JsonConvert.SerializeObject(result);
                    break;
                case ResponseTypes.Xml:
                    using (var stringwriter = new System.IO.StringWriter())
                    {
                        var serializer = new XmlSerializer(result.GetType());
                        serializer.Serialize(stringwriter, result);
                        finalResult = stringwriter.ToString();
                    }
                    break;
               
            }
         
           return Ok(finalResult); 
        }
       
    }
}
