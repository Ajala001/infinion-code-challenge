using App.Domain.Enums;

namespace App.Domain.Entities
{
    public class User : Auditables
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public Gender Gender { get; set; }
        public required string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? ProfilePic { get; set; }
    }
}
