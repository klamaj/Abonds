using API.Services.Interfaces;
using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.Models.Clients;
using Core.Models.EmailModels;
using Core.Models.Enums;
using Core.Models.Questions;
using Infrastructure.Data;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SQLitePCL;

namespace API.Controllers.Clients
{
    public class ClientsController : BaseApiController
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IGenericRepository<ClientModel> _clientRepo;
        private readonly DatabaseContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        public ClientsController(ILogger<ClientsController> logger, IGenericRepository<ClientModel> clientRepo, DatabaseContext context, IEmailSender emailSender, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<IReadOnlyList<ClientModel>> GetClients([FromQuery] string? search, string? gender, string? ageFrom, string? ageTo, string? status)
        {
            var entities = new List<ClientModel>();
            var year = DateTime.Now.Year;
            if (status is not null)
            {
                var stat = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), status);
                entities = await _context.Clients.Where(c => c.Status == stat).ToListAsync();
            }
            if (gender is not null)
            {
                if (entities.Count > 0)
                {
                    entities = entities.Where(c => c.Sex == gender).ToList();
                }
                else
                {
                    entities = await _context.Clients.Where(c => c.Sex == gender).ToListAsync();
                }
            }
            if (ageFrom is not null)
            {
                if (entities.Count > 0)
                {
                    entities.Where(c => c.DateOfBirth.Year <= (year - Int32.Parse(ageFrom))).ToList();
                }
                else
                {
                    var calcYear = (year - Int32.Parse(ageFrom));
                    entities = await _context.Clients.Where(c => c.DateOfBirth.Year <= calcYear).ToListAsync();
                }
            }
            if (ageTo is not null)
            {
                if (entities.Count > 0)
                {
                    entities.Where(c => c.DateOfBirth.Year >= (year - Int32.Parse(ageTo))).ToList();
                }
                else
                {
                    entities = await _context.Clients.Where(c => c.DateOfBirth.Year <= (year - Int32.Parse(ageTo))).ToListAsync();
                }
            }
            if (search is not null)
            {
                if (entities.Count > 0)
                {
                    entities = entities.Where(c => c.FirstName.StartsWith(search) || c.LastName.StartsWith(search)).ToList();
                }
                else
                {
                    entities = await _context.Clients.Where(c => c.FirstName.StartsWith(search) || c.LastName.StartsWith(search)).ToListAsync();
                }
            }
            if (entities.Count <= 0 && (search is null && gender is null && ageFrom is null && ageTo is null && status is null ))
            {
                entities = await _context.Clients.Include(x => x.Contract).ToListAsync();
            }

            return entities;
        }

        [HttpGet("Singles")]
        public async Task<ActionResult<ReturnSinglesDto>> ReturnSingles([FromQuery] string gender)
        {
            var entities = await _context.Clients.Where(c => c.MatchedUserId == null && c.Sex == gender).ToListAsync();

            var res = new List<ReturnSinglesDto>();

            foreach(var item in entities)
            {
                var client = new ReturnSinglesDto();
                client.Id = item.Id;
                client.Name = $"{item.FirstName} {item.LastName}";
                client.Image = item.ProfileImagePath;
                res.Add(client);
            }

            return Ok(res);
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
            if (clientModel.MatchedUserId >  0)
            {
                var user = await _clientRepo.GetByIdAsync(clientModel.MatchedUserId!.Value);
                user.MatchedUserId = clientModel.Id;
                await _clientRepo.UpdateAsync(user);
            }
            else 
            {
                clientModel.MatchedUserId = null;
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
        public async Task<ActionResult> DeleteClient(int id)
        {
            var client = await _clientRepo.GetByIdAsync(id);

            if (client.MatchedUserId != null)
            {
                var matched = await _clientRepo.GetByIdAsync(client.MatchedUserId.Value);
                matched.MatchedUserId = null;
                var res = await _clientRepo.UpdateAsync(matched);
            }
            var deleteRes = await _clientRepo.DeleteAsync(id);

            if (deleteRes is null) return NotFound($"Client {id} not found");
            
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
        public async Task<ActionResult<ClientModel>> AddContractToClient(int clientId, IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var filePath = Path.Combine("wwwroot/contracts", $"{Guid.NewGuid().ToString()}.{fileExt}");
                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                var contract = new ContractModel()
                {
                    ClientId = clientId,
                    ContractPath = filePath.ToString()
                };
                var res = await _context.Contracts.AddAsync(contract);
                await _context.SaveChangesAsync();

                var client = await _context.Clients.FindAsync(clientId);
                client.ContractId = res.Entity.Id;
                var updateClient = await _clientRepo.UpdateAsync(client);

                return Ok(res);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{clientId}/SendMessage")]
        public async Task<ActionResult> SendMessageToClient(int clientId, [FromBody] MessageDto content)
        {
            var client = await _context.Clients.FindAsync(clientId);
            var message = new MessageModel(new string[] { client!.Email! }, "Message from Alpha Bonds", content.Message!);
            await _emailSender.SendEmailAsync(message);

            return Ok($"Message successfully sent");
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
                        if (clientAnswers.Count > 0)
                        {
                            var answerDto = new AnswerDto();
                            answerDto.AnswerValue = clientAnswers[0].AnswerValue;
                            answerDto.Selected = true;
                            answersDtos.Add(answerDto);
                        }
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

        [HttpPost("{clientId}/Images")]
        public async Task<ActionResult> AddClientsImages(int clientId, List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var imagesList = new List<ImageModel>();

            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
            bool hasImage = false;

            foreach (var file in files)
            {
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var filePath = Path.Combine("wwwroot/images", $"{Guid.NewGuid().ToString()}.{fileExt}");
                if (!hasImage)
                {
                    client.ProfileImagePath = "images/" + Path.GetFileName(filePath);
                    hasImage = true;
                }

                using (var img = Image.Load(file.OpenReadStream()))
                {
                    // var fullPath = Path.Combine(_web.WebRootPath, "uploads", file.FileName);
                    string newSize = ImageResize(img, 600, 600);
                    string[] sizeArray = newSize.Split(",");
                    img.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));
                    img.Save(filePath);
                }
                var image = new ImageModel()
                {
                    ImagePath = Path.GetFileName(filePath),
                    ClientId = clientId
                };
                var entity = await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();
                imagesList.Add(entity.Entity);
            }

            var res = await _clientRepo.UpdateAsync(client);

            return Ok(imagesList);
        }

        [HttpGet("{clientId}/Images")]
        public async Task<IReadOnlyList<ImageModel>> GetImagesByClientId(int clientId)
        {
            var entities = await _context.Images.Where(x => x.ClientId == clientId).ToListAsync();

            // var res = _mapper.Map<IReadOnlyList<ImageModel>, IReadOnlyList<ImageDto>>(entities);

            return entities;
        }

        // Resixe Image
        private string ImageResize(Image img, int MaxWidth, int MaxHeight)
        {
            if (img.Width > MaxWidth || img.Height > MaxHeight)
            {
                double widthRatio = (double)img.Width / (double)MaxWidth;
                double heightRatio = (double)img.Height / (double)MaxHeight;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);

                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }

        // Send Questions to Client
        [HttpPost("{clientId}/SendQuestion/{questionId}")]
        public async Task<ActionResult> SendQuestionsToClient(int clientId, int questionId)
        {
            var accessForm = new AccessQuestionFormModel()
            {
                AccessGuid = Guid.NewGuid(),
                ClientId = clientId,
                QuestionCategoryId = questionId
            };

            var client = await _context.Clients.FindAsync(clientId);

            if (client != null)
            {
                var res = await _context.AccessQuestionForm.AddAsync(accessForm);
                await _context.SaveChangesAsync();

                client.QuestionsSend = true;
                var updateRes = await _clientRepo.UpdateAsync(client);

                var message = new MessageModel(new string[] { client!.Email! }, "Message from Alpha Bonds", $"http://localhost:4200?access={accessForm.AccessGuid.ToString()}");
                await _emailSender.SendEmailAsync(message);
                return Ok();
            }

            return BadRequest();
        }

        // Add Interests
        [HttpPost("{clientId}/Interests")]
        public async Task<ActionResult> AddInterests(int clientId, List<int> interests)
        {
            var client = await _clientRepo.GetByIdAsync(clientId);
            client.ClientInterests = interests;
            var updateClient = await _clientRepo.UpdateAsync(client);
            return Ok(updateClient);
        }

        // Get Interest
        [HttpGet("{clientId}/Interests")]
        public async Task<ActionResult> GetClientInterests(int clientId)
        {
            var entity =  await _clientRepo.GetByIdAsync(clientId);

            var subInterestList = new List<SubInterestDto>();

            if (entity.ClientInterests.Count > 0)
            foreach(var item in entity.ClientInterests)
            {
                var sub = new SubInterestDto();
                var subInterest = await _context.SubInterests.Include(x => x.Interest).FirstOrDefaultAsync(x => x.Id == item);
                if (subInterest != null)
                {
                    sub.SubInterest =  subInterest.SubInterestName;
                    sub.SubInterestColor = subInterest.Interest.InterestColor;
                }
                subInterestList.Add(sub);
            }
            return Ok(subInterestList);
        }
    }
}