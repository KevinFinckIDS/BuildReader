using System;
namespace InComm.BuildReader
{
    class Program
    {
        static void Main(string[] args)
        {
            new TeamCityReporter(null, new ConsoleWriter()).ShowAll();
            Console.Write("Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
