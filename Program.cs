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

            NSS_Simulation simulation = new NSS_Simulation(FTR_file_path, min_offset, max_offset);

            simulation.RunSimulation();         
        }

    }
}
