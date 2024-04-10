namespace Core.DTOs
{
    public class ClientInterestsDto
    {
        public int ClientId { get; set; }
        public ICollection<InterestDto>? Interests { get; set; }
    }

    public class InterestDto {
        public string? InterestName { get; set; }
        public string? Interest { get; set; }
    }
}