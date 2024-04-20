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
            Origin = null;
            Destination = null;
            TakeOffTime = string.Empty;
            LandingTime = string.Empty;
            Longitude = default(Single);
            Latitude = default(Single);
            AMSL = default(Single);
            Plane = null;
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
        public virtual (string takeOffTime,string landingTime) GetFlightTime()
        {
            return (this.TakeOffTime, this.LandingTime);
        }
        public virtual (Single Latitude,Single Longitude) GetOriginPos()
        {
            return (this.Origin.Latitude, this.Origin.Longitude);
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
