using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    [Serializable]
    public class PassengerPlane: Plane
    {
        public UInt16 FirstClassSize { get; set; }

        public UInt16 BuisnessClassSize { get; set; }

        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlane():base()
        {
            FirstClassSize = default(UInt16);
            BuisnessClassSize = default(UInt16);
            EconomyClassSize = default(UInt16);
        }
        public PassengerPlane(UInt64 id, string serial, string country, string model, UInt16 firstClassSize, UInt16 buisnessClassSize, UInt16 economyClassSize):base(id,"Passenger Plane", serial, country, model)    
        {
            FirstClassSize = firstClassSize;
            BuisnessClassSize = buisnessClassSize;
            EconomyClassSize = economyClassSize;
        }
    }
}
