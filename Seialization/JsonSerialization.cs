using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace projekt_Jan_Machalski
{
    public class JsonSerialization:ISerialization
    {
        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            string json = JsonSerializer.Serialize(obj,new JsonSerializerOptions
            {
                WriteIndented = true 
            });
            File.WriteAllText(filePath, json);
        }
    }
}
