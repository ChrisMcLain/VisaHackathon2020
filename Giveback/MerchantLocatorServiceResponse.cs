namespace VisaHackathon2020
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MerchantLocatorServiceResponseWrapper
    {
        [JsonProperty("merchantLocatorServiceResponse")]
        public MerchantLocatorServiceResponse MerchantLocatorServiceResponse { get; set; }
    }

    public class MerchantLocatorRequest
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        public string AsJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public partial class MerchantLocatorServiceResponse
    {
        [JsonProperty("response")]
        public List<ResponseElement> Response { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("responseMessageId")]
        public string ResponseMessageId { get; set; }

        [JsonProperty("startIndex")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StartIndex { get; set; }

        [JsonProperty("numRecordsMatched")]
        public long NumRecordsMatched { get; set; }

        [JsonProperty("numRecordsReturned")]
        public long NumRecordsReturned { get; set; }

        [JsonProperty("requestMessageId")]
        public string RequestMessageId { get; set; }

        [JsonProperty("messageDateTime")]
        public DateTimeOffset MessageDateTime { get; set; }

        [JsonProperty("endIndex")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long EndIndex { get; set; }
    }

    public partial class ResponseElement
    {
        [JsonProperty("responseValues")]
        public ResponseValues ResponseValues { get; set; }

        [JsonProperty("matchIndicators")]
        public MatchIndicators MatchIndicators { get; set; }

        [JsonProperty("matchScore")]
        public string MatchScore { get; set; }
    }

    public partial class MatchIndicators
    {
        [JsonProperty("merchantCountryCode")]
        public string MerchantCountryCode { get; set; }

        [JsonProperty("merchantCategoryCode")]
        public string MerchantCategoryCode { get; set; }
    }

    public partial class ResponseValues
    {
        [JsonProperty("merchantCountryCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MerchantCountryCode { get; set; }

        [JsonProperty("visaStoreId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long VisaStoreId { get; set; }

        [JsonProperty("merchantUrl")]
        public List<object> MerchantUrl { get; set; }

        [JsonProperty("locationAddressLatitude")]
        public string LocationAddressLatitude { get; set; }

        [JsonProperty("locationAddressLongitude")]
        public string LocationAddressLongitude { get; set; }

        [JsonProperty("visaMerchantName")]
        public string VisaMerchantName { get; set; }

        [JsonProperty("primaryMerchantCategoryCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PrimaryMerchantCategoryCode { get; set; }

        [JsonProperty("visaStoreName")]
        public string VisaStoreName { get; set; }

        [JsonProperty("visaMerchantId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long VisaMerchantId { get; set; }

        [JsonProperty("merchantCategoryCodeDesc")]
        public List<string> MerchantCategoryCodeDesc { get; set; }

        [JsonProperty("visaEnterpriseName")]
        public string VisaEnterpriseName { get; set; }

        [JsonProperty("merchantStreetAddress")]
        public string MerchantStreetAddress { get; set; }

        [JsonProperty("merchantPostalCode")]
        public string MerchantPostalCode { get; set; }

        [JsonProperty("merchantState")]
        public string MerchantState { get; set; }

        [JsonProperty("merchantCity")]
        public string MerchantCity { get; set; }

        [JsonProperty("paymentAcceptanceMethod")]
        public List<string> PaymentAcceptanceMethod { get; set; }

        [JsonProperty("terminalType")]
        public List<TerminalType> TerminalType { get; set; }

        [JsonProperty("firstTranDateRange")]
        public string FirstTranDateRange { get; set; }

        [JsonProperty("lastTranDateRange")]
        public string LastTranDateRange { get; set; }

        [JsonProperty("merchantCategoryCode")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> MerchantCategoryCode { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }
    }

    public partial class Status
    {
        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }
    }

    public enum TerminalType { Chip, Paywave, Swipe };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TerminalTypeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(List<long>);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>)untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
    }

    internal class TerminalTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TerminalType) || t == typeof(TerminalType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CHIP":
                    return TerminalType.Chip;
                case "PAYWAVE":
                    return TerminalType.Paywave;
                case "SWIPE":
                    return TerminalType.Swipe;
            }
            throw new Exception("Cannot unmarshal type TerminalType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TerminalType)untypedValue;
            switch (value)
            {
                case TerminalType.Chip:
                    serializer.Serialize(writer, "CHIP");
                    return;
                case TerminalType.Paywave:
                    serializer.Serialize(writer, "PAYWAVE");
                    return;
                case TerminalType.Swipe:
                    serializer.Serialize(writer, "SWIPE");
                    return;
            }
            throw new Exception("Cannot marshal type TerminalType");
        }

        public static readonly TerminalTypeConverter Singleton = new TerminalTypeConverter();
    }
}
