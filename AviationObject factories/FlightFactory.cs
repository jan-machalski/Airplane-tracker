using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class FlightFactory:AviationObjectFactory
    {
        Database database = Database.Instance;
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            var newObject = new Flight(
                UInt64.Parse(data[1]),
                UInt64.Parse(data[2]),
                UInt64.Parse(data[3]),
                data[4],
                data[5],
                Single.Parse(data[6],provider),
                Single.Parse(data[7],provider),
                Single.Parse(data[8],provider),
                UInt64.Parse(data[9]),
                data[10].Trim('[', ']').Split(';').Select(UInt64.Parse).ToArray(),
                data[11].Trim('[', ']').Split(';').Select(UInt64.Parse).ToArray());
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            AviationObjectFactoryManager manager = AviationObjectFactoryManager.Instance;
            UInt64 originID = BitConverter.ToUInt64(data, 15);
            UInt16 CC = BitConverter.ToUInt16(data, 55);
            UInt16 PCC = BitConverter.ToUInt16(data, 57 + 8 * CC);
            UInt64[] crew = new UInt64[CC];
            UInt64[] load = new UInt64[PCC];
            long takeoff_time_milliseconds = BitConverter.ToInt64(data, 31);
            long landing_time_milliseconds = BitConverter.ToInt64(data, 39);
            DateTimeOffset TakeoffTime = DateTimeOffset.FromUnixTimeMilliseconds(takeoff_time_milliseconds);
            DateTimeOffset LandingTime = DateTimeOffset.FromUnixTimeMilliseconds(landing_time_milliseconds);

            for (int i = 0; i<CC;i++)
                crew[i] = BitConverter.ToUInt64(data, 57+8*i);

            for(int i = 0; i< PCC;i++)
                load[i] = BitConverter.ToUInt64(data,59 + 8*CC + 8*i);

            var newObject = new Flight(
                BitConverter.ToUInt64(data, 7),
                originID,
                BitConverter.ToUInt64(data,23),
                $"{TakeoffTime.Hour:D2}:{TakeoffTime.Minute:D2}",
                $"{LandingTime.Hour:D2}:{LandingTime.Minute:D2}",
                database.AirportInfo[originID].Longitude,
                database.AirportInfo[originID].Latitude,
                database.AirportInfo[originID].AMSL,
                BitConverter.ToUInt64(data,47),
                crew,
                load);
            database.AddObject(newObject);
            return newObject;
        }
    }
}
