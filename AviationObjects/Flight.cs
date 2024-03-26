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
        public UInt64 OriginID { get; set; }
        public UInt64 TargetID { get; set; }
        public string TakeOffTime { get; set; }
        public string LandingTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewIDs { get; set; }
        public UInt64[] LoadIDs { get; set; }
        public Flight():base()
        {
            OriginID = default(UInt64);
            TargetID = default(UInt64);
            TakeOffTime = string.Empty;
            LandingTime = string.Empty;
            Longitude = default(Single);
            Latitude = default(Single);
            AMSL = default(Single);
            PlaneID = default(UInt64);
            CrewIDs = new UInt64[0];
            LoadIDs = new UInt64[0];
        }
        public Flight(UInt64 id,UInt64 originID, UInt64 targetID, string takeOffTime, string landingTime, Single longitude, Single latitude, Single amsl, UInt64 planeID, UInt64[] crewIDs, UInt64[] loadIDs):base(id,"Flight")
        {
            OriginID = originID;
            TargetID = targetID;
            TakeOffTime = takeOffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            PlaneID = planeID;
            CrewIDs = crewIDs;
            LoadIDs = loadIDs;
        }
    }
}
