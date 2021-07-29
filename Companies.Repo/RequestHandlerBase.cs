﻿using Companies.data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Companies.Repo
{
    public abstract class RequestHandlerBase
    {
        private readonly HttpClient _httpClient;

        protected RequestHandlerBase()
        {
            _httpClient = new HttpClient();
        }

        public HttpClient HttpClient => _httpClient;
        public abstract IShippingProviderApiDetails ShippingProviderApiDetails { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="headerName"></param>
        /// <param name="headerValue"></param>
        protected void AddRequestHeader(string headerName, string headerValue)
        {
            this.HttpClient.DefaultRequestHeaders.Add(headerName, headerValue);
        }

        /// <summary>
        ///
        /// </summary>
       
        protected JObject ParseToJsonObject(string json)
        {
            return JObject.Parse(json);
        }
        protected string ParseToText<TObject>(string json)
        {
            return JsonSerializer.Serialize(json);
        }
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ParseToXml<TObject>(TObject obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TObject));
            using var stringWriter = new StringWriter();
            using XmlWriter writer = XmlWriter.Create(stringWriter);
            xmlSerializer.Serialize(writer, obj);
            return stringWriter.ToString();
        }

    //    public abstract string GetApiAcceptedDataFormat(ResponseTypes responseTypes);

        /// <summary>
        ///
        /// </summary>
        /// <param name="onSuccess"></param>
        /// <returns></returns>
        protected async Task MakeRequest(Action<string> onSuccess, ResponseTypes responseTypes)
        {
            try
            {
                string MediaType = responseTypes == ResponseTypes.Json ? "application/json" : "application/xml";
                if (ShippingProviderApiDetails == null)
                    throw new ArgumentNullException(nameof(ShippingProviderApiDetails));
                if (string.IsNullOrEmpty(ShippingProviderApiDetails.ApiBaseUrl))
                {
                    throw new Exception("Missing API Url");
                }
                //if we need to update the request, e.g. attaching specific request headers | credentials.
                var data = string.Empty;// GetApiAcceptedDataFormat(responseTypes);
                //  based on response type we can update the media type
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaType));
                // request data to post
                var stringContent = new StringContent(data, Encoding.UTF8, MediaType);

                var response = await _httpClient.PostAsync(new Uri(ShippingProviderApiDetails.ApiBaseUrl), stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    onSuccess?.Invoke(responseContent);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
