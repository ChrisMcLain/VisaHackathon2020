using System;
using Newtonsoft.Json;

namespace VisaHackathon2020.Giveback
{
    public class FundsTransferRequest
    {
        [JsonProperty("cardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("cardExpiryDate")]
        public string CardExpiryDate { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("merchantId")]
        public long MerchantId { get; set; }

        [JsonProperty("localTransactionDateTime")]
        public DateTimeOffset LocalTransactionDateTime { get; set; }

        public FundsTransferRequest()
        {
            LocalTransactionDateTime = DateTimeOffset.Now;
        }
        
        public string AsJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    
    public partial class FundsTransferServiceResponse
    {
        [JsonProperty("transactionIdentifier")]
        public long TransactionIdentifier { get; set; }

        [JsonProperty("actionCode")]
        public string ActionCode { get; set; }

        [JsonProperty("approvalCode")]
        public string ApprovalCode { get; set; }

        [JsonProperty("responseCode")]
        public long ResponseCode { get; set; }

        [JsonProperty("transmissionDateTime")]
        public DateTimeOffset TransmissionDateTime { get; set; }
    }
}