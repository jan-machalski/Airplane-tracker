using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class CargoFactory:AviationObjectFactory
    {
        Database database = Database.Instance;
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            var newObject = new Cargo(
                UInt64.Parse(data[1]), 
                Single.Parse(data[2],provider), 
                data[3], 
                data[4]);
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16 DL = BitConverter.ToUInt16(data, 25);
            var newObject = new Cargo(
                BitConverter.ToUInt64(data, 7),
                BitConverter.ToSingle(data, 15),
                Encoding.ASCII.GetString(data, 19, 6),
                Encoding.ASCII.GetString(data, 27, DL)
                );
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(Dictionary<string, string> data)
        {
            var newObject = new Cargo(data);
            database.AddObject(newObject);
            return newObject;
        }

    }
}
