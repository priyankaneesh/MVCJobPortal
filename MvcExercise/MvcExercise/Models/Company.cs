using System.ComponentModel.DataAnnotations;

namespace MvcExercise.Models
{
    public partial class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Industry { get; set; } = string.Empty; // e.g., IT, Healthcare, Finance

        [MaxLength(100)]
        public string WebsiteUrl { get; set; } = string.Empty;

        [MaxLength(100)]
        public string ContactEmail { get; set; } = string.Empty;

        [MaxLength(15)]
        public string ContactPhone { get; set; } = string.Empty;

        // New fields
        [MaxLength(1000)]
        public string About { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Vision { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Mission { get; set; } = string.Empty;

        // Navigation Property - to list jobs posted by the company
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
