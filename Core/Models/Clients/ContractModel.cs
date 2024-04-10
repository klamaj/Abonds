namespace Core.Models.Clients
{
    public class ContractModel : BaseModel
    {
        public string? ContratPath  { get; set; }
        public int ClientId { get; set; }
        public ClientModel? Client { get; set; }
    }
}