using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public interface IMedia
    {
        public string Report(CargoPlane cargoPlane);
        public string Report(PassengerPlane passengerPlane);
        public string Report(Airport airport);
    }
}
