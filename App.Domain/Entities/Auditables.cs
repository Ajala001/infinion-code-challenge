namespace App.Domain.Entities
{
    public abstract class Auditables
    {
        public Guid Id { get; set; }        
        public required DateTime CreatedAt { get; set; }
        public required string CreatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
