using Microsoft.AspNetCore.Mvc;
using TestTaskQuestions.Models.DTO;
using TestTaskQuestions.Services.Interfaces;

namespace TestTaskQuestions.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [HttpGet("/api/survey/{surveyId}/questions/{order:int}")]
        public async Task<IActionResult> GetQuestion(Guid surveyId, int order)
        {
            var result = await _surveyService.GetQuestionAsync(surveyId, order);
            if (result == null)
                return NotFound(new { message = "Question not found" });

            return Ok(result);
        }

        [HttpPost("/api/interviews/{interviewId}/answers")]
        public async Task<IActionResult> SubmitAnswer(Guid interviewId, [FromBody] AnswerSubmissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _surveyService.SaveAnswerAsync(interviewId, dto);
            if (result == null)
                return NotFound(new { message = "Invalid interview/question/answer" });

            return Ok(result);
        }

    }
}
