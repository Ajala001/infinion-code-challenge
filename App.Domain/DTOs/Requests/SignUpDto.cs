using App.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.DTOs.Requests
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your First Name")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [EmailAddress(ErrorMessage = "Enter A Valid Email")]
        public required string Email { get; set; }


        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        public required string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please Enter A Strong Password")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Your Password")]
        [Compare("Password", ErrorMessage = "Passwords Do Not Match")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please Select Your Gender")]
        public required Gender Gender { get; set; }

        public IFormFile? ProfilePic { get; set; }

    }
}
