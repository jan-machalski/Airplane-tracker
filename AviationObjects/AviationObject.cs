using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;


namespace projekt_Jan_Machalski
{
    [Serializable]
    [JsonDerivedType(typeof(Airport),"Airport")]
    [JsonDerivedType(typeof(Cargo),"Cargo")]
    [JsonDerivedType(typeof(CargoPlane),"CargoPlane")]
    [JsonDerivedType(typeof(Crew),"Crew")]
    [JsonDerivedType(typeof(Flight),"Flight")]
    [JsonDerivedType(typeof(Passenger),"Passenger")]
    [JsonDerivedType(typeof(PassengerPlane),"PassengerPlane")]
    public abstract class AviationObject
    {
        public UInt64 ID { get; set; }
        public static string? ObjectType;

        public AviationObject() 
        {
            ID = default(UInt64);
            ObjectType = string.Empty;
        }
        public AviationObject(UInt64 id, string type)
        {
            ID = id;
            ObjectType = type;
        }
        public virtual Dictionary<string,string> GetInfoDictionary()
        {
            return null;
        }
        public virtual void UpdateObject(Dictionary<string,string> info, bool newObject = false) { }
        public (bool valid, string info) IsDictionaryValid(Dictionary<string, string> dic)
        {
            var validDic = this.GetInfoDictionary();
            foreach (var p in dic)
            {
                if (!validDic.ContainsKey(p.Key))
                {
                    return (false, $"invalid variable name: {p.Key}");
                }
            }
            return (true, "");
        }
        public void UpdateID(Dictionary<string,string> info,bool newObject)
        {
            if (info.ContainsKey("ID"))
            {
                UInt64 newId;

                if (!UInt64.TryParse(info["ID"], out newId))
                {
                    throw new ArgumentException($"invalid id value: {info["ID"]}");
                }
                if (!newObject)
                {
                    var database = Database.Instance;
                    database.ChangeID(this.ID, newId);
                    this.ID = newId;
                }
                this.ID = newId;
            }
        }
        public void ParseSingleField(string value, out Single field, string fieldName)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            if(!Single.TryParse(value,out field))
            {
                throw new ArgumentException($"unable to parse the value of {fieldName}: {value}");
            }
        }
        public void ParseUIntField(string value, out UInt64 field, string fieldName)
        {
            if(!UInt64.TryParse(value, out field))
            {
                throw new ArgumentException($"unable to parse the value of {fieldName}: {value}");
            }
        }
        public void ParseShortField(string value, out UInt16 field, string fieldName)
        {
            if (!UInt16.TryParse(value, out field))
            {
                throw new ArgumentException($"unable to parse the value of {fieldName}: {value}");
            }
        }
        public void ParseDoubleField(string value, out double field, string fieldName)
        {
            if (!double.TryParse(value, out field))
            {
                throw new ArgumentException($"unable to parse the value of {fieldName}: {value}");
            }
        }

    }
}
