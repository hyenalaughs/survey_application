using TestTaskQuestions.Models.DTO;

namespace TestTaskQuestions.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<QuestionDto?> GetQuestionAsync(Guid surveyId, int order);
        Task<NextQuestionResultDto?> SaveAnswerAsync(Guid interviewId, AnswerSubmissionDto dto);
    }
}
