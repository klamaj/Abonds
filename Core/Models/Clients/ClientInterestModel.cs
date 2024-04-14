using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClientInterestModel : BaseModel
    {
        [JsonProperty]
        public int? ClientId { get; set; }
        public ClientModel? ClientModel { get; set; }
        [JsonProperty]
        public int? SubInterestId { get; set; }
        [JsonProperty]
        public SubInterestModel? SubInterest { get; set; }
    }
}