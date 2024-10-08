namespace App.Domain.Entities
{
    public class Product : Auditables
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
