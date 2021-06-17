using System;
using System.IO;
using System.Threading.Tasks;
using DomainModel.Content;
using Interfaces.Content;
using Microsoft.AspNetCore.Http;

namespace Services.Content
{
    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _dbContext;

        public PhotoService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Photo> Add(IFormFile photo, int productId, string wwwroot)
        {
            var filePath =
                Directory.CreateDirectory(Path.Combine(wwwroot, WorkContext.ImagePath, productId.ToString()));
            if (!Directory.Exists(filePath.FullName)) Directory.CreateDirectory(filePath.FullName);

            var fileName = Path.Combine(WorkContext.ImagePath, productId.ToString(),
                Guid.NewGuid() + Path.GetExtension(photo.FileName));

            await using var fileSteam = new FileStream(Path.Combine(wwwroot, fileName), FileMode.Create);
            await photo.CopyToAsync(fileSteam);

            var entity = new Photo
            {
                Path = @"\" + fileName,
                ProductId = productId
            };
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Photo> AddNew(IFormFile photo, int newId, string wwwroot)
        {
            var filePath =
                Directory.CreateDirectory(Path.Combine(wwwroot, WorkContext.ImagePath, newId.ToString()));
            if (!Directory.Exists(filePath.FullName)) Directory.CreateDirectory(filePath.FullName);

            var fileName = Path.Combine(WorkContext.ImagePath, newId.ToString(),
                Guid.NewGuid() + Path.GetExtension(photo.FileName));

            await using var fileSteam = new FileStream(Path.Combine(wwwroot, fileName), FileMode.Create);
            await photo.CopyToAsync(fileSteam);

            var entity = new Photo
            {
                Path = @"\" + fileName
            };
            //await _dbContext.AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}