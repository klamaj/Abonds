using Core.Models.Clients;

namespace Core.DTOs
{
    public class ClientDto
    {
        public ClientModel? Client { get; set; }
        public ClientModel? MtachedClient { get; set; }
    }
}