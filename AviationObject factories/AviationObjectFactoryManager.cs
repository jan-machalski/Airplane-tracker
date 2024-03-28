using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class AviationObjectFactoryManager
    {

        private static readonly Lazy<AviationObjectFactoryManager> instance = new Lazy<AviationObjectFactoryManager>(() => new AviationObjectFactoryManager());

        private AviationObjectFactoryManager() { }
        public static AviationObjectFactoryManager Instance => instance.Value;

        private Database database = Database.Instance;
        
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

        };

        private static readonly Dictionary<string, string> ObjectCodeTranslate = new Dictionary<string, string>()
        {
            {"C","C" },
            {"P","P" },
            {"CA","CA" },
            {"CP","CP" },
            {"PP","PP" },
            {"AI","AI" },
            {"FL","FL" },
            {"NCR","C" },
            {"NPA","P" },
            {"NCA","CA" },
            {"NCP","CP" },
            {"NPP","PP" },
            {"NAI","AI" },
            {"NFL","FL" }
        };

        

        // the method finds correct factory for the expected object type and creates the object based on a line from an FTR file
        public AviationObject CreateObject(string data)
        {
            List<string> dataList = data.Split(',').ToList();
            if (factories.TryGetValue(ObjectCodeTranslate[dataList[0]],out var factory))
            {
                return factory.CreateAviationObject(dataList);
            }
            throw new ArgumentException("Unknown type: " + data[0]);
        }
        public AviationObject CreateObject(byte[] data)
        {
            string code = Encoding.ASCII.GetString(data,0,3);
            if (factories.TryGetValue(ObjectCodeTranslate[code], out var factory))
            {
                return factory.CreateAviationObject(data);
            }
            throw new ArgumentException("Unknown type: " + code);
        }
    }
}
