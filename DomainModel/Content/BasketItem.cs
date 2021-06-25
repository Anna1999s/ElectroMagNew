using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class BasketItem : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }
    }
}
