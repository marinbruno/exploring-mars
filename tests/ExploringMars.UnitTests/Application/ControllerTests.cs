using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringMars.Application;
using ExploringMars.Application.Views.InputView;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExploringMars.UnitTests.Application
{
    public class ControllerTests : TestsRoot
    {
        [Fact]
        public async Task
            GetProbesLandingPositions_GivenNoPlateausMeasurementInput_ShouldAskUserForPlateausMeasurementOnce()
        {
            var inputView = new Mock<InputView>();
            var controller = new Controller(ConsoleReader.Object, inputView.Object);
            
            await controller.GetProbesLandingPositions();
            
            inputView.Verify(i => i.AskUserForPlateausMeasurement(), Times.Once);
        }
        
        [Fact]
        public async Task
            GetProbesLandingPositions_GivenAPlateausMeasurementInputAndNoProbesSetup_ShouldAskUserForProbesSetupOnce()
        {
            var inputView = new Mock<InputView>();
            var plateausMeasurement = new List<int> {ValidIntegerInput, ValidIntegerInput};
            var probeInput = new List<ProbeInputView> {new ProbeInputView()};
            var regexMatch = new Mock<Match>();
            var probeInstructions = new List<List<string>> {new List<string>()};
            inputView.Setup(i => i.PlateausMeasurement)
                .Returns(plateausMeasurement);
            inputView.Setup(i => i.ProbeInput)
                .Returns(probeInput);
            inputView.Setup(i => i.InstructionsInput)
                .Returns(probeInstructions);
            inputView.Setup(i => i.ValidateInput(It.IsAny<string>(), It.IsAny<string>(), out []))
            var controller = new Controller(ConsoleReader.Object, inputView.Object);
            
            await controller.GetProbesLandingPositions();
            
            inputView.Verify(i => i.AskUserForProbesStartingSetup(), Times.Once);
            inputView.Verify(i => i.AskUserForProbesStartingSetup(), Times.Once);
        }
    }
}