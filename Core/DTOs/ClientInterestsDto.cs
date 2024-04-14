namespace Core.DTOs
{
    public class ClientInterestsDto
    {
        public int ClientId { get; set; }
        public List<SubInterestDto>? SubInterests { get; set; }
    }
}