using Core.Models.Enums;
using Newtonsoft.Json;

namespace Core.Models.Clients
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ClientModel : BaseModel
    {
        [JsonProperty]
        public string? FirstName { get; set; }
        [JsonProperty]
        public string? LastName { get; set; }
        [JsonProperty]
        public string? DateOfBirth { get; set; }
        [JsonProperty]
        public string? Email { get; set; }
        [JsonProperty]
        public string? ProfileImagePath { get; set; }
        [JsonProperty]
        public string? Sex { get; set; }
        [JsonProperty]
        public PersonalStatus Status { get; set; } = PersonalStatus.Free;
        [JsonProperty]
        public int? MatchedUserId { get; set; }
        [JsonProperty]
        public ClientModel? MatchedUser { get; set; }
        [JsonProperty]
        public int? ContractId { get; set; }
        [JsonProperty]
        public ContractModel? Contract { get; set; }
        [JsonProperty]
        public bool QuestionsSend { get; set; } = false;
        [JsonProperty]
        public bool AnsweredQuestions { get; set; } = false;
        [JsonProperty]
        public ICollection<ClientInerestModel>? ClientInterests { get; set; }
        public ICollection<ImageModel>? Images { get; set; }
    }
}