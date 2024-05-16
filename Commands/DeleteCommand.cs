using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class DeleteCommand:Command
    {
        public DeleteCommand(string input)
        {
            var database = Database.Instance;
            var inputWords = input.Split(' ');
            for (int j = 0; j < inputWords.Count(); j++)
            {
                if (inputWords[j].EndsWith(','))
                    inputWords[j] = inputWords[j].Remove(inputWords[j].Length - 1);
            }
            this.AffectedObjects = database.GetObjectList(inputWords[1]);
            int i = 2;
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
            int counter = 0;
            Database database = Database.Instance;
            var objectsToUse = CheckAffectedObjects(Conditions, Logic);
            foreach(var item in  objectsToUse)
            {
                database.DeleteObject(item.ID);
                counter++;
            }
            Console.WriteLine($"Succesfully deleted {counter} objects");

        }
    }
}
