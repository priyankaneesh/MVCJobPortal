using System.ComponentModel.DataAnnotations;

namespace MvcExercise.Dtos
{
    public class LoginDtos
    {
        [Required, MaxLength(100)]

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }
    }

}
