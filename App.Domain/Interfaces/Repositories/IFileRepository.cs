using Microsoft.AspNetCore.Http;

namespace App.Domain.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
