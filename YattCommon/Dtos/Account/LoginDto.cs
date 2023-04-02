using System.ComponentModel.DataAnnotations;

namespace YattCommon.Dtos.Account
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Minimum Password length is 6 character")]
        [MaxLength(15, ErrorMessage = "Maximum Password length is 15 character")]
        public string Password { get; set; } = string.Empty;
    }
}
