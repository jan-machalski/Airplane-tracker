using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class FlightFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return new Flight(
                UInt64.Parse(data[1]),
                UInt64.Parse(data[2]),
                UInt64.Parse(data[3]),
                data[4],
                data[5],
                Single.Parse(data[6],provider),
                Single.Parse(data[7],provider),
                Single.Parse(data[8],provider),
                UInt64.Parse(data[9]),
                data[9].Trim('[', ']').Split(';').Select(UInt64.Parse).ToArray(),
                data[10].Trim('[', ']').Split(';').Select(UInt64.Parse).ToArray());
        }
    }
}
