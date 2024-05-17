using ExCSS;
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
        public Airport(Dictionary<string, string> dic):this()
        {
            this.UpdateObject(dic,true);
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
        public override Dictionary<string, string> GetInfoDictionary()
        {
            return new Dictionary<string, string>
            {
                {"ID", this.ID.ToString()},
                {"Name", this.Name},
                {"Code", this.Code},
                {"WorldPosition","{"+Longitude.ToString()+","+Latitude.ToString()+"}" },
                {"WorldPosition.Long",Longitude.ToString() },
                {"WorldPosition.Lat", Latitude.ToString() },
                {"ASML",this.AMSL.ToString()},
                {"CountryCode", this.Country }
            };
        }
        public override void UpdateObject(Dictionary<string, string> info,bool newObject = false)
        {
            var valid = IsDictionaryValid(info);
            if(!valid.valid)
                throw new InvalidDataException(valid.info);
            UpdateID(info, newObject);
            if(info.ContainsKey("Name"))
            {
                this.Name = info["Name"];
            }
            if(info.ContainsKey("WorldPosition"))
            {
                int lonFrom = info["WorldPosition"].IndexOf("{") + 1;
                int lonTo = info["WorldPosition"].IndexOf(",");
                string longitude = info["WorldPosition"].Substring(lonFrom, lonTo - lonFrom);

                int latFrom = lonFrom + 1;
                int latTo = info["WorldPosition"].IndexOf("}");
                string latitude = info["WorldPosition"].Substring(latFrom,latTo - latFrom);
                Single newLongitude,newLatitude;
                ParseSingleField(longitude, out newLongitude, "longitude");
                this.Longitude = newLongitude;

                ParseSingleField(latitude,out newLatitude, "latitude");
                this.Latitude = newLatitude;
            }
            else
            {
                if(info.ContainsKey("WorldPosition.Long"))
                {
                    Single newLongitude;
                    ParseSingleField(info["WorldPosition.Lon"], out newLongitude, "longitude");
                    this.Longitude= newLongitude;
                }if(info.ContainsKey("WorldPosition.Lat"))
                {
                    Single newLatitude;
                    ParseSingleField(info["WorldPosition.Lat"], out newLatitude, "latitude");
                    this.Latitude= newLatitude;
                }
            }
            if(info.ContainsKey("ASML"))
            {
                Single newASML;
                ParseSingleField(info["ASML"],out newASML, "ASML");
                this.AMSL = newASML;
            }
            if (info.ContainsKey("CountryCode"))
                this.Country = info["CountryCode"];
        } 

        public string Report(IMedia media)
        {
            return media.Report(this);
        }
        
    }
}
