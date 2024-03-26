using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class CargoPlane: Plane
    {
        public Single MaxLoad { get; set; }
        public CargoPlane():base()
        {
            MaxLoad = default(Single);
        }
        public CargoPlane(UInt64 id, string serial, string country, string model, Single maxLoad):base(id,"Cargo Plane",serial,country,model)
        {
            MaxLoad = maxLoad;
        }
    }
}
