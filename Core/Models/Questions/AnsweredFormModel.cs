namespace Core.Models.Questions
{
    public class AnsweredFormModel : BaseModel
    {
        public int? ClientId { get; set; }
        public int? QuestionCategoryId { get; set; }
        public ICollection<ClientAnswerModel>? Answers { get; set; }
    }
}