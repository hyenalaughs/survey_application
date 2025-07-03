using TestTaskQuestions.Models;

namespace TestTaskQuestions.DAL.Interfaces
{
    public interface ISurveyRepository : IRepository<Survey>
    {
        Task<Survey?> GetWithQuestionsAsync(Guid surveyId);
    }
}
