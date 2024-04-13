using Newtonsoft.Json;

namespace Core.Models.Questions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AnsweredFormModel : BaseModel
    {
        [JsonProperty]
        public int? ClientId { get; set; }
        [JsonProperty]
        public int? QuestionCategoryId { get; set; }
        [JsonProperty]
        public ICollection<ClientAnswerModel>? Answers { get; set; }
    }
}