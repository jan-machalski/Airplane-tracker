using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Radio: IMedia
    {
        public string Name { get; init; }
        public Radio(string name)
        {
            Name = name;
        }   
        public string Report(Airport airport)
        {
            return $"Reporting for {Name}, Ladies and gentelmen, we are at the {airport.Name} airport.";
        }
        public string Report(CargoPlane cargoPlane)
        {
            return $"Reporting for {Name}, Ladies and gentelmen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }
        public string Report(PassengerPlane passengerPlane)
        {
            return $"Reporting for {Name},Ladies and gentelmen, we’ve just witnessed {passengerPlane.Serial} take off.";
        }
    }
}
