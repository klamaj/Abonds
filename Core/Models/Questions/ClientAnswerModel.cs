using Core.Models.Clients;
using Newtonsoft.Json;

namespace Core.Models.Questions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClientAnswerModel : BaseModel
    {
        [JsonProperty]
        public int ClientId { get; set; }
        public ClientModel? Client { get; set; }
        [JsonProperty]
        public int QuestionId { get; set; }
        [JsonProperty]
        public QuestionModel? Question { get; set; }
        [JsonProperty]
        public ICollection<string>? AnswerValue { get; set; }
    }
}