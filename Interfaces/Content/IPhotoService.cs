using System.Threading.Tasks;
using DomainModel.Content;
using Microsoft.AspNetCore.Http;

namespace Interfaces.Content
{
    public interface IPhotoService
    {
        Task<Photo> Add(IFormFile photo, int vehicleId, string filePath);
        Task<Photo> AddByParser(string photoName, int vehicleId, string filePath);
    }
}