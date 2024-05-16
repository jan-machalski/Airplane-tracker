using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Command
    {
        public List<string> DisplayFields = new List<string>();
        public List<(string, string, string)> Conditions = new List<(string, string, string)>();
        public List<string> Logic = new List<string>();
        public List<AviationObject> AffectedObjects = new List<AviationObject>();
        public List<AviationObject> CheckCondition(string field, string sign, string value)
        {
            List<AviationObject> result = new List<AviationObject>();
            foreach(var item in AffectedObjects)
            {
                var dic = item.GetInfoDictionary();
                if (!dic.ContainsKey(field))
                    throw new InvalidDataException($"invalid field name: {field}");
                if (sign == "=")
                {
                    if (value == dic[field])
                        result.Add(item);
                }
                else if (sign == "!=")
                {
                    if (value != dic[field])
                        result.Add(item);
                }
                else if (sign == ">=" || sign == "<=")
                {
                    if (!float.TryParse(dic[field], out var curVal))
                        throw new InvalidDataException($"unable to use >= for non numeric field: {field}");
                    if (!float.TryParse(value, out var cmpVal))
                        throw new InvalidDataException($"Invalid cmp value");
                    if((sign == "<=" && curVal<=cmpVal) || (sign == ">=" && curVal >= cmpVal))
                        result.Add(item);
                }
                else
                    throw new InvalidDataException($"invalid cmp sign: {sign}");
            }
            return result;
        }
        public List<AviationObject> CheckAffectedObjects(List<(string,string,string)> Conditions, List<string> Logic)
        {
            List<AviationObject> objectsToUse = new List<AviationObject>(this.AffectedObjects);
            if (Conditions.Count > 0)
            {
                objectsToUse = CheckCondition(Conditions.First().Item1, Conditions.First().Item2, Conditions.First().Item3);
            }
            for (int i = 0; i < Logic.Count; i++)
            {
                var newObjectsToCheck = CheckCondition(Conditions[i + 1].Item1, Conditions[i + 1].Item2, Conditions[i + 1].Item3);
                if (Logic[i] == "or")
                    objectsToUse = objectsToUse.Union(newObjectsToCheck).ToList();
                else if (Logic[i] == "and")
                    objectsToUse = objectsToUse.Intersect(newObjectsToCheck).ToList();
                else
                    throw new InvalidDataException($"incorrect logical operator: {Logic[i]}");
            }
            return objectsToUse;
        }
    }
}
