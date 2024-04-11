using Core.Models.Enums;

namespace Core.Models.Clients
{
    public class ClientModel : BaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Sex { get; set; }
        public PersonalStatus Status { get; set; } = PersonalStatus.Free;
        public int? MatchedUserId { get; set; }
        public ClientModel? MatchedUser { get; set; }
        public int? ContractId { get; set; }
        public ContractModel? Contract { get; set; }
        public bool QuestionsSend { get; set; } = false;
        public bool AnsweredQuestions { get; set; } = false;
        public ICollection<ClientInerestModel>? ClientInterests { get; set; }
    }
}