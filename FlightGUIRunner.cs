using FlightTrackerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class FlightGUIRunner
    {
        public static void RunFlightGUI()
        {
            Thread GUI_thread = new Thread(Runner.Run);
            GUI_thread.IsBackground = true;
            System.Timers.Timer timer = new System.Timers.Timer(1000 * FlightGUIUpdater.UpdateFrequencySeconds);
            timer.Elapsed += FlightGUIUpdater.UpdateMap;
            timer.Start();
            GUI_thread.Start();
        }
    }
}
