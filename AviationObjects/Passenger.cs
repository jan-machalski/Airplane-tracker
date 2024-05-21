using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Passenger: Load
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger():base()
        {
            Name = string.Empty;
            Age = default(UInt64);
            Phone = string.Empty;
            Email = string.Empty;
            Class = string.Empty;
            Miles = default(UInt64);
        }
        public Passenger(UInt64 id, string name, UInt64 age, string phone, string email, string passenger_class,UInt64 miles):base(id,"Passenger")
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = passenger_class;
            Miles = miles;
        }
        public Passenger(Dictionary<string,string> dic):this()
        {
            UpdateObject(dic);
        }
        public override Dictionary<string, string> GetInfoDictionary()
        {
            return new Dictionary<string, string>
            {
                { "ID", this.ID.ToString() },
                { "Name", this.Name },
                { "Age", this.Age.ToString() },
                { "Phone", this.Phone },
                { "Email", this.Email },
                { "Class", this.Class },
                { "Miles", this.Miles.ToString() }
            };
        }
        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            var valid = IsDictionaryValid(info);
            if (!valid.valid)
                throw new ArgumentException(valid.info);
            UpdateID(info, newObject);
            if(info.ContainsKey("Name"))
                this.Name = info["Name"];
            if(info.ContainsKey("Age"))
            {
                UInt64 newAge;
                ParseUIntField(info["Age"], out newAge, "Age");
                this.Age = newAge;
            }
            if (info.ContainsKey("Phone"))
                this.Phone = info["Phone"];
            if (info.ContainsKey("Email"))
                this.Email = info["Email"];
            if (info.ContainsKey("Class"))
                this.Class = info["Class"];
            if(info.ContainsKey("Miles"))
            {
                UInt64 newMiles;
                ParseUIntField(info["Miles"], out newMiles, "miles");
                this.Miles = newMiles;
            }
        }

    }
}
