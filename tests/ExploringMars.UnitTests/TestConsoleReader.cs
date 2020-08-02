using ExploringMars.Application;

namespace ExploringMars.UnitTests
{
    public abstract class TestConsoleReader : ConsoleReader
    {
        public override string GetUserInput()
        {
            return base.GetUserInput();
        }
    }
}