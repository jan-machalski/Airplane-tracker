using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
   
    public class AddCommand:Command
    {
        private string ClassName;
        public AddCommand(string input)
        {
            var database = Database.Instance;
            var inputWords = GetInputWords(input);
            ClassName = inputWords[1];
            if (inputWords[2] != "new")
                throw new ArgumentException("word new should follow the class name");
            int i = 3;
            SetKeyValueList(ref i, inputWords);
            if(!KeyValueList.ContainsKey("ID"))
            {
                KeyValueList.Add("ID",(database.MaxID + 1).ToString());
            }
        }
        public override void Execute()
        {
            var manager = AviationObjectFactoryManager.Instance;
            manager.CreateObject(ClassName, KeyValueList);
            Console.WriteLine("Succesfully added 1 new object");
        }
    }
}
