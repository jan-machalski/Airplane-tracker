using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Database
    {
        public static readonly Lazy<Database> instance = new Lazy<Database>(() => new Database());
        private Database() { }
        public static Database Instance => instance.Value;

        public Dictionary<UInt64, Airport> AirportInfo = new Dictionary<UInt64, Airport>();
        public Dictionary<UInt64, Cargo> CargoInfo = new Dictionary<UInt64, Cargo>();
        public Dictionary<UInt64, CargoPlane> CargoPlaneInfo = new Dictionary<UInt64, CargoPlane>();
        public Dictionary<UInt64, Crew> CrewInfo = new Dictionary<UInt64, Crew>();
        public Dictionary<UInt64, Flight> FlightInfo = new Dictionary<UInt64, Flight>();
        public Dictionary<UInt64, Passenger> PassengerInfo = new Dictionary<UInt64, Passenger>();
        public Dictionary<UInt64, PassengerPlane> PassengerPlaneInfo = new Dictionary<UInt64, PassengerPlane>();

        public void AddObject(Airport airport)
        {
            if(!AirportInfo.ContainsKey(airport.ID))
                AirportInfo.Add(airport.ID, airport);
        }
        public void AddObject(Cargo cargo)
        {
            if(!CargoInfo.ContainsKey(cargo.ID))
                CargoInfo.Add(cargo.ID, cargo);
        }
        public void AddObject(CargoPlane cargoPlane)
        {
            if(!CargoPlaneInfo.ContainsKey(cargoPlane.ID))
                CargoPlaneInfo.Add(cargoPlane.ID, cargoPlane);
        }
        public void AddObject(Crew crew)
        {
            if(!CrewInfo.ContainsKey(crew.ID))
                CrewInfo.Add(crew.ID, crew);
        }
        public void AddObject(Flight flight)
        {
            if(!FlightInfo.ContainsKey(flight.ID))
                FlightInfo.Add(flight.ID, flight);
        }
        public void AddObject(Passenger passenger)
        {
            if(!PassengerInfo.ContainsKey(passenger.ID))
            {
                PassengerInfo.Add(passenger.ID, passenger);
            }
        }
        public void AddObject(PassengerPlane passengerPlane)
        {
            if(!PassengerPlaneInfo.ContainsKey(passengerPlane.ID)) 
            {
                PassengerPlaneInfo.Add(passengerPlane.ID, passengerPlane);
            }
        }
    }
}
