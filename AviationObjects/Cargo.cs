using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Cargo: AviationObject
    {
        public Single Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Cargo():base()
        {
            Weight = default(Single);
            Code = string.Empty;
            Description = string.Empty;

        }
        public Cargo(UInt64 id,  Single weight, string code, string description):base(id,"Cargo")
        {
            Weight = weight;
            Code = code;
            Description = description;
        }
    }
}
