using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Program
    {
        static void Main()
        {
            // getting lines from the example FTR file
            string filename = "example_data.ftr";
            FileReader FTRreader = new FileReader();
            List<string> data = FTRreader.ReadFromFile(filename);

            // creating list of aviation objects based on the list of strings
            List<AviationObject> aviationObjects = new List<AviationObject>();
            foreach(string s in data)
            {
                aviationObjects.Add(AviationObjectFactoryManager.CreateObject(s));
            }
            
            JsonSerialization json = new JsonSerialization();

            // json serialization straight into a file
            json.SerializeToFile(aviationObjects, "test.json");

            // json serialization into a string and then into a file
            string SerializationResult = json.Serialize(aviationObjects);
            File.WriteAllText("test2.json",SerializationResult);

            Console.WriteLine("Serialization complete");
        }
    }
}
