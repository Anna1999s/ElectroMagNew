using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Content;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Services.Content
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<IdentityUser>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync();
        }
    }
}
