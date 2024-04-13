using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class InterestModel : BaseModel
    {
        [JsonProperty]
        public string? InterestName { get; set; }
        [JsonProperty]
        public string? InterestColor { get; set; }
        [JsonProperty]
        public ICollection<SubInterestModel>? SubInterests { get; set; }
    }
}