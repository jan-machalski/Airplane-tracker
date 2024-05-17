using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Crew: AviationObject
    {
        public string Name { get; set; }
        public UInt64 Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public UInt16 Practice { get; set; }
        public string Role { get; set; }
        public Crew():base()
        {
            Name = string.Empty;
            Age = default(UInt64);
            Phone = string.Empty;
            Email = string.Empty;
            Role = string.Empty;
            Practice = default(UInt16);
        }
        public Crew(UInt64 id,string name, UInt64 age, string phone, string email, UInt16 practice, string role):base(id,"Crew")
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }
        public Crew(Dictionary<string,string> dic):this()
        {
            UpdateObject(dic);
        }
        public override Dictionary<string, string> GetInfoDictionary()
        {
            return new Dictionary<string, string>
            {
                {"ID", this.ID.ToString()},
                { "Name", this.Name },
                { "Age", this.Age.ToString() },
                { "Phone", this.Phone },
                { "Email", this.Email },
                {"Practice", this.Practice.ToString() },
                {"Role", this.Role }
            };
        }
        public override void UpdateObject(Dictionary<string, string> info, bool newObject = false)
        {
            var valid = IsDictionaryValid(info);
            if (!valid.valid)
                throw new InvalidDataException(valid.info);
            UpdateID(info, newObject);
            if (info.ContainsKey("Name"))
                this.Name = info["Name"];
            if (info.ContainsKey("Age"))
            {
                UInt64 newAge;
                ParseUIntField(info["Age"], out newAge, "Age");
                this.Age = newAge;
            }
            if (info.ContainsKey("Phone"))
                this.Phone = info["Phone"];
            if (info.ContainsKey("Email"))
                this.Email = info["Email"];
            if (info.ContainsKey("Role"))
                this.Role = info["Role"];
            if(info.ContainsKey("Practice"))
            {
                UInt64 newPractice;
                ParseUIntField(info["Practice"], out newPractice, "practice");
                this.Practice = (UInt16)newPractice;
            }
        }
    }
}
