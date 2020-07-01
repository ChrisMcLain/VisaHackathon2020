namespace VisaHackathon2020.Giveback
{
    public class SearchModel
    {
        public bool ExpandedSearch { get; set; }
        public bool LocalSearch => !ExpandedSearch;
        
        public MerchantLocatorServiceResponse Response { get; set; }
        public bool HasResults => Response?.Response != null;
        public bool HasSearched => Response != null;
        
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        
        public int[] Category { get; set; }
    }
}