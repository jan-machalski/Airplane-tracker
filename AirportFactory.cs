using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class AirportFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return new Airport(
                UInt64.Parse(data[1]), 
                data[2], 
                data[3], 
                Single.Parse(data[4], provider), 
                Single.Parse(data[5], provider), 
                Single.Parse(data[6], provider), 
                data[7]);
        }
    }
}
