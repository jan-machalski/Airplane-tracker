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
            var inputWords = GetInputWords(input);
            this.AffectedObjects = database.GetObjectList(inputWords[1]);
            int i = 2;
            (Conditions, Logic) = GetConditionsAndLogic(i, inputWords);
        }
        public override void Execute()
        {
            int counter = 0;
            Database database = Database.Instance;
            var objectsToUse = CheckAffectedObjects(Conditions, Logic);
            foreach(var item in objectsToUse)
            {
                database.DeleteObject(item.ID);
                counter++;
            }
            Console.WriteLine($"Succesfully deleted {counter} objects");

        }
    }
}
