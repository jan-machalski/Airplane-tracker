using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projekt_Jan_Machalski;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class PassengerPlane: Plane,IReportable
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
        public PassengerPlane(Dictionary<string,string> dic):this()
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
                { "ID", this.ID.ToString() },
                { "Serial", this.Serial.ToString() },
                { "CountryCode", this.Country.ToString() },
                { "Model", this.Model.ToString() },
                { "FirstClassSize", this.FirstClassSize.ToString() },
                { "BuisnessClassSize", this.BuisnessClassSize.ToString() },
                { "EconomyClassSize", this.EconomyClassSize.ToString() },
            };
        }
        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            UInt16 newSize;
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
            if(info.ContainsKey("FirstClassSize"))
            {
                ParseShortField(info["FirstClassSize"], out newSize, "FirstClassSize");
                this.FirstClassSize = newSize;
            }
            if(info.ContainsKey("BuisnessClassSize"))
            {
                ParseShortField(info["BuisnessClassSize"], out newSize, "BuisnessClassSize");
                this.BuisnessClassSize = newSize;
            }
            if(info.ContainsKey("EconomyClassSize"))
            {
                ParseShortField(info["EconomyClassSize"], out newSize, "EconomyClassSize");
                this.EconomyClassSize = newSize;
            }
          
        }
    }
}
