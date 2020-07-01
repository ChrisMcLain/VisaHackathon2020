using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace VisaHackathon2020.Giveback
{
    public static class MerchantLocatorService
    {
        public static MerchantLocatorServiceResponse GetMerchantsNear(MerchantLocatorRequest request, int pages)
        {
            MerchantLocatorServiceResponse baseResponse = null;

            for (var i = 0; i < pages; i++)
            {
                request.StartIndex = i * 5;
                var jsonRequest = request.AsJson();
                Console.WriteLine(jsonRequest);
                
                var response = Program.HttpClient.PostAsync(
                    Program.ApiKeys.MerchantSearchApiUrl, 
                    new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            
                var result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<MerchantLocatorServiceResponseWrapper>(result)
                        .MerchantLocatorServiceResponse;

                    if (baseResponse == null)
                    {
                        baseResponse = deserialized;
                    }
                    else
                    {
                        baseResponse.Response.AddRange(deserialized.Response);
                        baseResponse.Header.NumRecordsReturned += deserialized.Response.Count;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error retrieving data - " + e);
                    break;
                }
            }

            return baseResponse;
        }
    }
}