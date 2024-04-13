using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models.Questions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QuestionModel : BaseModel
    {
        [JsonProperty]
        public string? QuestionTitle { get; set; }
        [JsonProperty]
        public string QuestionType { get; set; } = "textbox";
        [JsonProperty]
        public ICollection<QuestionAnswerModel>? QuestionAnswers { get; set; }
        [JsonProperty]
        public bool Required { get; set; } = false;
        public QuestionCategoryModel? QuestionCategory { get; set; }
        [JsonProperty]
        public int QuestionCategoryId { get; set; }
    }
}