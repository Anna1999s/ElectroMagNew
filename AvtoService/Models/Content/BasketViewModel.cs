using DomainModel.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models.Content
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public List<BasketProductViewModel> Products { get; set; }
        //[Required]
        //[Display(Name = "Город")]
        //public int CityId { get; set; }
        //public IList<SelectListItem> Cities { get; set; }
        //[Required]
        //[Display(Name = "Адрес")]
        //public string Address { get; set; }

        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[Display(Name = "Телефон")]
        //public string Phone { get; set; }
        //[Required]
        //[Display(Name = "ФИО получателя")]
        //public string BeneficiaryName { get; set; }


        //[Range(1, int.MaxValue, ErrorMessage = "Выберите способ доставки")]
        //public int DeliveryId { get; set; }
        //public IList<DeliveryViewModel> Deliveries { get; set; }
        //public double DeliveryPrice { get; set; }


        //[Range(1, int.MaxValue, ErrorMessage = "Выберите способ оплаты")]
        //public int PaymentTypeId { get; set; }
        //public IList<PaymentTypeViewModel> PaymentTypes { get; set; }
        //public string MinPrice { get; set; }

        public double Sum { get; set; }
        public double Total { get; set; }

        public Guid KeyGuid { get; set; }

        public BasketViewModel()
        {
            Products = new List<BasketProductViewModel>();
        }

        public static BasketViewModel FromDomainModel(Basket basket)
        {
            return new BasketViewModel
            {
                Id = basket.Id,             
                Products =
                    basket.Items != null
                        ? basket.Items.Select(BasketProductViewModel.FromDomainModel).ToList()
                        : new List<BasketProductViewModel>()
            };
        }
    }
}
