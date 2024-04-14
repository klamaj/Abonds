using Core.Models.Clients;
using Core.Models.Enums;

namespace Core.DTOs
{
    public class ClientWithInterests
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Sex { get; set; }
        public PersonalStatus Status { get; set; }
        public int? MatchedUserId { get; set; }
        public ClientModel? MatchedUser { get; set; }
        public int? ContractId { get; set; }
        public ContractModel? Contract { get; set; }
        public bool QuestionsSend { get; set; } = false;
        public bool AnsweredQuestions { get; set; } = false;
        public List<SubInterestDto>? SubInterests { get; set; }
    }

    public class SubInterestDto 
    {
        public int Id { get; set; }
        public string? SubInterestName { get; set; }
        public string? SubInterestColor { get; set; }
    }
}