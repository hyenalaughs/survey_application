using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTaskQuestions.Models
{
    public class Result
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("Interview")]
        public Guid InterviewId { get; set; }
        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        [ForeignKey("Answer")]
        public Guid AnswerId { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
        public Interwiew Interview { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public Answer Answer { get; set; } = null!;
    }
}
