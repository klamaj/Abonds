using Core.Models.Questions;
using Infrastructure.Data;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Questions
{
    public class QuestionCategoriesController : BaseApiController
    {
        private readonly IGenericRepository<QuestionCategoryModel> _questionRepo;
        private readonly DatabaseContext _context;
        private readonly IGenericRepository<QuestionModel> _questRepo;
        private readonly IGenericRepository<QuestionAnswerModel> _answerRepo;
        public QuestionCategoriesController(IGenericRepository<QuestionCategoryModel> questionRepo, DatabaseContext context, IGenericRepository<QuestionModel> questRepo, IGenericRepository<QuestionAnswerModel> answerRepo)
        {
            _answerRepo = answerRepo;
            _questRepo = questRepo;
            _context = context;
            _questionRepo = questionRepo;
        }

        /// <summary>
        /// Get QuestionCategories
        /// </summary>
        /// <returns>Question Categories List</returns>
        /// <response code="200">Question Categories List</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories
        ///
        /// </remarks>
        [HttpGet]
        public async Task<IReadOnlyList<QuestionCategoryModel>> GetQuestionCategories()
        {
            return await _questionRepo.ListAllAsync();
        }

        /// <summary>
        /// Add Question Category
        /// </summary>
        /// <param name="questionCategory" example="title">QuestionCategoryModel</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">QuestionCategory</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /QuestionCategories
        ///     {
        ///        "title": "Questions for men"
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add a Question Category</response>
        [HttpPost]
        public async Task<ActionResult<QuestionCategoryModel>> AddQuestionCategory([FromBody] QuestionCategoryModel questionCategory)
        {
            var questionCat = await _questionRepo.AddAsync(questionCategory);

            return Ok(questionCat);
        }

        /// <summary>
        /// Update QuestionCategory
        /// </summary>
        /// <param name="questionCategory" example="tetile">QuestionCategoryModel</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">Updated QuestionCategory</response>
        /// <remarks>
        /// Sample request:
        ///
        ///    PUT /QuestionCategories
        ///    {
        ///         "id": 1,
        ///         "title": "Questions for men",
        ///    }
        ///
        /// </remarks>
        /// <response code="404">QuestionCategory not found</response>
        [HttpPut]
        public async Task<ActionResult<QuestionCategoryModel>> UpdateQuestionCategory([FromBody] QuestionCategoryModel questionCategory)
        {
            var questionCat = await _questionRepo.UpdateAsync(questionCategory);

            if (questionCat is null) return NotFound($"Client {questionCategory.Id} not found");

            return Ok(questionCat);
        }

        /// <summary>
        /// Get QuestionCategory by ID
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">QuestionCategory</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories/32
        ///
        /// </remarks>
        /// <response code="404">QuestionCategory not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionCategoryModel>> GetQuestionCategoryById(int id)
        {
            var questionCat = await _questionRepo.GetByIdAsync(id);

            if (questionCat is null)
            {
                // _logger.LogInformation($"Client {id} not Found");
                return NotFound("Client not found");
            }

            // _logger.LogInformation($"Client {id} returned");
            return Ok(questionCat);
        }

        /// <summary>
        /// Delete QuestionCategory
        /// </summary>
        /// <param name="id" example="32">int</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">QuestionCategory</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /QuestionCategories/32
        ///
        /// </remarks>
        /// <response code="404">QuestionCategory not Found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionCategoryModel>> DeleteQuestionCategory(int id)
        {
            var questionCat = await _questionRepo.DeleteAsync(id);

            if (questionCat is null) return NotFound($"Client {id} not found");

            return Ok($"Client {questionCat.Id} successfully deleted");
        }

        /// <summary>
        /// Get Questions per Category
        /// </summary>
        /// <returns>Get Questions List per  Category</returns>
        /// <response code="200">Get Questions List per Category</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories/32/Questions
        ///
        /// </remarks>
        [HttpGet("{id}/Questions")]
        public async Task<IReadOnlyList<QuestionModel>> GetQuestionsByCategoryId(int id)
        {
            var questions = await _context.Questions
                .Where(q =>  q.QuestionCategoryId == id)
                .AsNoTracking()
                .ToListAsync();

            return questions;
        }

        /// <summary>
        /// Get Question by ID based on Category ID
        /// </summary>
        /// <param name="questionId" example="12">int</param>
        /// <returns>QuestionModel</returns>
        /// <response code="200">Question</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories/32/Questions/12
        ///
        /// </remarks>
        /// <response code="404">Question not Found</response>
        [HttpGet("{id}/Questions/{questionId}")]
        public async Task<ActionResult<QuestionModel>> GetQuestionByIdBasedOnCateogryId(int questionId)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(r => r.Id == questionId);

            if (question == null) return NotFound(question);

            return Ok(question);
        }

        /// <summary>
        /// Add Question to Category
        /// </summary>
        /// <param name="question" example="titel, type, required, questionCategoryId">QuestionModel</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">QuestionCategory</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /QuestionCategories/32/Questions
        ///     {
        ///        "title": "Some question",
        ///        "type": "radio",
        ///        "required": false,
        ///        "questionCategoryId": 32,
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add a Question to Category</response>
        [HttpPost("{id}/Questions")]
        public async Task<ActionResult<QuestionModel>> AddQuestionToCategory(QuestionModel question)
        {
            var res = await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            
            if (res is null) return BadRequest(); 
            return Ok(res);
        }

        /// <summary>
        /// Update Question to Category
        /// </summary>
        /// <param name="question" example="titel, type, required, questionCategoryId">QuestionModel</param>
        /// <returns>QuestionCategoryModel</returns>
        /// <response code="200">Question</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /QuestionCategories/32/Questions
        ///     {
        ///        "id": 12
        ///        "title": "Some question",
        ///        "type": "radio",
        ///        "required": false,
        ///        "questionCategoryId": 32,
        ///     }
        ///
        /// </remarks>
        /// <response code="404">Question not found</response>
        [HttpPut("{id}/Questions")]
        public async Task<ActionResult<QuestionModel>> UpdateQuestion([FromBody] QuestionModel question)
        {
            var res = await _questRepo.UpdateAsync(question);

            if (res is null) return NotFound($"Client {question.Id} not found");

            return Ok(res);
        }

        /// <summary>
        /// Delete Question
        /// </summary>
        /// <param name="questionId" example="12">int</param>
        /// <returns>QuestionModel</returns>
        /// <response code="200">Question</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /QuestionCategories/32/Questions/12
        ///
        /// </remarks>
        /// <response code="404">Question not Found</response>
        [HttpDelete("{id}/Questions/{questionId}")]
        public async Task<ActionResult<string>> DeleteQuestion(int questionId)
        {
            var res = await _questRepo.DeleteAsync(questionId);

            if (res is null) return NotFound($"Client {questionId} not found");

            return Ok($"Client {res.Id} successfully deleted");
        }

        /// <summary>
        /// Get Answers of Question
        /// </summary>
        /// <returns>QuestionAnswerModel List</returns>
        /// <param name="categoryId" example="32">int</param>
        /// <param name="questionId" example="12">int</param>
        /// <response code="200">QuestionAnswer List</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories/32/Questions/12/Answers
        ///
        /// </remarks>
        [HttpGet("{categoryId}/Questions/{questionId}/Answers")]
        public async Task<IReadOnlyList<QuestionAnswerModel>> GetQuestionAnswers(int categoryId, int questionId)
        {
            var res = await _context.QuestionsAnswers
                .Where(a => a.QuestionId == questionId)
                .AsNoTracking()
                .ToListAsync();

            return res;
        }

        /// <summary>
        /// Add Answer to Question
        /// </summary>
        /// <param name="categoryId" example="32">int</param>
        /// <param name="questionId" example="12">int</param>
        /// <param name="answer" example="value">AnswerModel</param>
        /// <returns>AnswerModel</returns>
        /// <response code="200">Answer</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /QuestionCategories/32/Questions/12/Answers
        ///     {
        ///        "value": "Some answer",
        ///        "questionId": "12"
        ///     }
        ///
        /// </remarks>
        /// <response code="400">An error occured while trying to add an Answer</response>
        [HttpPost("{categoryId}/Questions/{questionId}/Answers")]
        public async Task<ActionResult<QuestionAnswerModel>> AddAnswer(int categoryId, int questionId, QuestionAnswerModel answer)
        {
            answer.QuestionId = questionId;
            var res = await _answerRepo.AddAsync(answer);

            if (res is null) return BadRequest();

            return Ok(res);
        }

        /// <summary>
        /// Update Answer
        /// </summary>
        /// <param name="categoryId" example="32">int</param>
        /// <param name="questionId" example="12">int</param>
        /// <param name="answer" example="value">AnswerModel</param>
        /// <returns>QuestionAnswerModel</returns>
        /// <response code="200">QuestionAnswer</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /QuestionCategories/32/Questions/12/Answers
        ///     {
        ///        "id": 1
        ///        "value": "Some answer",
        ///        "questionId": "12"
        ///     }
        ///
        /// </remarks>
        /// <response code="404">Answer not found</response>
        [HttpPut("{categoryId}/Questions/{questionId}/Answers")]
        public async Task<ActionResult<QuestionAnswerModel>> UpdateAnswer(int categoryId, int questionId, QuestionAnswerModel answer)
        {
            var res = await _answerRepo.UpdateAsync(answer);

            if (res is null) return NotFound($"Answer {answer!.Id} not found");

            return Ok(res);
        }

        /// <summary>
        /// Get Answer by ID
        /// </summary>
        /// <param name="categoryId" example="32">int</param>
        /// <param name="questionId" example="12">int</param>
        /// <param name="answerId" example="32">int</param>
        /// <returns>QuestionAnswerModel</returns>
        /// <response code="200">QuestionAnswer</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /QuestionCategories/32/Questions/12/Answers/1
        ///
        /// </remarks>
        /// <response code="404">QuestionAnswer not Found</response>
        [HttpGet("{categoryId}/Questions/{quesitonId}/Answers/{answerId}")]
        public async Task<ActionResult<QuestionAnswerModel>> GetAnswerById(int categoryId, int questionId, int answerId)
        {
            var res = await _answerRepo.GetByIdAsync(answerId);

            if (res is null) return NotFound($"Answer {answerId} not found");

            return Ok(res);
        }

        /// <summary>
        /// Delete Answer
        /// </summary>
        /// <param name="categoryId" example="32">int</param>
        /// <param name="questionId" example="12">int</param>
        /// <param name="answerId" example="32">int</param>
        /// <returns>QuestionAnswerModel</returns>
        /// <response code="200">QuestionAnswer</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /QuestionCategories/32/Questions/12/Answers/1
        ///
        /// </remarks>
        /// <response code="404">Answer not Found</response>
        [HttpDelete("{categoryId}/Questions/{questionId}/Answers/{answerId}")]
        public async Task<ActionResult<string>> DeleteAnswer(int categoryId, int questionId, int answerId)
        {
            var res = await _answerRepo.DeleteAsync(answerId);

            if (res is null) return NotFound($"Answer {answerId} not found");

            return Ok($"Answer {res.Id} successfully deleted");
        }
    }
}