using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class FileReader
    {
        public static List<string> ReadFromFile(string filePath)
        {
            List<string> dataList = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        dataList.Add(line); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from file: " + ex.Message);
            }

            return dataList;
        }
    }
}
