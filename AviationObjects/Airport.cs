﻿using ExCSS;
using projekt_Jan_Machalski;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Airport: AviationObject, IReportable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public string Country { get; set; }
        public Airport():base() 
        {
            Name = string.Empty;
            Code = string.Empty;
            Longitude = default(Single);
            Latitude = default(Single);
            AMSL = default(Single);
            Country = string.Empty;
        }
        public Airport(UInt64 id,string name, string code, Single longitude, Single latitude, Single amsl, string country):base(id,"Airport")
        {
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = amsl;
            Country = country;
        }
        public override string Accept(IMedia media)
        {
            return media.Report(this);
        }
    }
}
