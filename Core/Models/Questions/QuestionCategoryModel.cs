using System.Text.Json.Serialization;

namespace Core.Models.Questions
{
    public class QuestionCategoryModel : BaseModel
    {
        [JsonPropertyName("title")]
        public string? QuestionCategoryTitle { get; set; }
        [JsonPropertyName("questions")]
        public ICollection<QuestionModel>? Questions { get; set; }
    }
}