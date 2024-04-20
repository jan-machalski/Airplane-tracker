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
    }
}
