using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class AviationObjectFactoryManager
    {
        // the dictionary links the type code with the correct factory 
        // should be expanded in case a new type of aviation object is added
        private static readonly Dictionary<string, AviationObjectFactory> factories = new Dictionary<string,  AviationObjectFactory>()
        {
            {"C", new CrewFactory() },
            {"P", new PassengerFactory() },
            {"CA", new CargoFactory() },
            {"CP", new CargoPlaneFactory() },
            {"PP", new PassengerPlaneFactory() },
            {"AI", new AirportFactory() },
            {"FL", new FlightFactory() },
            {"NCR", new CrewFactory() },
            {"NPA", new PassengerFactory() },
            {"NCA", new CargoFactory() },
            {"NCP", new CargoPlaneFactory() },
            {"NPP", new PassengerPlaneFactory() },
            {"NAI", new AirportFactory() },
            {"NFL", new FlightFactory() }
        };

        // the method finds correct factory for the expected object type and creates the object based on a line from an FTR file
        public static AviationObject CreateObject(string data)
        {
            List<string> dataList = data.Split(',').ToList();
            if (factories.TryGetValue(dataList[0],out var factory))
            {
                return factory.CreateAviationObject(dataList);
            }
            throw new ArgumentException("Unknown type: " + data[0]);
        }
        public static AviationObject CreateObject(byte[] data)
        {
            string code = Encoding.ASCII.GetString(data,0,3);
            if (factories.TryGetValue(code, out var factory))
            {
                return factory.CreateAviationObject(data);
            }
            throw new ArgumentException("Unknown type: " + code);
        }
    }
}
