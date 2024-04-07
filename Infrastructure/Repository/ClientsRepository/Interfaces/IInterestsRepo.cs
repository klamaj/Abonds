using Core.Models.Clients;

namespace Infrastructure.Repository.ClientsRepository.Interfaces
{
    public interface IInterestsRepo
    {
        Task<InterestModel> GetInterestByIdWithSubInterests(int id);
    }
}