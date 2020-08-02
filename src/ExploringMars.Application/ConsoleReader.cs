using System;

namespace ExploringMars.Application
{
    public class ConsoleReader : IConsoleReader
    {
        public virtual string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}