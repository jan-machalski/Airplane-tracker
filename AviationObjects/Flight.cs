using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Flight: AviationObject
    {
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Single AMSL { get; set; }
        public Plane Plane { get; set; }
        public Crew[] Crew {  get; set; }
        public Load[] Load { get; set; }
        public Flight():base()
        {
            Database database = Database.Instance;
            Origin = database.AirportInfo.First().Value;
            Destination = database.AirportInfo.First().Value;
            TakeOffTime = string.Empty;
            LandingTime = string.Empty;
            Longitude = default(Single);
            Latitude = default(Single);
            AMSL = default(Single);
            Plane = database.PassengerPlaneInfo.First().Value;
            Crew = null;
            Load = null;
        }
        public Flight(UInt64 id,UInt64 originID, UInt64 targetID, string takeOffTime, string landingTime, Single longitude, Single latitude, Single amsl, UInt64 planeID, UInt64[] crewIDs, UInt64[] loadIDs):base(id,"Flight")
        {
            Database database = Database.Instance;
            Origin = database.AirportInfo[originID];
            Destination = database.AirportInfo[targetID];
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            if(database.PassengerPlaneInfo.ContainsKey(planeID))
                Plane = database.PassengerPlaneInfo[planeID];
            else
                Plane = database.CargoPlaneInfo[planeID];
            Crew = new Crew[crewIDs.Length];
            for(int i = 0;i<crewIDs.Length;i++) { Crew[i] = database.CrewInfo[crewIDs[i]]; }
            Load = new Load[loadIDs.Length];
            for(int i = 0; i<loadIDs.Length;i++)
            {
                if (database.PassengerInfo.ContainsKey(loadIDs[i]))
                    Load[i] = database.PassengerInfo[loadIDs[i]];
                else
                    Load[i] = database.CargoInfo[loadIDs[i]];
            }
        }
        public Flight(Flight flight):base(flight.ID,"Flight")
        {
            this.Origin = flight.Origin;
            this.Destination = flight.Destination;
            this.Latitude = flight.Latitude;
            this.Longitude = flight.Longitude;
            this.LandingTime = flight.LandingTime;
            this.TakeOffTime = flight.TakeOffTime;
            this.AMSL = flight.AMSL;
            this.Plane = flight.Plane;
            this.Crew = new Crew[flight.Crew.Length];
            for(int i = 0;i<Crew.Length;i++)
                this.Crew[i] = flight.Crew[i];
            this.Load = new Load[flight.Load.Length];
            for (int i = 0; i < this.Load.Length; i++)
                this.Load[i] = flight.Load[i];
        }
        public Flight(Dictionary<string,string> dic):this()
        {
            UpdateObject(dic);
        }
        public virtual (string takeOffTime,string landingTime) GetFlightTime()
        {
            return (this.TakeOffTime, this.LandingTime);
        }
        public virtual (Single Latitude,Single Longitude) GetOriginPos()
        {
            return (this.Origin.Latitude, this.Origin.Longitude);
        }
        public override Dictionary<string, string> GetInfoDictionary()
        {
            var result = new Dictionary<string, string>()
            {
                {"ID", this.ID.ToString()},
                {"Origin",this.Origin.ToString() },
                {"Target",this.Destination.ToString()},
                { "WorldPosition","{" + Longitude.ToString() + "," + Latitude.ToString() + "}" },
                { "WorldPosition.Long",Longitude.ToString() },
                { "WorldPosition.Lat", Latitude.ToString() },
                {"AMSL", this.AMSL.ToString() },
                {"Plane", this.Plane.ToString() },
                {"TakeOffTime", this.TakeOffTime.ToString() },
                {"LandingTime", this.LandingTime.ToString() }
            };
            foreach(var s in Origin.GetInfoDictionary())
            {
                result.Add("Origin." + s.Key, s.Value);
            }
            foreach(var s in Destination.GetInfoDictionary())
            {
                result.Add("Target."+s.Key, s.Value);
            }
            foreach(var s in Plane.GetInfoDictionary())
            {
                result.Add("Plane." + s.Key, s.Value);
            }
            return result;
        }
        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            Database database = Database.Instance;
            var valid = IsDictionaryValid(info);
            if (!valid.valid)
                throw new ArgumentException(valid.info);
            UpdateID(info, newObject);
            if(info.ContainsKey("TakeOffTime") && DateTime.TryParseExact(info["TakeOffTime"],"HH:mm",CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                this.TakeOffTime = info["TakeOffTime"];
            if (info.ContainsKey("LandingTime") && DateTime.TryParseExact(info["LandingTime"], "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                this.LandingTime = info["LandingTime"];
            if(info.ContainsKey("ASML"))
            {
                Single newASML;
                ParseSingleField(info["ASML"], out newASML, "ASML");
                this.AMSL = newASML;
            }
            if(info.ContainsKey("Origin.ID"))
            {
                UInt64 newID;
                ParseUIntField(info["Origin.ID"], out newID, "Origin.ID");
                if (database.AirportInfo.ContainsKey(newID))
                    this.Origin = database.AirportInfo[newID];
                else
                    throw new ArgumentException($"No airport wit ID={newID} in the database");
            }
            else
            {
                foreach (var p in info.Keys)
                    if (p.StartsWith("Origin."))
                        throw new ArgumentException("Unable to modify the airport parameters by updating flight");
            }
            if (info.ContainsKey("Target.ID"))
            {
                UInt64 newID;
                ParseUIntField(info["Target.ID"], out newID, "Target.ID");
                if (database.AirportInfo.ContainsKey(newID))
                    this.Destination = database.AirportInfo[newID];
                else
                    throw new ArgumentException($"No airport wit ID={newID} in the database");
            }
            else
            {
                foreach (var p in info.Keys)
                    if (p.StartsWith("Target."))
                        throw new ArgumentException("Unable to modify the airport parameters by updating flight");
            }
            if (info.ContainsKey("Plane.ID"))
            {
                UInt64 newID;
                ParseUIntField(info["Plane.ID"], out newID, "Plane.ID");
                if (database.PassengerPlaneInfo.ContainsKey(newID))
                    this.Plane = database.PassengerPlaneInfo[newID];
                else if(database.CargoPlaneInfo.ContainsKey(newID))
                    this.Plane = database.CargoPlaneInfo[newID];
                else
                    throw new ArgumentException($"No airport wit ID={newID} in the database");
            }
            else
            {
                foreach (var p in info.Keys)
                    if (p.StartsWith("Plane."))
                        throw new ArgumentException("Unable to modify the airport parameters by updating flight");
            }
            if (info.ContainsKey("WorldPosition"))
            {
                int lonFrom = info["WorldPosition"].IndexOf("{") + 1;
                int lonTo = info["WorldPosition"].IndexOf(",");
                string longitude = info["WorldPosition"].Substring(lonFrom, lonTo - lonFrom);

                int latFrom = lonFrom + 1;
                int latTo = info["WorldPosition"].IndexOf("}");
                string latitude = info["WorldPosition"].Substring(latFrom, latTo - latFrom);
                Single newLongitude, newLatitude;
                ParseSingleField(longitude, out newLongitude, "longitude");

                ParseSingleField(latitude, out newLatitude, "latitude");
                database.UpdateFlightPos(this.ID, newLongitude, newLatitude,this.AMSL);
            }
            else
            {
                double newLongitude = this.Longitude;
                double newLatitude = this.Latitude;
                if (info.ContainsKey("WorldPosition.Long"))
                {
                    ParseDoubleField(info["WorldPosition.Long"], out newLongitude, "longitude");
                    this.Longitude = newLongitude;
                }
                if (info.ContainsKey("WorldPosition.Lat"))
                {
                    ParseDoubleField(info["WorldPosition.Lat"], out newLatitude, "latitude");
                    this.Latitude = newLatitude;
                }
                database.UpdateFlightPos(this.ID, (float)newLongitude, (float)newLatitude, this.AMSL);
            }
        }
    }
    public class FlightWithUpdatedPos : Flight
    {
        private string changeTime;
        private Single newLongitude;
        private Single newLatitude;

        public FlightWithUpdatedPos(Flight flight,Single longitude,Single Latitude):base(flight)
        {
            DateTime currentDate = DateTime.Now;
            changeTime = $"{currentDate:HH:mm}";
            newLatitude = Latitude;
            newLongitude = longitude;
        }
        public override (string takeOffTime, string landingTime) GetFlightTime()
        {
            return (changeTime,base.LandingTime);
        }
        public override (float Latitude, float Longitude) GetOriginPos()
        {
            return (newLatitude,newLongitude);
        }
    }
}
