using ExploringMars.Application.Views.InputView;
using Moq;
using Xunit;

namespace ExploringMars.UnitTests.Application.ViewTests
{
    public class InputViewTests
    {
        private Mock<InputView> _inputView = new Mock<InputView>();

        private const string ValidPlateauInput = "1 2";

        [Fact]
        public void AskUserForPlateausMeasurement_GivenValidInput_ShouldAddItsLengthAndWidthToPlateausMeasurement()
        {

        }
    }
}