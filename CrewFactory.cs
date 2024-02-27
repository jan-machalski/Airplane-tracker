using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class CrewFactory: AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            return new Crew(
                UInt64.Parse(data[1]), 
                data[2], 
                UInt64.Parse(data[3]), 
                data[4], 
                data[5], 
                UInt16.Parse(data[6]), 
                data[7]);
        }
    }
}
