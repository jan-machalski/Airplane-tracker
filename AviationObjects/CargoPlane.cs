using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projekt_Jan_Machalski;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class CargoPlane: Plane,IReportable
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
        public CargoPlane(Dictionary<string,string> dic):this()
        {
            UpdateObject(dic);
        }
        public string Report(IMedia media)
        {
            return media.Report(this);
        }
        public override Dictionary<string, string> GetInfoDictionary()
        {
            return new Dictionary<string, string>
            {
                {"ID", this.ID.ToString() },
                {"Serial",this.Serial.ToString()},
                {"CountryCode", this.Country.ToString()},
                {"Model", this.Model.ToString()},
                {"MaxLoad", this.MaxLoad.ToString()},
            };
        }
        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            var valid = IsDictionaryValid(info);
            if (!valid.valid)
                throw new ArgumentException(valid.info);
            UpdateID(info, newObject);
            if (info.ContainsKey("Serial"))
                this.Serial = info["Serial"];
            if (info.ContainsKey("CountryCode"))
                this.Country = info["CountryCode"];
            if (info.ContainsKey("Model"))
                this.Model = info["Model"];
            if(info.ContainsKey("MaxLoad"))
            {
                Single newMaxLoad;
                ParseSingleField(info["MaxLoad"], out newMaxLoad, "MaxLoad");
            }
        }
    }
}
