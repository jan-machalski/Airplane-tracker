using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class CargoPlaneFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return new CargoPlane(
                UInt64.Parse(data[1]), 
                data[2], 
                data[3], 
                data[4], 
                Single.Parse(data[5], provider));
        }
    }
}
