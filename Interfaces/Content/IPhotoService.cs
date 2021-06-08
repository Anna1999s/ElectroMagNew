using System.Threading.Tasks;
using DomainModel.Content;
using Microsoft.AspNetCore.Http;

namespace Interfaces.Content
{
    public interface IPhotoService
    {
        Task<Photo> Add(IFormFile photo, int productId, string filePath);
        
    }
}