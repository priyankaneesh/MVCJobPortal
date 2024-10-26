using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcExercise.Models
{
    public partial class AppliedJobs
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Job")]
        public Guid JobId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Job Job { get; set; } = null!;

        public DateTime AppliedDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string Status { get; set; } = "Pending"; // e.g., Pending, Accepted, Rejected
    }
}
