using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class PassengerFactory: AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            return new Passenger(
                UInt64.Parse(data[1]), 
                data[2], 
                UInt64.Parse(data[3]), 
                data[4], 
                data[5], 
                data[6], 
                UInt64.Parse(data[7]));
        }
    }
}
