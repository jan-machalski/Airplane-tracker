﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class DisplayCommand : Command
    {

        public DisplayCommand(string input)
        {
            var database = Database.Instance;
            var inputWords = input.Split(' ');
            for (int j = 0; j < inputWords.Count(); j++)
            {
                if (inputWords[j].EndsWith(','))
                    inputWords[j] = inputWords[j].Remove(inputWords[j].Length - 1);
            }
            int i = 1;
            while (i < inputWords.Length && inputWords[i] != "from")
            {
                DisplayFields.Add(inputWords[i]);
                i++;
            }
            i++;
            AffectedObjects = database.GetObjectList(inputWords[i]);
            if(DisplayFields.Contains("*") && AffectedObjects.Count > 0)
            {
                DisplayFields = new List<string>();
                var dir = AffectedObjects.First().GetInfoDictionary();
                foreach(var p in dir)
                {
                    if(!p.Key.Contains('.'))
                        DisplayFields.Add(p.Key);
                }
            }
            i++;
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
        }
        public void Execute()
        {
            var objectsToUse = CheckAffectedObjects(Conditions, Logic);
            List<Dictionary<string, string>> DisplayRows = new List<Dictionary<string, string>>();
            foreach (var o in objectsToUse)
            {
                Dictionary<string, string> row = new Dictionary<string, string>();
                var d = o.GetInfoDictionary();
                foreach (var field in DisplayFields)
                {
                    if (d.ContainsKey(field))
                        row[field] = d[field];
                    else
                        throw new InvalidDataException($"incorrect data field: {field}");
                }
                DisplayRows.Add(row);
            }
            DisplayTable(DisplayRows);

        }
        static void DisplayTable(List<Dictionary<string, string>> rows)
        {
            var headers = rows.SelectMany(dict => dict.Keys).Distinct().ToList();

            var columnWidths = headers.ToDictionary(
                header => header,
                header => Math.Max(header.Length, rows.Max(row => row.ContainsKey(header) ? row[header].Length : 0)) + 2 
            );

            Console.Write("|");
            foreach (var header in headers)
            {
                Console.Write($" {header.PadRight(columnWidths[header] - 1)}|");
            }
            Console.WriteLine();

            Console.WriteLine(new string('-', columnWidths.Values.Sum() + headers.Count + 1));

            foreach (var row in rows)
            {
                Console.Write("|");
                foreach (var header in headers)
                {
                    if (row.TryGetValue(header, out string value))
                    {
                        Console.Write($" {value.PadLeft(columnWidths[header] - 1)}|");
                    }
                    else
                    {
                        Console.Write($"{"".PadLeft(columnWidths[header] - 1)}|");
                    }
                }
                Console.WriteLine();

                Console.WriteLine(new string('-', columnWidths.Values.Sum() + headers.Count + 1));
            }
        }
    }
}
