using Core.Models.Clients;
using Newtonsoft.Json;

namespace Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ImageModel : BaseModel
    {
        [JsonProperty]
        public string? ImagePath { get; set; }
        [JsonProperty]
        public int ClientId { get; set; }
        public ClientModel? Client { get; set; }
    }
}