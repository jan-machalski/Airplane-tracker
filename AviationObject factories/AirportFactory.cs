﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class AirportFactory:AviationObjectFactory
    {
        public override AviationObject CreateAviationObject(List<string> data)
        {
            var provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return new Airport(
                UInt64.Parse(data[1]), 
                data[2], 
                data[3], 
                Single.Parse(data[4], provider), 
                Single.Parse(data[5], provider), 
                Single.Parse(data[6], provider), 
                data[7]);
        }
        public override AviationObject CreateAviationObject(byte[] data)
        {
            UInt16  NL = BitConverter.ToUInt16(data, 15);
            return new Airport(
                BitConverter.ToUInt64(data, 7),
                Encoding.ASCII.GetString(data, 17, NL),
                Encoding.ASCII.GetString(data, 17 + NL, 3),
                BitConverter.ToSingle(data, 20 + NL),
                BitConverter.ToSingle(data, 24 + NL),
                BitConverter.ToSingle(data, 28 + NL),
                Encoding.ASCII.GetString(data, 32 + NL,3)
                );
        }
    }
}
