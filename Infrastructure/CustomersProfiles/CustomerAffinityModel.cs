using System.Text.Json.Serialization;

namespace Combi.Swipe.Infrastructure.CustomersProfiles
{
    public class CustomerAffinityModel
    {
        [JsonPropertyName("CUSTOMER_ID")]
        public long CUSTOMER_ID { get; set; }

        [JsonPropertyName("RISK_STYLE")]
        public required string RISK_STYLE { get; set; } = string.Empty;

        [JsonPropertyName("FAVORITE_TEAM")]
        public required string FAVORITE_TEAM { get; set; } = string.Empty;

        [JsonPropertyName("FAVORITE_MARKET_NAME")]
        public required string FAVORITE_MARKET_NAME { get; set; } = string.Empty;
    }
}
