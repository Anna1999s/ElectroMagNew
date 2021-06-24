using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class BasketProductViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }      
        public string Name { get; set; }
        public string Img { get; set; }       
        public int MaxCount { get; set; }        


        public static BasketProductViewModel FromDomainModel(BasketItem item)
        {
            return new BasketProductViewModel
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Count = item.Count
            };
        }
    }
}
