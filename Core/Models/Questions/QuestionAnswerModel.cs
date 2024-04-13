using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models.Questions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QuestionAnswerModel : BaseModel
    {
        [JsonProperty]
        public string? AnswerValue { get; set; }
        public QuestionModel? Question { get; set; }
        [JsonProperty]
        public int QuestionId { get; set; }
    }
}