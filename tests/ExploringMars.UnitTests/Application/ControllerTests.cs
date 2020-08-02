using ExploringMars.Application;

namespace ExploringMars.UnitTests.Application
{
    public class ControllerTests : TestsRoot
    {
        private readonly Controller _controller = new Controller(ConsoleReader.Object);
    }
}