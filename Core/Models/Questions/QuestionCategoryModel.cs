using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Core.Models.Questions
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QuestionCategoryModel : BaseModel
    {
        [JsonProperty]
        public string? QuestionCategoryTitle { get; set; }
        [JsonProperty]
        public ICollection<QuestionModel>? Questions { get; set; }
    }
}