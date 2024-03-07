using Core.Models.Clients;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Clients
{
    public class ClientsController : BaseApiController
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IGenericRepository<ClientModel> _clientRepo;
        public ClientsController(ILogger<ClientsController> logger, IGenericRepository<ClientModel> clientRepo)
        {
            _clientRepo = clientRepo;
            _logger = logger;
        }

        /// <summary>
        /// Get Clients
        /// </summary>
        /// <returns>ClientModel List</returns>
        /// <response code="200">Clients List</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Clients
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IReadOnlyList<ClientModel>> GetClients()
        {
            // _logger.LogInformation("List all clients");
            return await _clientRepo.ListAllAsync();
        }

        /// <summary>
        /// Get Client by Id
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>ClientModel</returns>
        /// <response code="200">Cleint</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Clients/32
        ///
        /// </remarks>
        /// <response code="404">Client not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientModel>> GetClientById(int id)
        {
            var client = await _clientRepo.GetByIdAsync(id);

            if (client is null)
            {
                // _logger.LogInformation($"Client {id} not Found");
                return NotFound("Client not found");
            }

            // _logger.LogInformation($"Client {id} returned");
            return Ok(client);
        }

        /// <summary>
        /// Add Client
        /// </summary>
        /// <param name="clientModel" example="FirstName, LastName, DateOfBirth, Email, Image, Sex, PersonStatus, Phone">ClientModel</param>
        /// <returns>ClientModel</returns>
        /// <response code="200">Cleint</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Clients
        ///     {
        ///        "firstName": "Kristel",
        ///        "lastName": "Lamaj",
        ///        "dateOfBirth": "DD-MM-YYYY",
        ///        "email": "example@example.com",
        ///        "image": "file",
        ///        "phone": "+306912345678",
        ///        "sex": "male",
        ///        "PersonStaus": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add a Client</response>
        [HttpPost]
        public async Task<ActionResult<ClientModel>> AddClient([FromBody] ClientModel clientModel)
        {
            var client = await _clientRepo.AddAsync(clientModel);

            // _logger.LogInformation($"Client added with ID: {client.Id}");
            return Ok(client);
        }

        /// <summary>
        /// Update Client
        /// </summary>
        /// <param name="clientModel" example="FirstName, LastName, DateOfBirth, Email, Image, Sex, PersonStatus, Phone">ClientModel</param>
        /// <returns>ClientModel</returns>
        /// <response code="200">Updated Client</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Clients
        ///     {
        ///        "id" : "32,
        ///        "firstName": "Kristel",
        ///        "lastName": "Lamaj",
        ///        "dateOfBirth": "DD-MM-YYYY",
        ///        "email": "example@example.com",
        ///        "image": "file",
        ///        "phone": "+306912345678",
        ///        "sex": "male",
        ///        "PersonStaus": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="404">Client not found</response>
        [HttpPut]
        public async Task<ActionResult<ClientModel>> UpdateClient([FromBody] ClientModel clientModel)
        {
            var client = await _clientRepo.UpdateAsync(clientModel);

            if (client is null) return NotFound($"Client {clientModel.Id} not found");

            return Ok(client);
        }

        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>ClientModel</returns>
        /// <response code="200">Cleint</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Clients/32
        ///
        /// </remarks>
        /// <response code="404">Client not Found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientModel>> DeleteClient(int id)
        {
            var client = await _clientRepo.DeleteAsync(id);

            if (client is null) return NotFound($"Client {id} not found");
            
            return Ok($"Client {client.Id} successfully deleted");
        }
    }
}