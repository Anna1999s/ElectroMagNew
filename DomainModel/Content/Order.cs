using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{

    public class Order
    {
        public int Id { get; set; }
        public virtual IList<OrderProduct> OrderProducts { get; set; }

        //клиент
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        //сотрудник
        public int? EmployeesId { get; set; }
        public virtual Employee Employees { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal OrderPrice { get; set; }
    }

}
