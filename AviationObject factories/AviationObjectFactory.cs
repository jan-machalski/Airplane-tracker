using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public abstract class AviationObjectFactory
    {
        public abstract AviationObject CreateAviationObject(List<string> data);
        public abstract AviationObject CreateAviationObject(byte[] data);
    }
}
