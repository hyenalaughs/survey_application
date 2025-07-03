namespace TestTaskQuestions.Models.DTO
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
