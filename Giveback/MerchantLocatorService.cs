using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace VisaHackathon2020.Giveback
{
    public static class MerchantLocatorService
    {
        public static MerchantLocatorServiceResponse GetMerchantsNear(MerchantLocatorRequest request)
        {
            var jsonRequest = request.AsJson();
            Console.WriteLine(jsonRequest);
            
            var response = Program.HttpClient.PostAsync(
                Program.ApiKeys.MerchantSearchApiUrl, 
                new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            return JsonConvert.DeserializeObject<MerchantLocatorServiceResponseWrapper>(result)
                .MerchantLocatorServiceResponse;
        }
    }
}