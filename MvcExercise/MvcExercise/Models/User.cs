using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcExercise.Models
{

    [Index(nameof(Email), IsUnique = true)]

    public partial class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        [Required, MaxLength(100)]
        [EmailAddress]
        // Adding unique constraint to the email
        public string Email { get; set; } = null!;

        public string? Gender { get; set; }



        public string? Phone { get; set; }


        public string password { get; set; } = null!;


        [Required]
        public Enums.Roles Role { get; set; }



        public string? Designation { get; set; }

        [ForeignKey("Company")]
        public Guid? CompanyId { get; set; }

        // Navigation property to Company
        public Company? Company { get; set; }

         
    }


}

