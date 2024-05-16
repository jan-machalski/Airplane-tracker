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
        public Airport(Dictionary<string, string> dic)
        {
            Name = string.Empty;
            Code = string.Empty;
            Longitude = default(Single);
            Latitude = default(Single);
            AMSL = default(Single);
            Country = string.Empty;
            this.UpdateObject(dic);
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
                {"WorldPosition.Longitude",Longitude.ToString() },
                {"WorldPosition.Latitude", Latitude.ToString() },
                {"ASML",this.AMSL.ToString()},
                {"CountryCode", this.Country }
            };
        }
        public override void UpdateObject(Dictionary<string, string> info)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            var valid = IsDictionaryValid(info);
            if(!valid.valid)
                throw new InvalidDataException(valid.info);
            if(info.ContainsKey("ID"))
            {
                UInt64 newId;
                
                if (!UInt64.TryParse(info["ID"],out newId))
                {
                    throw new InvalidDataException($"invalid id value: {info["Id"]}");
                }
                var database = Database.Instance;
                database.ChangeID(this.ID, newId);
                this.ID = newId;
            }
            if(info.ContainsKey("Name"))
            {
                this.Name = info["Name"];
            }
            if(info.ContainsKey("WorldPosition"))
            {
                int lonFrom = info["WorldPosition"].IndexOf("{") + 1;
                int lonTo = info["worldPosition"].IndexOf(",");
                string longitude = info["WorldPosition"].Substring(lonFrom, lonTo - lonFrom);

                int latFrom = lonFrom + 1;
                int latTo = info["WorldPosition"].IndexOf("}");
                string latitude = info["WorldPosition"].Substring(latFrom,latTo - latFrom);
                float newLongitude,newLatitude;
                if(!Single.TryParse(longitude,provider,out newLongitude))
                {
                    throw new InvalidDataException($"invalid longitude: {longitude}");
                }
                this.Longitude = (Single)newLongitude;
                if(!Single.TryParse(latitude,provider,out newLatitude))
                {
                    throw new InvalidDataException($"invalid latitude: {latitude}");
                }
                this.Latitude = (Single)newLatitude;
            }
            else
            {
                if(info.ContainsKey("Longitude"))
                {
                    float newLongitude;
                    if (!Single.TryParse(info["Longitude"], provider, out newLongitude))
                        throw new InvalidDataException($"invalid longitude: {info["Longitude"]}");
                    this.Longitude= (Single)newLongitude;
                }if(info.ContainsKey("Latitude"))
                {
                    float newLatitude;
                    if (!Single.TryParse(info["Latitude"], provider, out newLatitude))
                        throw new InvalidDataException($"invalid latitude: {info["Latitude"]}");
                    this.Latitude= (Single)newLatitude;
                }
            }
            if(info.ContainsKey("ASML"))
            {
                float newASML;
                if (!Single.TryParse(info["ASML"], provider, out newASML))
                    throw new InvalidDataException("invalid ASML");
                this.AMSL = (Single)newASML;
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
