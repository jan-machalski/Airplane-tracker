using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Cargo: Load
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
        public Cargo(Dictionary<string,string> dic):this()
        {
            UpdateObject(dic, true);
        }
        public override Dictionary<string,string> GetInfoDictionary()
        {
            return new Dictionary<string, string>
            {
                {"ID",(this.ID).ToString() },
                {"Weight", this.Weight.ToString() },
                {"Code", this.Code },
                {"Description", this.Description}
            };
        }

        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            var valid = IsDictionaryValid(info);
            if (!valid.valid)
                throw new InvalidDataException(valid.info);
            UpdateID(info, newObject);
            if(info.ContainsKey("Weight"))
            {
                Single newWeight;
                ParseSingleField(info["Weight"], out newWeight, "weight");
                this.Weight = newWeight;
            }
            if(info.ContainsKey("Code"))
                this.Code = info["Code"];
            if(info.ContainsKey("Description"))
                this.Description = info["Description"];
        }
    }
}
