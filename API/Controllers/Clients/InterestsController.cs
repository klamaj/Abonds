using Core.Models.Clients;
using Infrastructure.Data;
using Infrastructure.Repository.ClientsRepository.Interfaces;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers.Clients
{
    public class InterestsController : BaseApiController
    {
        private readonly IGenericRepository<InterestModel> _interestRepo;
        private readonly IGenericRepository<SubInterestModel> _subInterestRepo;
        private readonly IInterestsRepo _repo;
        private readonly DatabaseContext _context;
        public InterestsController(IGenericRepository<InterestModel> interestRepo, IGenericRepository<SubInterestModel> subInterestRepo, IInterestsRepo repo, DatabaseContext context)
        {
            _context = context;
            _repo = repo;
            _subInterestRepo = subInterestRepo;
            _interestRepo = interestRepo;
        }

        /// <summary>
        /// Get Interests
        /// </summary>
        /// <returns>InterestModel List</returns>
        /// <response code="200">Interests List</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Interests
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IReadOnlyList<InterestModel>> GetInterests()
        {
            // var spec = new InterestsWithSubInterestsSpecification();
            var entities = await _context.Interests.Include(x => x.SubInterests).ToListAsync();
            return entities;
        }

        /// <summary>
        /// Get Interest by Id
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>InterestModel</returns>
        /// <response code="200">Interest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Interests/32
        ///
        /// </remarks>
        /// <response code="404">Interest not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<InterestModel>> GetInterestById(int id)
        {
            var interest = await _interestRepo.GetByIdAsync(id);

            if (interest is null)
            {
                // _logger.LogInformation($"Client {id} not Found");
                return NotFound($"Interest {id} not found");
            }
            var entity = await _context.SubInterests.Where(x => x.InterestId == id).ToListAsync();
            interest.SubInterests = entity;

            // _logger.LogInformation($"Client {id} returned");
            return Ok(interest);
        }

        /// <summary>
        /// Add Interest
        /// </summary>
        /// <param name="interestModel" example="name, color">InterestModel</param>
        /// <returns>InterestModel</returns>
        /// <response code="200">Interest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Interests
        ///     {
        ///        "name": "Sports",
        ///        "color": "#FFFFFF"
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add an Interest</response>
        [HttpPost]
        public async Task<ActionResult<InterestModel>> AddInterest([FromBody] InterestModel interestModel)
        {
            var interest = await _interestRepo.AddAsync(interestModel);

            // _logger.LogInformation($"Interest added with ID: {interest.Id}");
            return Ok(interest);
        }

        /// <summary>
        /// Update Interest
        /// </summary>
        /// <param name="interestModel" example="name, color">InterestModel</param>
        /// <returns>InteretModel</returns>
        /// <response code="200">Updated Interest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///    PUT /Interests
        ///     {
        ///        "name": "Sports",
        ///        "color": "#FFFFFF"
        ///     }
        ///
        /// </remarks>
        /// <response code="404">Interest not found</response>
        [HttpPut]
        public async Task<ActionResult<InterestModel>> UpdateInterest([FromBody] InterestModel interestModel)
        {
            if (interestModel.SubInterests.Count > 0)
            {
                var inter = new InterestModel();
                inter.InterestName = interestModel.InterestName;
                inter.InterestColor = interestModel.InterestColor;
                inter.Id = interestModel.Id;

                var sub = new SubInterestModel();
                foreach (var item in interestModel.SubInterests)
                {
                    sub.InterestId = interestModel.Id;
                    sub.SubInterestName = item.SubInterestName;
                    var exists = _context.SubInterests.Where(x => x.InterestId == interestModel.Id && x.SubInterestName == item.SubInterestName).ToList();
                    if (exists.Count == 0) await _subInterestRepo.AddAsync(sub);
                }

                var interestWithSub = await _interestRepo.GetByIdAsync(interestModel.Id);
                var entity = await _context.SubInterests.Where(x => x.InterestId == interestModel.Id).ToListAsync();
                interestWithSub.SubInterests = entity;

                return Ok(interestWithSub);

            }
            var interest = await _interestRepo.UpdateAsync(interestModel);

            if (interest is null) return NotFound($"Client {interestModel.Id} not found");

            return Ok(interest);
        }

        /// <summary>
        /// Delete Interest
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>InterestModel</returns>
        /// <response code="200">Interest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Interests/32
        ///
        /// </remarks>
        /// <response code="404">Interest not Found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<InterestModel>> DeleteInterest(int id)
        {
            var interest = await _interestRepo.DeleteAsync(id);

            if (interest is null) return NotFound($"Interest {id} not found");

            return Ok($"Client {interest.Id} successfully deleted");
        }

        /// <summary>
        /// Get SubInterests
        /// </summary>
        /// <param name="interestId" example="32">int</param>
        /// <returns>SubInterests</returns>
        /// <response code="200">SubInterests List</response>
        /// <response code="204">No SubInterests for InterestId</response>
        /// respones 
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Interests/32/SubInterests
        ///
        /// </remarks>
        [HttpGet("{interestId}/SubInterests")]
        public async Task<ActionResult<IReadOnlyList<SubInterestModel>>> GetSubInterests(int interestId)
        {
            var entity = await _context.SubInterests.Where(x => x.InterestId == interestId).ToListAsync();

            if (entity is null) return NoContent();

            return Ok(entity);
        }

        /// <summary>
        /// Add SubInterest
        /// </summary>
        /// <param name="subInterest" example="name">SubInterestModel</param>
        /// <param name="interestId" example="32">int</param>
        /// <returns>SubInterestModel</returns>
        /// <response code="200">SubInterest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Interests/32/SubInterests
        ///     {
        ///        "name": "Football",
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add an Interest</response>
        [HttpPost("{interestId}/SubInterests")]
        public async Task<ActionResult<InterestModel>> AddInterest([FromBody] SubInterestModel subInterest, int interestId)
        {
            subInterest.InterestId = interestId;
            var subInterestRes = await _subInterestRepo.AddAsync(subInterest);

            var interest = await _interestRepo.GetByIdAsync(interestId);

            var entity = await _context.SubInterests.Where(x => x.InterestId == interestId).ToListAsync();
            interest.SubInterests = entity;
            // _logger.LogInformation($"Interest added with ID: {interest.Id}");
            return Ok(interest);
        }

        /// <summary>
        /// Delete SubInterest
        /// </summary>
        /// <param name="subInterestId" example="32">int</param>
        /// <param name="interestId" example="32">int</param>
        /// <returns>SubInterestModel</returns>
        /// <response code="200">SubInterest</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Interests/32/SubInterests/32
        ///
        /// </remarks>
        /// <response code="404">SubInterest not Found</response>
        [HttpDelete("{interestId}/SubInterests/{subInterestId}")]
        public async Task<ActionResult<SubInterestModel>> DeleteSubInterest(Int16 interestId, int subInterestId)
        {
            var subInterest = await _subInterestRepo.DeleteAsync(subInterestId);

            if (subInterest is null) return NotFound($"SubInterest {subInterestId} not found");

            return Ok($"SubInterest {subInterest.Id} successfully deleted");
        }
    }
}