using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public class UpdateCommand:Command
    {
       
        public UpdateCommand(string input) 
        {
            Database database = Database.Instance;
            var inputWords = GetInputWords(input);
            AffectedObjects = database.GetObjectList(inputWords[1]);
            if (inputWords[2] != "set")
                throw new ArgumentException("invalid input - set should follow class name");
            int i = 3;
            SetKeyValueList(ref i, inputWords);
            (Conditions, Logic) = GetConditionsAndLogic(i, inputWords);
        }
        public override void Execute()
        {
            var objectsToUse = CheckAffectedObjects(Conditions, Logic);
            foreach(var obj in objectsToUse)
            {
                obj.UpdateObject(KeyValueList);
            }
            Console.WriteLine($"Succesfully updated {objectsToUse.Count} objects");
        }
    }
}
