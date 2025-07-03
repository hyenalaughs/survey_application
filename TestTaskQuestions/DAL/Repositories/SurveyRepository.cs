using Microsoft.EntityFrameworkCore;
using TestTaskQuestions.DAL.Core;
using TestTaskQuestions.DAL.Interfaces;
using TestTaskQuestions.Models;

namespace TestTaskQuestions.DAL.Repositories
{
    public class SurveyRepository : Repository<Survey>, ISurveyRepository
    {
        public SurveyRepository(SurveyDbContext context) : base(context) { }

        public async Task<Survey?> GetWithQuestionsAsync(Guid surveyId)
        {
            return await _context.Surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(s => s.Id == surveyId);
        }
    }
}
