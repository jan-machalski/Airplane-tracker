using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public abstract class Load:AviationObject
    {
        public Load() : base() { }
        public Load(UInt64 id, string type):base(id,type) { }
    }
}
