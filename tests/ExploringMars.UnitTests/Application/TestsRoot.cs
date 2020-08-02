using Moq;

namespace ExploringMars.UnitTests.Application
{
    public class TestsRoot
    {
        protected static readonly Mock<TestsConsoleReader> ConsoleReader = new Mock<TestsConsoleReader>();

        protected const int ValidIntegerInput = 1;
        protected const string ValidStringInput = "N";
        protected const string InvalidInput = "#,2";
        
        protected static readonly string ValidPlateauInput = $"{ValidIntegerInput} {ValidIntegerInput}";
        protected static readonly string ValidProbesStartingSetupInput = $"{ValidIntegerInput} {ValidIntegerInput} {ValidStringInput}";
        protected const string ValidInstructionsInput = "LRMRLRMRLRMRL";
    }
}