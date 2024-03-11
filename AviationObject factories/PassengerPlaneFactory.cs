using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class PassengerPlaneFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            return new PassengerPlane(
                UInt64.Parse(data[1]), 
                data[2], 
                data[3], 
                data[4], 
                UInt16.Parse(data[5]), 
                UInt16.Parse(data[6]), 
                UInt16.Parse(data[7]));
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16 ML = BitConverter.ToUInt16(data, 28);
            return new PassengerPlane(
                BitConverter.ToUInt64(data, 7),
                Encoding.ASCII.GetString(data, 15, 10).TrimEnd('\0'),
                Encoding.ASCII.GetString(data, 25, 3),
                Encoding.ASCII.GetString(data, 30, ML),
                BitConverter.ToUInt16(data, 30 + ML),
                BitConverter.ToUInt16(data, 32 + ML),
                BitConverter.ToUInt16(data, 34 + ML));
        }
    }
}
