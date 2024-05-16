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
            LoadFTR();
            string command1 = "delete Airport where ID = 5";
            string command2 = "display * from Airport where ID <= 8 and ID >= 3";
            var test1 = new DeleteCommand(command1);

            test1.Execute();
            var test2 = new DisplayCommand(command2);
            test2.Execute();

            Logger.LogMessage("Program started!");
            RunCommandPrompt();
        }

        static void RunCommandPrompt()
        {
            string FTR_file_path = "example.ftre";
            int min_offset = 100;
            int max_offset = 200;
            Database database = Database.Instance;
            bool ftrLoaded = false;
            bool trackerStarted = false;
            

            NSS_Simulation simulation = new NSS_Simulation(FTR_file_path, min_offset, max_offset);

            bool running = true;
            string command;

            do
            {
                Console.WriteLine("Commands:" +
                        "\n-run:\t\trun NSS simulation" +
                        "\n-ftr:\t\tload data from FTR file" +
                        "\n-track:\t\tdisplay the flight tracker "+
                        "\n-report\t\treport all news"+
                        "\n-print:\t\tcreate a snapshot" +
                        "\n-exit:\t\texit programm\n");
                command = Console.ReadLine();
                Console.WriteLine();
                switch (command)
                {
                    case "ftr":
                        LoadFTR();
                        ftrLoaded = true;
                        break;
                    case "run":
                        if (ftrLoaded == false)
                            Console.WriteLine("FTR file has to be loaded before starting simulation");
                        else
                            simulation.RunSimulation();
                        break;
                    case "track":
                        if (!trackerStarted)
                            FlightGUIRunner.RunFlightGUI();
                        else
                            Console.WriteLine("Flight tracker GUI can only be started once per each programm start");
                        trackerStarted = true;
                        break;
                    case "report":
                        GenerateAllNews(); 
                        break;
                    case "print":
                        simulation.CreateSnapshot();
                        break;
                    case "exit":
                        Console.WriteLine("exiting...");
                        Logger.LogMessage("Program exited!\n");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("incorrect command");
                        break;
                }

            } while (running);
        }

        static void LoadFTR()
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
        }
        static void GenerateAllNews()
        {
            NewsGenerator generator = new NewsGenerator(NewsListsCreator.GetMediaList(),NewsListsCreator.GetReportableList());
            string news = generator.GenerateNextNews();
            while(news != null)
            {
                Console.WriteLine(news);
                news = generator.GenerateNextNews();
            }
        }

    }
}



