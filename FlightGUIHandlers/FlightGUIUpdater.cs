using Avalonia.Metadata;
using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace projekt_Jan_Machalski
{
    public class FlightGUIUpdater
    {
        public const int UpdateFrequencySeconds = 1;

        public static void UpdateMap(object? sender, ElapsedEventArgs? e)
        {
            Database database = Database.Instance;
          
            FlightsGUIData data = new FlightsGUIData();

            List<FlightGUI> newFlightsData = new List<FlightGUI>();

            foreach(var f in database.FlightInfo) // create FlightGUI structure for every flight with available info
            {
                FlightGUI newData = new FlightGuiAdapter(f.Value);
                newFlightsData.Add(newData);
                f.Value.Latitude = newData.WorldPosition.Latitude;
                f.Value.Longitude = newData.WorldPosition.Longitude;
            }

            data.UpdateFlights(newFlightsData);
            Runner.UpdateGUI(data);
        }

    }
}
