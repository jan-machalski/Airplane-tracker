using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Newspaper:IMedia
    {
        public string Name { get; init; }
        public Newspaper(string name)
        {
            Name = name;
        }
        public string Report(Airport airport)
        {
            return $"{Name} - A report from the {airport.Name} airport, {airport.Country}.";
        }
        public string Report(CargoPlane cargoPlane)
        {
            return $"{Name} - An interview with the crew of {cargoPlane.Serial}.";
        }
        public string Report(PassengerPlane passengerPlane)
        {
            return $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
        }
    }
}
