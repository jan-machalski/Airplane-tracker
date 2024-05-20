using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class CargoPlaneFactory:AviationObjectFactory
    {
        Database database = Database.Instance;
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            var newObject = new CargoPlane(
                UInt64.Parse(data[1]), 
                data[2], 
                data[3], 
                data[4], 
                Single.Parse(data[5], provider));
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16 ML = BitConverter.ToUInt16(data, 28);
            var newObject = new CargoPlane(
                BitConverter.ToUInt64(data, 7),
                Encoding.ASCII.GetString(data, 15, 10).TrimEnd('\0'),
                Encoding.ASCII.GetString(data, 25, 3),
                Encoding.ASCII.GetString(data, 30, ML),
                BitConverter.ToSingle(data, 30 + ML));
            database.AddObject(newObject);
            return newObject;
        }
        public override AviationObject CreateAviationObject(Dictionary<string, string> data)
        {
            var newObject = new CargoPlane(data);
            database.AddObject(newObject);
            return newObject;
        }

    }
}
