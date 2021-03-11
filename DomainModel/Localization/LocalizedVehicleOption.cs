using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Localization
{
    public class LocalizedVehicleOption: LocalizedData
    {
        public int? VehicleTypeId { get; set; }
    }
}
