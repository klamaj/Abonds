using System.Text.Json.Serialization;

namespace Core.Models.Questions
{
    public class QuestionAnswerModel : BaseModel
    {
        [JsonPropertyName("value")]
        public string? AnswerValue { get; set; }
        public QuestionModel? Question { get; set; }
        [JsonPropertyName("questionId")]
        public int QuestionId { get; set; }
    }
}