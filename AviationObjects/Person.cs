using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public abstract class Person: AviationObject
    {
        public string Name { get; set; }

        public UInt64 Age { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Person():base()
        {
            Name = string.Empty;
            Age = default(UInt64);
            Phone = string.Empty;
            Email = string.Empty;
        }
        public Person (UInt64 id, string objectType,string name, UInt64 age, string phone, string email):base(id,objectType)
        {
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
        }
    }
}
