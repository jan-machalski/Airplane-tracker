using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class CargoFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            var newObject = new Cargo(
                UInt64.Parse(data[1]), 
                Single.Parse(data[2],provider), 
                data[3], 
                data[4]);
            AddToInfoDictionary(newObject);
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
            AddToInfoDictionary(newObject);
            return newObject;
        }

        public void AddToInfoDictionary(Cargo cargo)
        {
            AviationObjectFactoryManager manager = AviationObjectFactoryManager.Instance;
            if (!manager.CargoInfo.ContainsKey(cargo.ID))
            {
                manager.CargoInfo.Add(cargo.ID, cargo);
            }
        }
    }
}
