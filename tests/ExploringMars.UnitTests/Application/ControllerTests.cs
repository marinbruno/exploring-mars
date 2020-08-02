using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringMars.Application;
using ExploringMars.Application.Views.InputView;
using ExploringMars.Domain;
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
        public async Task GetProbesLandingPositions_WhenAllDataIsReadFromUser_ShouldNotAskUserAgain()
        {
            var inputView = new Mock<InputView>();
            var probeService = new Mock<ProbePathCalculatorService>();
            var probeInputView = BuildProbeInputView();
            inputView.Setup(i => i.PlateausMeasurement)
                .Returns(new List<int> {5, 5});
            inputView.Setup(i => i.ProbeInput)
                .Returns(new List<ProbeInputView> {probeInputView, probeInputView});
            inputView.Setup(i => i.InstructionsInput)
                .Returns(new List<List<string>>() {new List<string>() {"M"}});
            inputView.Setup(i => i.CountProbeInputs())
                .Returns(2);
            inputView.Setup(i => i.CountInstructionInputs())
                .Returns(2);
            inputView.Setup(i => i.HasPlateausMeasurement())
                .Returns(true);
            probeService.Setup(p => p.CalculateProbesLandingPositions(
                    It.IsAny<List<int>>(), It.IsAny<List<int>>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<List<int>>(), It.IsAny<string>(), It.IsAny<List<string>>()))
                .ReturnsAsync(new List<List<string>>());
            var controller = new Controller(ConsoleReader.Object, inputView.Object);
            
            await controller.GetProbesLandingPositions();
            
            inputView.Verify(i => i.AskUserForPlateausMeasurement(), Times.Never);
            inputView.Verify(i => i.AskUserForProbesInstructions(), Times.Never);
            inputView.Verify(i => i.AskUserForProbesStartingSetup(), Times.Never);
        }


        private ProbeInputView BuildProbeInputView()
        {
            return new ProbeInputView()
            {
                Direction = "N",
                Position = new List<int>{ 2, 2 }
            };
        }
    }
}