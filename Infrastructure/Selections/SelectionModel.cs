using System.Text.Json.Serialization;

namespace Combi.Swipe.Infrastructure.Selections
{
    public class SelectionModel
    {
        [JsonPropertyName("SELECTION_ID")]
        public string SelectionId { get; set; }

        [JsonPropertyName("SELECTION_DESCRIPTION")]
        public string Description { get; set; }

        [JsonPropertyName("MARKET_ID")]
        public long MarketId { get; set; }

        [JsonPropertyName("MARKET_NAME")]
        public string MarketName { get; set; }

        [JsonPropertyName("MATCH_ID")]
        public long MatchId { get; set; }

        [JsonPropertyName("ODDS")]
        public decimal Odds { get; set; }

        [JsonPropertyName("SPORT_ID")]
        public string SportId { get; set; }
    }
}
