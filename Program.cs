using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using FlightTrackerGUI;
using NetworkSourceSimulator;

namespace projekt_Jan_Machalski
{
    public class Program
    {
        static void Main()
        {
            TestWithStage1data();
            Database database = Database.Instance;
            
        }

        static void TestWithStage1data()
        {
            string FTR_file_path = "example_data.ftr";
            string json_filepath = "test.json";
            AviationObjectFactoryManager manager = AviationObjectFactoryManager.Instance;

            //testing stage 1
            List<AviationObject> aviationObjects = new List<AviationObject>();
            List<string> FTR_data = FileReader.ReadFromFile(FTR_file_path);
            foreach (string s in FTR_data)
            {
                aviationObjects.Add(manager.CreateObject(s));
            }
            JsonSerialization.SerializeToFile(aviationObjects, json_filepath);

            System.Timers.Timer timer = new System.Timers.Timer(1000 * FlightGUIUpdater.UpdateFrequencySeconds);
            timer.Elapsed += FlightGUIUpdater.UpdateMap;
            timer.Start();
            Runner.Run();
        }
        static void TestWithStage2data()
        {
            string FTR_file_path = "example_data.ftr";
            int min_offset = 1;
            int max_offset = 5;

            NSS_Simulation simulation = new NSS_Simulation(FTR_file_path, min_offset, max_offset);

            simulation.RunSimulation();

            System.Timers.Timer timer = new System.Timers.Timer(1000 * FlightGUIUpdater.UpdateFrequencySeconds);
            timer.Elapsed += FlightGUIUpdater.UpdateMap;
            timer.Start();
            Runner.Run();
        }

    }
}



