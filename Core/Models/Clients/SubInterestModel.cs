using System.Text.Json.Serialization;

namespace Core.Models.Clients
{
    public class SubInterestModel : BaseModel
    {
        [JsonPropertyName("name")]
        public string? SubInterestName { get; set; }
        public InterestModel? Interest { get; set; }
        public int IntrestId { get; set; }
    }
}