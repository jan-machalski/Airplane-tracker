using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    [Serializable]
    [JsonDerivedType(typeof(Airport), typeDiscriminator: "Airport")]
    [JsonDerivedType(typeof(Cargo), typeDiscriminator: "Cargo")]
    [JsonDerivedType(typeof(CargoPlane), typeDiscriminator: "CargoPlane")]
    [JsonDerivedType(typeof(Crew), typeDiscriminator: "Crew")]
    [JsonDerivedType(typeof(Flight), typeDiscriminator: "Flight")]
    [JsonDerivedType(typeof(Passenger), typeDiscriminator: "Passenger")]
    [JsonDerivedType(typeof(PassengerPlane), typeDiscriminator: "PassengerPlane")]
    public abstract class AviationObject
    {
        public UInt64 ID { get; set; }

        public AviationObject() 
        {
            ID = default(UInt64);
        }
        public AviationObject(UInt64 id)
        {
            ID = id;
        }

       
    }
}
