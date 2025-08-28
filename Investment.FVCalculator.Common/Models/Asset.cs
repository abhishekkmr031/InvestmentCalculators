using System.Text.Json.Serialization;

namespace Investment.FVCalculator.Common.Models
{
    public class Asset
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("startamt")]
        public double StartAmount { get; set; }

        [JsonPropertyName("sip")]
        public double SIPAmount { get; set; } = 0;

        [JsonPropertyName("int")]
        public double Interest { get; set; } = 5;

        [JsonPropertyName("investyears")]
        public int InvestYears { get; set; } = 1;

        [JsonPropertyName("stayinvestedyears")]
        public int HoldYears { get; set; } = 1;
    }
}
