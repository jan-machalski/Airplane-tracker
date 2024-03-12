using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public static string ObjectType;

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

       
    }
}
