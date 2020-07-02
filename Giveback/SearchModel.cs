namespace VisaHackathon2020.Giveback
{
    public class SearchModel
    {
        public bool ExpandedSearch => SearchQuery != null && !SearchQuery.Equals(string.Empty);
        public string SearchQuery { get; set; }
        
        public MerchantLocatorServiceResponse Response { get; set; }
        public bool HasResults => Response?.Response != null;
        public bool HasSearched => Response != null;
        
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        
        public int[] Category { get; set; }
    }
}