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
        public virtual IdentityUser User { get; set; }
        public virtual IList<BasketItem> Items { get; set; }
        public Guid KeyGuid { get; set; }
    }
}
