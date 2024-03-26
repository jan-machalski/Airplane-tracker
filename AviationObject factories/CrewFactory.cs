using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace projekt_Jan_Machalski
{
    public class CrewFactory: AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var newObject = new Crew(
                UInt64.Parse(data[1]), 
                data[2], 
                UInt64.Parse(data[3]), 
                data[4], 
                data[5], 
                UInt16.Parse(data[6]), 
                data[7]);
            AddToInfoDictionary( newObject );
            return newObject;
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16 NL = BitConverter.ToUInt16(data, 15);
            UInt16 EL = BitConverter.ToUInt16(data, 31 + NL);
            var newObject = new Crew(
                BitConverter.ToUInt64(data,7),
                Encoding.ASCII.GetString(data,17,NL),
                BitConverter.ToUInt16(data,17+NL),
                Encoding.ASCII.GetString(data,19+NL,12),
                Encoding.ASCII.GetString(data,NL+33,EL),
                BitConverter.ToUInt16(data,33+NL+EL),
                Encoding.ASCII.GetString(data,35+NL+EL,1));
            AddToInfoDictionary(newObject);
            return newObject;
        
        }
        public void AddToInfoDictionary(Crew crew)
        {
            AviationObjectFactoryManager manager = AviationObjectFactoryManager.Instance;
            if (!manager.CrewInfo.ContainsKey(crew.ID))
            {
                manager.CrewInfo.Add(crew.ID, crew);
            }
        }
    }
}
