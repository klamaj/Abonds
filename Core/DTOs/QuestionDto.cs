namespace Core.DTOs
{
    public class CategoryDto
    {
        public string? QuestionCategory { get; set; }
        public List<QuestionDto>? Questions { get; set; }
    }

    public class QuestionDto
    {
        public string? QuestionTitle { get; set; }
        public string? QuestionType { get; set; }
        public List<AnswerDto>? Answers { get; set; }
    }

    public class AnswerDto
    {
        public string? AnswerValue { get; set; }
        public bool Selected { get; set; } = false;
    }
}