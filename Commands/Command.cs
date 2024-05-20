using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class Command
    {
        public Dictionary<string, string> KeyValueList = new Dictionary<string, string>();
        public List<string> DisplayFields = new List<string>();
        public List<(string, string, string)> Conditions = new List<(string, string, string)>();
        public List<string> Logic = new List<string>();
        public List<AviationObject> AffectedObjects = new List<AviationObject>();
        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
        public List<AviationObject> CheckCondition(string field, string sign, string value)
        {
            List<AviationObject> result = new List<AviationObject>();
            foreach(var item in AffectedObjects)
            {
                var dic = item.GetInfoDictionary();
                if (!dic.ContainsKey(field))
                    throw new ArgumentException($"invalid field name: {field}");
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
                        throw new ArgumentException($"unable to use >= for non numeric field: {field}");
                    if (!float.TryParse(value, out var cmpVal))
                        throw new ArgumentException($"Invalid cmp value");
                    if((sign == "<=" && curVal<=cmpVal) || (sign == ">=" && curVal >= cmpVal))
                        result.Add(item);
                }
                else
                    throw new ArgumentException($"invalid cmp sign: {sign}");
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
                    throw new ArgumentException($"incorrect logical operator: {Logic[i]}");
            }
            return objectsToUse;
        }
        public (List<(string,string,string)> Conditions, List<string> Logic) GetConditionsAndLogic(int i, string[] inputWords)
        {
            if (i < inputWords.Length && inputWords[i] != "where")
                throw new InvalidDataException("invalid conditions");
            i++;
            if (i + 2 < inputWords.Length)
            {
                Conditions.Add((inputWords[i], inputWords[i + 1], inputWords[i + 2]));
                i += 3;
            }
            while (i + 3 < inputWords.Length)
            {
                Conditions.Add((inputWords[i + 1], inputWords[i + 2], inputWords[i + 3]));
                Logic.Add(inputWords[i]);
                i += 4;
            }
            return (Conditions, Logic);
        }
        public void SetKeyValueList(ref int i, string[] inputWords)
        {
            while (i < inputWords.Length && inputWords[i] != "where")
            {
                inputWords[i] = inputWords[i].TrimStart('(').TrimEnd(')');
                if (inputWords[i].Contains('='))
                {
                    string[] parts = inputWords[i].Split('=');
                    KeyValueList.Add(parts[0], parts[1]);
                    i++;
                }
                else if (i < inputWords.Length - 2 && inputWords[i + 1] == "=")
                {
                    KeyValueList.Add(inputWords[i], inputWords[i + 2].TrimEnd(')'));
                    i += 3;
                }
                else
                    throw new ArgumentException("input should be: 'field=value' or 'field = value'");
            }
        }
        public string[] GetInputWords(string input)
        {
            input = input.TrimEnd().TrimStart();
            var inputWords = input.Split(' ');
            for (int j = 0; j < inputWords.Count(); j++)
            {
                if (inputWords[j].EndsWith(','))
                    inputWords[j] = inputWords[j].Remove(inputWords[j].Length - 1);
            }
            return inputWords;
        }
    }
}
