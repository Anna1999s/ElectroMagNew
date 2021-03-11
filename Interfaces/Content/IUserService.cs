using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Interfaces.Content
{
    public interface IUserService
    {
        Task<IList<IdentityUser>> GetAll();

        Task<IdentityUser> GetById(int id);

    }
}
