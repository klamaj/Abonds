using Newtonsoft.Json;

namespace Core.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BaseModel
    {
        [JsonProperty]
        public int Id { get; set; }
    }
}