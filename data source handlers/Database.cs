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
        public UInt64 MaxID = 0;

        public Dictionary<UInt64, Airport> AirportInfo = new Dictionary<UInt64, Airport>();
        public Dictionary<UInt64, Cargo> CargoInfo = new Dictionary<UInt64, Cargo>();
        public Dictionary<UInt64, CargoPlane> CargoPlaneInfo = new Dictionary<UInt64, CargoPlane>();
        public Dictionary<UInt64, Crew> CrewInfo = new Dictionary<UInt64, Crew>();
        public Dictionary<UInt64, Flight> FlightInfo = new Dictionary<UInt64, Flight>();
        public Dictionary<UInt64, Passenger> PassengerInfo = new Dictionary<UInt64, Passenger>();
        public Dictionary<UInt64, PassengerPlane> PassengerPlaneInfo = new Dictionary<UInt64, PassengerPlane>();
        public HashSet<UInt64> UsedIDs = new HashSet<UInt64>();
        public void AddObject(Airport airport)
        {
            if (!UsedIDs.Contains(airport.ID))
            {
                AirportInfo.Add(airport.ID, airport);
                UsedIDs.Add(airport.ID);
                if (airport.ID > MaxID)
                    MaxID = airport.ID;
            }
            else
                throw new ArgumentException($"Id: {airport.ID} already in use");
        }
        public void AddObject(Cargo cargo)
        {
            if (!UsedIDs.Contains(cargo.ID))
            {
                CargoInfo.Add(cargo.ID, cargo);
                UsedIDs.Add(cargo.ID);
                if(cargo.ID > MaxID)
                    MaxID = cargo.ID;
            }
            else
                throw new ArgumentException($"Id: {cargo.ID} already in use");
        }
        public void AddObject(CargoPlane cargoPlane)
        {
            if (!UsedIDs.Contains(cargoPlane.ID))
            {
                CargoPlaneInfo.Add(cargoPlane.ID, cargoPlane);
                UsedIDs.Add(cargoPlane.ID);
                if(cargoPlane.ID > MaxID)
                    MaxID = cargoPlane.ID;
            }
            else
                throw new ArgumentException($"Id: {cargoPlane.ID} already in use");
        }
        public void AddObject(Crew crew)
        {
            if (!UsedIDs.Contains(crew.ID))
            {
                CrewInfo.Add(crew.ID, crew);
                UsedIDs.Add(crew.ID);
                if(crew.ID > MaxID)
                    MaxID = crew.ID;
            }
            else
                throw new ArgumentException($"Id: {crew.ID} already in use");
        }
        public void AddObject(Flight flight)
        {
            if (!UsedIDs.Contains(flight.ID))
            {
                FlightInfo.Add(flight.ID, flight);
                UsedIDs.Add(flight.ID);
                if(flight.ID > MaxID)
                    MaxID = flight.ID;
            }
            else
                throw new ArgumentException($"Id: {flight.ID} already in use");
        }

        public void AddObject(Passenger passenger)
        {
            if(!UsedIDs.Contains(passenger.ID))
            {
                PassengerInfo.Add(passenger.ID, passenger);
                UsedIDs.Add(passenger.ID);
                if(passenger.ID > MaxID)
                    MaxID = passenger.ID;
            }
            else
                throw new ArgumentException($"Id: {passenger.ID} already in use");
        }
        public void AddObject(PassengerPlane passengerPlane)
        {
            if(!UsedIDs.Contains(passengerPlane.ID)) 
            {
                PassengerPlaneInfo.Add(passengerPlane.ID, passengerPlane);
                UsedIDs.Add(passengerPlane.ID);
                if(passengerPlane.ID > MaxID)
                    MaxID = passengerPlane.ID;
            }
            else
                throw new ArgumentException($"Id: {passengerPlane.ID} already in use");
        }
        public List<AviationObject> GetObjectList(string fieldName)
        {
            List<AviationObject> result = new List<AviationObject>();
           switch(fieldName)
            {
                case ("Airport"):
                case ("Airports"):
                    foreach (var p in AirportInfo) result.Add(p.Value);
                    break;
                case ("Cargo"):
                case ("Cargos"):
                    foreach(var p in CargoInfo) result.Add(p.Value);
                    break;
                case ("CargoPlane"):
                case ("CargoPlanes"):
                    foreach( var p in CargoPlaneInfo) result.Add(p.Value);
                    break;
                case ("Crew"):
                case ("Crews"):
                    foreach(var p in CrewInfo) result.Add(p.Value);
                    break;
                case ("Flight"):
                case ("Flights"):
                    foreach(var p in FlightInfo) result.Add(p.Value);
                    break;
                case ("Passenger"):
                case ("Passengers"):
                    foreach(var p in PassengerInfo) result.Add(p.Value);
                    break;
                case ("PassengerPlane"):
                case ("PassengerPlanes"):
                    foreach(var p in PassengerPlaneInfo)result.Add(p.Value);
                    break;
                default:
                    throw new ArgumentException($"Invalid object class name: {fieldName}");

            }
            return result;
        }
        public void DeleteObject(UInt64 id)
        {
            if (!UsedIDs.Contains(id))
                return;
            if(AirportInfo.ContainsKey(id))
                AirportInfo.Remove(id);
            else if(CargoInfo.ContainsKey(id))
                CargoInfo.Remove(id);
            else if(CargoPlaneInfo.ContainsKey(id))
                CargoPlaneInfo.Remove(id);
            else if(CrewInfo.ContainsKey(id))
                CrewInfo.Remove(id);
            else if(FlightInfo.ContainsKey(id))
                FlightInfo.Remove(id);
            else if(PassengerInfo.ContainsKey(id))
                PassengerInfo.Remove(id);
            else if(PassengerPlaneInfo.ContainsKey(id))
                PassengerPlaneInfo.Remove(id);
            UsedIDs.Remove(id);
        }
        public void UpdateContactInfo(UInt64 id, string phoneNumber, string emailAdress)
        {
            if (PassengerInfo.ContainsKey(id))
            {
                PassengerInfo[id].Phone = phoneNumber;
                PassengerInfo[id].Email = emailAdress;
                Logger.LogMessage($"Passenger (id: {id}) changed their contact info to phone: {phoneNumber} and email: {emailAdress}");
            }
            else if (CrewInfo.ContainsKey(id))
            {
                string oldPhone = CrewInfo[id].Phone;
                string oldEmail = CrewInfo[id].Email;
                CrewInfo[id].Phone = phoneNumber;
                CrewInfo[id].Email = emailAdress;
                Logger.LogMessage($"Crew (id: {id}) changed their contact info to phone: from {oldPhone} to {phoneNumber} and email: from {oldEmail} to{emailAdress}");
            }
            else
                Logger.LogMessage($"Unable to change contact info - no crew or passengers with id: {id}");

        }
        public void UpdateFlightPos(UInt64 id,Single newLongitude,Single newLatitude,Single AMSL)
        {
            if (FlightInfo.ContainsKey(id))
            {
                var oldAMSL = FlightInfo[id].AMSL;
                var oldLongitude = FlightInfo[id].Longitude;
                var oldLatitude = FlightInfo[id].Latitude;
                FlightInfo[id].AMSL = AMSL;
                FlightInfo[id] = new FlightWithUpdatedPos(FlightInfo[id], newLongitude, newLatitude);
                Logger.LogMessage($"Flight (id: {id}) succesfully changed position: AMSL: from {oldAMSL} to {AMSL}, " +
                    $"Longitude: from {oldLongitude} to {newLongitude}, Latitude: from {oldLatitude} to {newLatitude}");
            }
            else if(CargoPlaneInfo.ContainsKey(id) || PassengerPlaneInfo.ContainsKey(id))
            {
                foreach(var f in FlightInfo)
                {
                    var time = f.Value.GetFlightTime();
                    if(FlightGuiAdapter.GetProgress(time.takeOffTime,time.landingTime)>0 && f.Value.Plane.ID == id)
                    {
                        var oldAMSL = f.Value.AMSL;
                        var oldLongitude = f.Value.Longitude;
                        var oldLatitude = f.Value.Latitude;
                        FlightInfo[id].AMSL = AMSL;
                        FlightInfo[id] = new FlightWithUpdatedPos(FlightInfo[id], newLongitude, newLatitude);
                        Logger.LogMessage($"Plane (id: {id}) succesfully changed position: AMSL: from {oldAMSL} to {AMSL}, " +
                            $"Longitude: from {oldLongitude} to {newLongitude}, Latitude: from {oldLatitude} to {newLatitude}");
                        break;
                    }
                }
            }
            else
            {
                Logger.LogMessage($"Unable to change flight position - flight with given id ({id}) does not exist");
            }
        }
        public void ChangeID(UInt64 oldID, UInt64 newID)
        {
            if(oldID == newID)
            {
                Logger.LogMessage($"ID change failed - old ID ({oldID}) is the same as new ID");
                return;
            }
            if(UsedIDs.Contains(newID))
            {
                Logger.LogMessage($"ID change failed - new ID ({newID}) is already assigned to a different object");
                return;
            }
            if(AirportInfo.ContainsKey(oldID))
            {
                var value = AirportInfo[oldID];
                AirportInfo.Remove(oldID);
                AirportInfo.Add(newID, value);
                Logger.LogMessage($"Airport (ID: {oldID}) changed ID to {newID}");
                return;
            }
            else if (CargoInfo.ContainsKey(oldID))
            {
                var value = CargoInfo[oldID];
                CargoInfo.Remove(oldID);
                CargoInfo.Add(newID, value);
                Logger.LogMessage($"Cargo (ID: {oldID}) changed ID to {newID}");
            }
            else if (CargoPlaneInfo.ContainsKey(oldID))
            {
                var value = CargoPlaneInfo[oldID];
                CargoPlaneInfo.Remove(oldID);
                CargoPlaneInfo.Add(newID, value);
                Logger.LogMessage($"Cargo plane (ID: {oldID}) changed ID to {newID}");
            }
            else if (PassengerPlaneInfo.ContainsKey(oldID))
            {
                var value = PassengerPlaneInfo[oldID];
                PassengerPlaneInfo.Remove(oldID);
                PassengerPlaneInfo.Add(newID, value);
                Logger.LogMessage($"Passenger plane (ID: {oldID}) changed ID to {newID}");
            }
            else if (CrewInfo.ContainsKey(oldID))
            {
                var value = CrewInfo[oldID];
                CrewInfo.Remove(oldID);
                CrewInfo.Add(newID, value);
                Logger.LogMessage($"Crew (ID: {oldID}) changed ID to {newID}");
            }
            else if (FlightInfo.ContainsKey(oldID))
            {
                var value = FlightInfo[oldID];
                FlightInfo.Remove(oldID);
                FlightInfo.Add(newID, value);
                Logger.LogMessage($"Flight (ID: {oldID}) changed ID to {newID}");
            }
            else if (PassengerInfo.ContainsKey(oldID))
            {
                var value = PassengerInfo[oldID];
                PassengerInfo.Remove(oldID);
                PassengerInfo.Add(newID, value);
                Logger.LogMessage($"Passenger (ID: {oldID}) changed ID to {newID}");
            }
           
            UsedIDs.Remove(oldID);
            UsedIDs.Add(newID);
        }
    }
}
