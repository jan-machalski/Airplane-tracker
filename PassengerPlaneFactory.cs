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
    }
}
