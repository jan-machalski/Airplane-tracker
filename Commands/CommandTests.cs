using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    internal class CommandTests
    {
        public static void TestAirport()
        {
            string command1 = "delete Airport where ID = 5";
            string command2 = "display ID, Name from Airports   ";
            string command3 = "update Airport set (Name = AAA) where ID >= 4";
            string command4 = "add Airport new Name = BBB";
            var test1 = new DeleteCommand(command1);

            test1.Execute();
            var test2 = new DisplayCommand(command2);
            test2.Execute();

            var test3 = new UpdateCommand(command3);
            test3.Execute();
            var test4 = new AddCommand(command4);
            test4.Execute();
            test2 = new DisplayCommand(command2);
            test2.Execute();
        }
        public static void TestCargo()
        {

            var test4b = new AddCommand("add Cargo new Weight = 100 ID = 9999 Code = aaaabb Description = newcargo");
            test4b.Execute();
            var test3b = new UpdateCommand("update Cargo set (Description=dzidok) where ID = 38");
            test3b.Execute();
            var test2b = new DeleteCommand("delete Cargo where ID <= 30");
            test2b.Execute();
            string command1b = "display * from Cargo where Weight >= 15";
            var test1b = new DisplayCommand(command1b);
            test1b.Execute();
        }
        public static void TestPassenger()
        {
            var test4 = new AddCommand("add Passenger new Name = Wojtek, Class = Economy, Age = 50, Miles = 50000");
            test4.Execute();
            var test3 = new UpdateCommand("update Passengers set Email = premium where Miles >= 20000 and Class = Economy");
            test3.Execute();
            var test2 = new DeleteCommand("delete Passengers where ID >= 700");
            test2.Execute();
            var test1 = new DisplayCommand("display * from Passengers where Age = 50");
            test1.Execute();
        }
        public static void TestCrew()
        {
            var test4 = new AddCommand("Add Crew new (Age = 60)");
            test4.Execute();
            var test3 = new UpdateCommand("update Crew set Age = 19, Role=aaa where Age >= 55");
            test3.Execute();
            var test2 = new DeleteCommand("delete Crew where Age = 28");
            test2.Execute();
            var test1 = new DisplayCommand("display * from Crew where ID >= 300 and Age <= 30" );
            test1.Execute();
        }
    }
}
