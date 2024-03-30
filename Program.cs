﻿using System;
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
            RunCommandPrompt();
        }

        static void RunCommandPrompt()
        {
            string FTR_file_path = "example_data.ftr";
            int min_offset = 1;
            int max_offset = 5;

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
                        break;
                    case "run":
                        simulation.RunSimulation();
                        break;
                    case "track":
                        FlightGUIRunner.RunFlightGUI();
                        break;
                    case "report":
                        GenerateAllNews(); 
                        break;
                    case "print":
                        simulation.CreateSnapshot();
                        break;
                    case "exit":
                        Console.WriteLine("exiting...");
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



