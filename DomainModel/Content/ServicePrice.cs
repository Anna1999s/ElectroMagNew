using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Content
{
    public class ServicePrice :BaseEntity
    {
        public int? FilialId { get; set; }
        public Filial Filial { get; set; }
        public int? ServiceId { get; set; }
        public Service Service { get; set; }

        public double Price { get; set; }
        public int Time { get; set; }
        public List<Box> Boxes { get; set; }    
        public List<Emploeer> Emploeers { get; set; }}
}
