using TestTaskQuestions.Models.DTO;
using TestTaskQuestions.Models;
using TestTaskQuestions.Services.Interfaces;
using TestTaskQuestions.DAL.Interfaces;

namespace TestTaskQuestions.Services.Core
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IRepository<Question> _questionRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly IRepository<Interwiew> _interviewRepository;
        private readonly IRepository<Result> _resultRepository;

        public SurveyService(
            ISurveyRepository surveyRepository,
            IRepository<Question> questionRepository,
            IRepository<Answer> answerRepository,
            IRepository<Interwiew> interviewRepository,
            IRepository<Result> resultRepository)
        {
            _surveyRepository = surveyRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _interviewRepository = interviewRepository;
            _resultRepository = resultRepository;
        }

        public async Task<QuestionDto?> GetQuestionAsync(Guid surveyId, int order)
        {
            var question = (await _questionRepository
                .GetAllAsync())
                .Where(q => q.SurveyId == surveyId && q.OrderNumber == order)
                .Select(q => new QuestionDto
                {
                    QuestionId = q.Id,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Text = a.Text
                    }).ToList()
                })
                .FirstOrDefault();

            return question;
        }

        public async Task<NextQuestionResultDto?> SaveAnswerAsync(Guid interviewId, AnswerSubmissionDto dto)
        {
            var interview = await _interviewRepository.GetByIdAsync(interviewId);
            if (interview == null)
                return null;

            var question = await _questionRepository.GetByIdAsync(dto.QuestionId);
            if (question == null || question.SurveyId != interview.SurveyId)
                return null;

            var answer = await _answerRepository.GetByIdAsync(dto.AnswerId);
            if (answer == null || answer.QuestionId != question.Id)
                return null;

            var existing = (await _resultRepository.GetAllAsync())
                .FirstOrDefault(r => r.InterviewId == interviewId && r.QuestionId == dto.QuestionId);

            if (existing == null)
            {
                var result = new Result
                {
                    InterviewId = interviewId,
                    QuestionId = dto.QuestionId,
                    AnswerId = dto.AnswerId,
                    AnsweredAt = DateTime.UtcNow
                };

                await _resultRepository.AddAsync(result);
                await _resultRepository.SaveChangesAsync();
            }

            var next = (await _questionRepository.GetAllAsync())
                .Where(q => q.SurveyId == interview.SurveyId && q.OrderNumber > question.OrderNumber)
                .OrderBy(q => q.OrderNumber)
                .FirstOrDefault();

            return new NextQuestionResultDto
            {
                NextOrder = next?.OrderNumber,
                IsFinished = next == null
            };
        }
    }
}

