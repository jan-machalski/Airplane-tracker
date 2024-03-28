using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Television:IMedia
    {
        public string Name { get; init; }
        public Television(string name)
        {
            Name = name;
        }

        public string Report(Airport airport)
        {
            return $"<An image of {airport.Name} airport>";
        }
        public string Report(CargoPlane cargoPlane)
        {
            return $"<An image of {cargoPlane.Model} cargo plane>";
        }
        public string Report(PassengerPlane passengerPlane)
        {
            return $"<An image of {passengerPlane.Model} passenger plane>";
        }
    }
}
