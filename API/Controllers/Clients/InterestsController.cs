using Core.Models.Clients;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Clients
{
    public class InterestsController : BaseApiController
    {
        private readonly IGenericRepository<InterestModel> _interestRepo;
        public InterestsController(IGenericRepository<InterestModel> interestRepo)
        {
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
            return await _interestRepo.ListAllAsync();
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

            if (interest is null) return NotFound($"Client {id} not found");

            return Ok($"Client {interest.Id} successfully deleted");
        }
    }
}