using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Client
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public virtual IList<Order> Orders { get; set; }
        
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
