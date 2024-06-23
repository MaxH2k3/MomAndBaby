using System.ComponentModel.DataAnnotations;

namespace MomAndBaby.BusinessObject.Models
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        [MaxLength(250, ErrorMessage = "Name is too long!")]
        [MinLength(3, ErrorMessage = "Name is too short!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Password { get; set; }

        public LoginUserDto(string userName, string email, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }
    }
}
