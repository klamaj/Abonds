using Core.Models.Clients;
using Infrastructure.Data;
using Infrastructure.Repository.ClientsRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.ClientsRepository.Services
{
    public class InterestsRepo : IInterestsRepo
    {
        private readonly DatabaseContext _context;
        public InterestsRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<InterestModel> GetInterestByIdWithSubInterests(int id)
        {
            var entity = await _context.Interests.FirstOrDefaultAsync(x => x.Id == id);

            return entity!;
        }
    }
}