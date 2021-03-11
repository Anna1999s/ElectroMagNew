using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class Order : BaseEntity
    {
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int? EmploeerId { get; set; }
        public Emploeer Emploeer { get; set; }

        public List<ServicePrice> Services { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get => DateStart.AddMinutes(Services.Sum(_ => _.Time)); }
    }
}
