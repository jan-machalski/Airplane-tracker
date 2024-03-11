using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Crew: Person
    {
        public UInt16 Practice { get; set; }
        public string Role { get; set; }
        public Crew():base()
        {
            Role = string.Empty;
            Practice = default(UInt16);
        }
        public Crew(UInt64 id,string name, UInt64 age, string phone, string email, UInt16 practice, string role):base(id,"Crew",name,age,phone,email)
        {
            Practice = practice;
            Role = role;
        }
    }
}
