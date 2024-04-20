using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Logger
    {
        public static void LogMessage(string message)
        {
            DateTime currentDate = DateTime.Now;
            string logFileName = $"log_{currentDate:yyyy_MM_dd}.txt";

            string logText = $"{currentDate:HH:mm} | {message}";

            try
            {
                bool fileExists = File.Exists(logFileName);

                using (StreamWriter sw = new StreamWriter(logFileName, true))
                {
                    if (fileExists)
                    {
                        sw.WriteLine(logText);
                    }
                    else
                    {
                        sw.WriteLine(logText);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas zapisu do pliku: {ex.Message}");
            }
        }
    }
}
