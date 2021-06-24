using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public IList<BasketItem> Items { get; set; }
        public Guid KeyGuid { get; set; }
    }
}
