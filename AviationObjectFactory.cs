using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public abstract class AviationObjectFactory
    {
        public abstract AviationObject CreateAviationObject(List<string> data);
    }
}
