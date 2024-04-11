using API.Services.Interfaces;
using Core.DTOs;
using Core.Models.Clients;
using Core.Models.EmailModels;
using Core.Models.Questions;
using Infrastructure.Data;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers.Clients
{
    public class ClientsController : BaseApiController
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IGenericRepository<ClientModel> _clientRepo;
        private readonly DatabaseContext _context;
        private readonly IEmailSender _emailSender;
        public ClientsController(ILogger<ClientsController> logger, IGenericRepository<ClientModel> clientRepo, DatabaseContext context, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _context = context;
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
            var entities = await _context.Clients.Include(x => x.Contract).ToListAsync();
            return entities;
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
            if (clientModel.MatchedUserId.HasValue)
            {
                var user = await _clientRepo.GetByIdAsync(clientModel.MatchedUserId!.Value);
                user.MatchedUserId = clientModel.Id;
                await _clientRepo.UpdateAsync(user);
            }

            var client = await _clientRepo.UpdateAsync(clientModel);
            return (client);
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

        /// <summary>
        /// Get Contract by ClientId
        /// </summary>
        /// <param name="clientId" example="32">int</param>
        /// <returns>ContractModel</returns>
        /// <response code="200">Contract</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Clients/32/Contract
        ///
        /// </remarks>
        /// <response code="404">Contract not Found</response>
        [HttpGet("{clientId}/Contract")]
        public async Task<ActionResult> GetContractByClientId(int clientId)
        {
            var entity = await _context.Contracts.Where(x => x.ClientId == clientId).FirstOrDefaultAsync();
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost("{clientId}/Contract")]
        public async Task<ActionResult<ClientModel>> AddContractToClient(int clientId, [FromBody] ContractModel contract)
        {
            contract.ClientId = clientId;
            var entity = await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();

            var client = await _context.Clients.FindAsync(clientId);
            client!.ContractId = entity.Entity.Id;

            var res = await _clientRepo.UpdateAsync(client);
            return (res);
        }

        [HttpPost("{clientId}/SendMessage")]
        public async Task<ActionResult> SendMessageToClient(int clientId, [FromBody] MessageDto content)
        {
            var client = await _context.Clients.FindAsync(clientId);
            var message = new MessageModel(new string[] { client!.Email! }, "Message from Alpha Bonds", content.Message!);
            await _emailSender.SendEmailAsync(message);

            return Ok($"Message successfully sent");
        }

        [HttpGet("{clinetId}/Interests")]
        public async Task<ActionResult> GetClientInterests(int clinetId)
        {
            // var entity = await _context.Interests.Include(x => x.SubInterests).ThenInclude(x => x.ClientInterests.Where(c => c.ClientId == clinetId)).ToListAsync();
            
            var res = await _context.ClientInterests.Where(x => x.ClientId == clinetId).ToListAsync();

            foreach (var item in res)
            {
                var sub = await _context.SubInterests.FindAsync(item.SubInterestId);

                
            }


            return Ok(res);
        }

        [HttpPost("{clinetId}/Interests")]
        public async Task<ActionResult> AddInterestsToClient(int clinetId, [FromBody] List<ClientInerestModel> clientInerests)
        {
            // foreach (var item in clientInerests)
            // {
            //     item.ClientId = clinetId;
            //     await _context.ClientInterests.AddAsync(item);
            // }
            // await _context.SaveChangesAsync();
            var entities = await _context.Clients.Include(x => x.Contract).Include(s => s.ClientInterests).ThenInclude(s => s.SubInterest).ThenInclude(i => i.Interest).FirstOrDefaultAsync(x => x.Id == clinetId);
            return Ok(entities);
        }

        [HttpPost("SubmitForm")]
        public async Task<ActionResult> SubmitAnswersForm([FromBody] List<ClientAnswerModel> model)
        {
            foreach (var item in model )
            {
                await _context.ClientAnswers.AddAsync(item);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{clientId}/Answers")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetClientAnswers(int clientId)
        {
            var answeredForm = await _context.AnsweredForms.Where(x => x.ClientId == clientId).ToListAsync();

            var categoryDtos = new List<CategoryDto>();

            foreach (var form in answeredForm)
            {
                // Create Category Dto
                var categoryDto = new CategoryDto();
                var entity = await _context.QuestionCategories.FindAsync(form.QuestionCategoryId);
                categoryDto.QuestionCategory = entity!.QuestionCategoryTitle;

                // Get Questions
                var questions = await _context.Questions.Where(q => q.QuestionCategoryId == form.QuestionCategoryId).ToListAsync();

                var questionDtos = new List<QuestionDto>();

                foreach (var question in questions)
                {
                    // Create Question Dto
                    var questionDto = new QuestionDto();
                    questionDto.QuestionTitle = question.QuestionTitle;
                    questionDto.QuestionType = question.QuestionType;

                    var clientAnswers = await _context.ClientAnswers.Where(x => x.QuestionId == question.Id && x.ClientId == clientId).ToListAsync();

                    var answersDtos = new List<AnswerDto>();

                    if (question.QuestionType == "textbox" || question.QuestionType == "linear")
                    {
                        var answerDto = new AnswerDto();
                        answerDto.AnswerValue = clientAnswers[0].AnswerValue;
                        answerDto.Selected = true;
                        answersDtos.Add(answerDto);
                    }
                    else if (question.QuestionType == "radio")
                    {
                        var answers = await _context.QuestionsAnswers.Where(q => q.QuestionId == question.Id).ToListAsync();

                        foreach (var answer in answers)
                        {
                            var answerDto = new AnswerDto();
                            answerDto.AnswerValue = answer.AnswerValue;
                            answerDto.Selected = (answer.Id.ToString() == clientAnswers[0].AnswerValue) ? true : false;
                            answersDtos.Add(answerDto);
                        }
                    }
                    else
                    {
                        var answers = await _context.QuestionsAnswers.Where(q => q.QuestionId == question.Id).ToListAsync();
                        foreach (var answer in answers)
                        {
                            var answerDto = new AnswerDto();
                            answerDto.AnswerValue = answer.AnswerValue;
                            foreach(var clientAns in clientAnswers)
                            {
                                if (answer.Id.ToString() == clientAns.AnswerValue)
                                {
                                    answerDto.Selected = true;
                                    break;
                                }
                            }
                            answersDtos.Add(answerDto);
                        }
                    }

                    questionDto.Answers = answersDtos;

                    questionDtos.Add(questionDto);
                }

                categoryDto.Questions = questionDtos;

                categoryDtos.Add(categoryDto);
            }

            return Ok(categoryDtos);
        }

        [HttpPost("{clientId}/FormModel")]
        public async Task<ActionResult> FormModel(int clientId, [FromBody] AnsweredFormModel model)
        {
            model.ClientId = clientId;
            var entity = await _context.AnsweredForms.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }
    }
}