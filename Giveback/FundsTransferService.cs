using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace VisaHackathon2020.Giveback
{
    public static class FundsTransferService
    {
        public static FundsTransferServiceResponse TransferFunds(FundsTransferRequest request)
        {
            var jsonRequest = request.AsJson();
            Console.WriteLine(jsonRequest);
                
            var response = Program.HttpClient.PostAsync(
                Program.ApiKeys.FundsTransferApiUrl, 
                new StringContent(jsonRequest, Encoding.UTF8, "application/json")).Result;
            
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            return JsonConvert.DeserializeObject<FundsTransferServiceResponse>(result);
        }
    }
}