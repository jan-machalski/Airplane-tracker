﻿using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace projekt_Jan_Machalski
{
    public class NSS_Simulation
    {
        private List<AviationObject> latestObject = new List<AviationObject>();
        private NetworkSourceSimulator.NetworkSourceSimulator source;
        private bool running;
        public NSS_Simulation(string FTR_file_path,int min_offset,int max_offset)
        {
            source = new NetworkSourceSimulator.NetworkSourceSimulator(FTR_file_path, min_offset, max_offset);
            running = false;
        }

        public void OnNewDataReadyHandler(object? sender, NewDataReadyArgs e)
        {
            AviationObjectFactoryManager manager = AviationObjectFactoryManager.Instance;
            latestObject.Add(manager.CreateObject(source.GetMessageAt(e.MessageIndex).MessageBytes));
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
        public void RunSimulation()
        {
            if (running)
            {
                Console.WriteLine("Simulation already running on this object instance");
                return;
            }

            string command;
            running = true;

            source.OnNewDataReady += OnNewDataReadyHandler;

            Thread NSS_thread = new Thread(new ThreadStart(source.Run));
            NSS_thread.IsBackground = true;
            NSS_thread.Start();

           
        }
    }
}
