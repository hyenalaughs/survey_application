using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestTaskQuestions.Models
{
    public class Interwiew
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey("Survey")]
        public Guid SurveyId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public Survey Survey { get; set; } = null!;
        public ICollection<Result> Results { get; set; } = new List<Result>();
    }
}
