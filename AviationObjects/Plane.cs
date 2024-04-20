using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace projekt_Jan_Machalski
{
    public abstract class Plane : AviationObject
    {
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public Flight Flight { get; set; }

        public Plane():base()
        {
            Serial = string.Empty;
            Country = string.Empty;
            Model = string.Empty;
            Flight = null;
        }
        public Plane(UInt64 id,string objectType, string serial, string country, string model):base(id,objectType)
        {
            Serial = serial;
            Country = country;
            Model = model;
            Flight = null;
        }
    }
}
