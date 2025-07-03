using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTaskQuestions.Models
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Question")]
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Question Question { get; set; } = null!;
    }
}
