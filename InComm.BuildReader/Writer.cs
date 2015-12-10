using System;

namespace InComm.BuildReader
{
    public interface IWriter
    {
        void Write(string message);
    }
    class ConsoleWriter : IWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
