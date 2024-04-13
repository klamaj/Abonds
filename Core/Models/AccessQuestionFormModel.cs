namespace Core.Models
{
    public class AccessQuestionFormModel : BaseModel
    {
        public Guid AccessGuid { get; set; }
        public int? ClientId { get; set; }
        public int? QuestionCategoryId { get; set; }
    }
}