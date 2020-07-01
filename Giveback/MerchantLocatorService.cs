using System;
using System.Linq;
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
                        foreach (var merchant in deserialized.Response
                            .Where(merchant => baseResponse.Response
                                .All(n => n.ResponseValues.VisaStoreId 
                                          != merchant.ResponseValues.VisaStoreId)))
                        {
                            baseResponse.Response.Add(merchant);
                            baseResponse.Header.NumRecordsReturned += 1;
                        }
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