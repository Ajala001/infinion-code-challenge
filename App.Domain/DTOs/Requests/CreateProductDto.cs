using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.DTOs.Requests
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Please Enter the Product Name")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please Enter The Price")]
        public required decimal Price { get; set; }
        public IFormFile? ImageUrl { get; set; }
    }
}
