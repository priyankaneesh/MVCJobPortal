using System.ComponentModel.DataAnnotations;

namespace MvcExercise.Models
{
    public partial class Job
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = null!;

        [Required, MaxLength(500)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime PostedDate { get; set; } = DateTime.Now;

        // Additional Fields
        [Required]
        public string EmploymentType { get; set; } = "Full-Time"; // Full-Time, Part-Time, etc.

        [Required]
        public string ExperienceLevel { get; set; } = "Entry-Level"; // Entry-Level, Mid-Level, Senior-Level, etc.

        [MaxLength(250)]
        public string Requirements { get; set; } = string.Empty; // Key job requirements

        [MaxLength(250)]
        public string Benefits { get; set; } = string.Empty; // Benefits offered, if any

        public string CompanyName { get; set; } = "Default Company";

        [MaxLength(500)]
        public string CompanyDescription { get; set; } = string.Empty;

    }
}
