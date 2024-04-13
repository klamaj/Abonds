using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SubInterestModel : BaseModel
    {
        [JsonProperty]
        public string? SubInterestName { get; set; }
        [JsonProperty]
        public InterestModel? Interest { get; set; }
        [JsonProperty]
        public int InterestId { get; set; }
        public ICollection<ClientInerestModel>? ClientInterests { get; set; }
    }
}