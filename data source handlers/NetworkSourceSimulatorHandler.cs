using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace projekt_Jan_Machalski
{
    public class NetworkSourceSimulatorHandler
    {
        private List<AviationObject> latestObject = new List<AviationObject>();

        public void OnNewDataReadyHandler(object? sender, NewDataReadyArgs e)
        {
            if(sender is NetworkSourceSimulator.NetworkSourceSimulator simulator)
                latestObject.Add(AviationObjectFactoryManager.CreateObject(simulator.GetMessageAt(e.MessageIndex).MessageBytes));
        }
        public void CreateSnapshot()
        {

            if (latestObject.Count != 0)
            {
                string snapshotFileName = $"snapshot_{DateTime.Now:HH_mm_ss}.json";

                JsonSerialization.SerializeToFile(latestObject, snapshotFileName);

                Console.WriteLine($"Snapshot created, file name: {snapshotFileName}");
            }
            else
            {
                Console.WriteLine("No data to create snapshot");
            }
        }
    }
}
