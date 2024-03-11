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

            RunSimulation(FTR_file_path, min_offset, max_offset);
            
            return;


        }
        static void RunSimulation(string FTR_file_path,int min_offset, int max_offset)
        {
            string command;
            bool running = true;

            NetworkSourceSimulator.NetworkSourceSimulator source = new NetworkSourceSimulator.NetworkSourceSimulator(FTR_file_path, min_offset, max_offset);
            var source_handler = new NetworkSourceSimulatorHandler();
            source.OnNewDataReady += source_handler.OnNewDataReadyHandler;

            Thread NSS_thread = new Thread(new ThreadStart(source.Run));
            NSS_thread.IsBackground = true;
            NSS_thread.Start();

            Console.WriteLine("Commands:\n-print:\tcreate a snapshot\n-exit:\texit programm");
            do
            {
                command = Console.ReadLine();
                switch (command)
                {
                    case "print":
                        source_handler.CreateSnapshot();
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
    }
}
