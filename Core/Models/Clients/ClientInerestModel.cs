
using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClientInerestModel : BaseModel
    {
        [JsonProperty]
        public int ClientId { get; set; }
        public ClientModel? Client { get; set; }
        [JsonProperty]
        public int SubInterestId { get; set; }
        [JsonProperty]
        public SubInterestModel? SubInterest { get; set; }
    }
}