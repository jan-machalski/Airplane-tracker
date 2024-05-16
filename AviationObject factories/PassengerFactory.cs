using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class PassengerFactory: AviationObjectFactory
    {
        Database database = Database.Instance;
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var newObject = new Passenger(
                UInt64.Parse(data[1]), 
                data[2], 
                UInt64.Parse(data[3]), 
                data[4], 
                data[5], 
                data[6], 
                UInt64.Parse(data[7]));
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16 NL = BitConverter.ToUInt16(data, 15);
            UInt16 EL = BitConverter.ToUInt16(data, 31 + NL);
            var newObject = new Passenger(
                BitConverter.ToUInt64(data, 7),
                Encoding.ASCII.GetString(data, 17, NL),
                BitConverter.ToUInt16(data, 17 + NL),
                Encoding.ASCII.GetString(data, 19 + NL, 12),
                Encoding.ASCII.GetString(data, NL + 33, EL),
                Encoding.ASCII.GetString(data, 34 + NL + EL, 1),
                BitConverter.ToUInt64(data,34+NL+EL));
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

    }
}
