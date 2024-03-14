using System.Text.Json.Serialization;

namespace Core.Models.Questions
{
    public class QuestionModel : BaseModel
    {
        [JsonPropertyName("title")]
        public string? QuestionTitle { get; set; }
        [JsonPropertyName("type")]
        public string QuestionType { get; set; } = "textbox";
        [JsonPropertyName("answers")]
        public ICollection<QuestionAnswerModel>? QuestionAnswers { get; set; }
        [JsonPropertyName("required")]
        public bool Required { get; set; } = false;
        public QuestionCategoryModel? QuestionCategory { get; set; }
        [JsonPropertyName("questionId")]
        public int QuestionCategoryId { get; set; }
    }
}