using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Employee
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }
        public string Function { get; set; } //должность

        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
