using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Product : BaseEntity
    {
        
        //public IFormFile[] UploadedPhotos { get; set; }
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Старая цена")]
        public double? OldPrice { get; set; }
        [Display(Name = "Статус")]
        public bool Status { get; set; }
        [Display(Name = "Гарантия")]
        public string Guarantee { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }

        //категория
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        [Display(Name = "Категория")]
        public virtual ProductCategory Category { get; set; }


        //склад
        [Display(Name = "Склад")]
        public int? WarehouseId { get; set; }
        [Display(Name = "Склад")]
        public virtual Warehouse Warehouse { get; set; }
        public virtual List<Photo> Photos { get; set; } //= new List<Photo>();
    }
}
