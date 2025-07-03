using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTaskQuestions.Models
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("Survey")]
        public Guid SurveyId { get; set; }
        public string Text { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
        public Survey Survey { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
