using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.Content
{

    public class Order
    {
        public int Id { get; set; }
        public virtual IList<Product> Products { get; set; }

        //клиент
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }

        //сотрудник
        public int? EmployeesId { get; set; }
        public virtual Employee Employees { get; set; }

        public DateTime OrderDate { get; set; }
        public double OrderPrice { get; set; }
    }

}
