using System.Text.Json.Serialization;

namespace Core.Models.Clients
{
    public class InterestModel : BaseModel
    {
        [JsonPropertyName("interestName")]
        public string? InterestName { get; set; }
        [JsonPropertyName("interestColor")]
        public string? InterestColor { get; set; }
        public ICollection<SubInterestModel>? SubInterests { get; set; }
    }
}