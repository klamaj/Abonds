using System.Text.Json.Serialization;

namespace Core.Models.Clients
{
    public class InterestModel : BaseModel
    {
        [JsonPropertyName("name")]
        public string? InterestName { get; set; }
        [JsonPropertyName("color")]
        public string? InterestColor { get; set; }
        public ICollection<SubInterestModel>? SubInterests { get; set; }
    }
}