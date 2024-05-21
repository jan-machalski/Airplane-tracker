using NetTopologySuite.IO;
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
            List<string> inputParts = new List<string>();
            int j = i + 1;
            {
                while(j < inputWords.Length)
                {
                    char firstChar = inputWords[j][0];
                    
                    if (firstChar == '"' || firstChar == '\'')
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(inputWords[j].Substring(1));
                        char lastChar = inputWords[j][inputWords[j].Length - 1];
                        while(lastChar != firstChar && j+1 < inputWords.Length)
                        {
                            sb.Append(' ' + inputWords[++j]);
                            lastChar = inputWords[j][inputWords[j].Length - 1];
                        }
                        if (j == inputWords.Length && lastChar != firstChar)
                            throw new ArgumentException("Missing quotation mark in conditions list");
                        else
                        {
                            sb.Length = sb.Length - 1;
                            inputParts.Add(sb.ToString());
                        }
                    }
                    else 
                        inputParts.Add(inputWords[j]);
                    j++;
                }
            }
            var inputPartsArray = inputParts.ToArray();
            i=0;
            if (i + 2 < inputPartsArray.Length)
            {
                Conditions.Add((inputPartsArray[i], inputPartsArray[i + 1], inputPartsArray[i + 2]));
                i += 3;
            }
            while (i + 3 < inputPartsArray.Length)
            {
                Conditions.Add((inputPartsArray[i + 1], inputPartsArray[i + 2], inputPartsArray[i + 3]));
                Logic.Add(inputPartsArray[i]);
                i += 4;
            }
            return (Conditions, Logic);
        }
        public void SetKeyValueList(ref int j, string[] inputWords)
        {
            List<string> inputParts = new List<string>();
            while(j < inputWords.Length && inputWords[j] != "where") 
            {
                char firstChar = inputWords[j][0];
                    
                    if (firstChar == '"' || firstChar == '\'')
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(inputWords[j].Substring(1));
                        char lastChar = inputWords[j][inputWords[j].Length - 1];
                        while(lastChar != firstChar && j+1 < inputWords.Length)
                        {
                            sb.Append(' ' + inputWords[++j]);
                            lastChar = inputWords[j][inputWords[j].Length - 1];
                        }
                        if (j == inputWords.Length && lastChar != firstChar)
                            throw new ArgumentException("Missing quotation mark in key value list");
                        else
                        {
                            sb.Length = sb.Length - 1;
                            inputParts.Add(sb.ToString());
                        }
                    }
                    else 
                        inputParts.Add(inputWords[j]);
                    j++;
            }
            var inputPartsArray = inputParts.ToArray();
            int i = 0;
            while (i < inputPartsArray.Length)
            {
                inputPartsArray[i] = inputPartsArray[i].TrimStart('(').TrimEnd(')');
                if (inputPartsArray[i].Contains('='))
                {
                    string[] parts = inputPartsArray[i].Split('=');
                    KeyValueList.Add(parts[0], parts[1]);
                    i++;
                }
                else if (i < inputPartsArray.Length - 2 && inputPartsArray[i + 1] == "=")
                {
                    KeyValueList.Add(inputPartsArray[i], inputPartsArray[i + 2].TrimEnd(')'));
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
