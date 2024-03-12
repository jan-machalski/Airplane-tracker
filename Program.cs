using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NetworkSourceSimulator;

namespace projekt_Jan_Machalski
{
    public class Program
    {
        static void Main()
        {
            string FTR_file_path = "example_data.ftr";
            int min_offset = 10;
            int max_offset = 50;
            string json_filepath = "test.json";

            //testing stage 1
            List<AviationObject> aviationObjects = new List<AviationObject>();
            List<string> FTR_data = FileReader.ReadFromFile(FTR_file_path);
            foreach (string s in FTR_data)
            {
                aviationObjects.Add(AviationObjectFactoryManager.CreateObject(s));
            }
            JsonSerialization.SerializeToFile(aviationObjects, json_filepath);



            //testing stage 2
            NSS_Simulation simulation = new NSS_Simulation(FTR_file_path, min_offset, max_offset);

            simulation.RunSimulation();         
        }



    }
}



