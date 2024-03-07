using Core.Models.Clients;
using Infrastructure.Repository.Interfaces;

namespace API.Controllers.Clients
{
    public class SubInterestsController : BaseApiController
    {
        private readonly IGenericRepository<SubInterestModel> _subInterestRepo;
        public SubInterestsController(IGenericRepository<SubInterestModel> subInterestRepo)
        {
            _subInterestRepo = subInterestRepo;
        }

        
    }
}