namespace Core.DTOs
{
    public class ClientInterestsDto
    {
        public int ClientId { get; set; }
        public List<SubInterestDto>? SubInterests { get; set; }
    }

    public class SubInterestDto {
        public string? SubInterest { get; set; }
        public string? SubInterestColor { get; set; }
    }
}