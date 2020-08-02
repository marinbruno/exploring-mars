using ExploringMars.Application;

namespace ExploringMars.UnitTests.Application
{
    public abstract class TestsConsoleReader : ConsoleReader
    {
        public override string GetUserInput()
        {
            return base.GetUserInput();
        }
    }
}