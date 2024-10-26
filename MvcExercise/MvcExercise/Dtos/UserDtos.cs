using Microsoft.EntityFrameworkCore;
using MvcExercise.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MvcExercise.Enums;

namespace MvcExercise.Dtos
{
 

    public partial class UserDtos
    {
     

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


        public UserDtos() { }
        public UserDtos(string firstname, string lastname, string email, string gender, string phone, string password, Roles role, Guid? companyId, Company? company, string? designation)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            Gender = gender;
            Phone = phone;
            this.password = password;
            Role = role;
            CompanyId = companyId;
            Company = company;
            Designation = designation;


        }
    }
}
