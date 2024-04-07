using System.Text.Json.Serialization;

namespace Core.Models
{
    public class BaseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}