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
    }
}