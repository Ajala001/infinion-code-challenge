namespace App.Domain.DTOs.Requests
{
    public record ProductFilterDto
    {
        public string? SearchQuery { get; init; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
