namespace App.Domain.DTOs.Responses
{
    public class ProductResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Price { get; set; }
    }
}
