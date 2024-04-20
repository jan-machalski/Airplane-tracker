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
    }
}
