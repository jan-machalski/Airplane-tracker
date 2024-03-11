using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace projekt_Jan_Machalski
{
    public interface ISerialization
    {
        string Serialize<T>(T obj);
    }
}
