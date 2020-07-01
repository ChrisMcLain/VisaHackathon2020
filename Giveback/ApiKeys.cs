using System.IO;
using Newtonsoft.Json;

namespace VisaHackathon2020.Giveback
{
    public class ApiKeys
    {
        public string GoogleMaps { get; set; }
        public string MerchantSearchApiUrl { get; set; }
        public string FundsTransferApiUrl { get; set; }
        public string FinancialStruggleApiUrl { get; set; }

        public static ApiKeys FromFile(string path)
        {
            return JsonConvert.DeserializeObject<ApiKeys>(File.ReadAllText(path));
        }
    }
}