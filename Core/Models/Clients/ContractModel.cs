using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ContractModel : BaseModel
    {
        [JsonProperty]
        public string? ContratPath  { get; set; }
        [JsonProperty]
        public int ClientId { get; set; }
        public ClientModel? Client { get; set; }
    }
}