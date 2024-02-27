using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace projekt_Jan_Machalski
{
    [Serializable]
    public class Passenger: Person
    {

        public string Class { get; set; }
        public UInt64 Miles { get; set; }

        public Passenger():base()
        {
            Class = string.Empty;
            Miles = default(UInt64);
        }
        public Passenger(UInt64 id, string name, UInt64 age, string phone, string email, string passenger_class,UInt64 miles):base(id, name, age, phone, email)
        {
            Class = passenger_class;
            Miles = miles;
        }
    }
}
