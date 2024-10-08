using System.ComponentModel.DataAnnotations;

namespace App.Domain.DTOs.Requests
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter A Valid Email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Please Enter A Strong Password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
