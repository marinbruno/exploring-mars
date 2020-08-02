using System.Collections.Generic;
using System.Linq;
using ExploringMars.Application.Exceptions;
using ExploringMars.Application.Views.InputView;
using FluentAssertions;
using Xunit;

namespace ExploringMars.UnitTests.Application
{
    public class InputViewTests : TestsRoot
    {
        private readonly InputView _inputView = new InputView(ConsoleReader.Object);

        [Fact]
        public void AskUserForPlateausMeasurement_GivenValidInput_ShouldAddItsLengthAndWidthToPlateausMeasurementAsExpected()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(ValidPlateauInput);
            
            _inputView.AskUserForPlateausMeasurement();

            _inputView.PlateausMeasurement[0].Should().Be(ValidIntegerInput);
            _inputView.PlateausMeasurement[1].Should().Be(ValidIntegerInput);
        }
        
        [Fact]
        public void AskUserForProbesStartingSetup_GivenValidInput_ShouldAddItsPositionAndDirectionToProbesStartingSetupAsExpected()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(ValidProbesStartingSetupInput);

            _inputView.PlateausMeasurement = new List<int> {5, 5};
            _inputView.AskUserForProbesStartingSetup();

            _inputView.ProbeInput.First().Position[0].Should().Be(ValidIntegerInput);
            _inputView.ProbeInput.First().Position[1].Should().Be(ValidIntegerInput);
            _inputView.ProbeInput.First().Direction.Should().Be(ValidStringInput);
        }
        
        [Fact]
        public void AskUserForProbesInstructions_GivenValidInput_ShouldAddItsProbesInstructionsAsExpected()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(ValidInstructionsInput);

            _inputView.AskUserForProbesInstructions();

            _inputView.InstructionsInput.First().Should()
                .BeEquivalentTo(ValidInstructionsInput.Select(c => c.ToString()).ToList());
        }
        
        [Fact]
        public void AskUserForPlateausMeasurement_GivenInvalidInput_ShouldThrowInvalidInputException()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(InvalidInput);
            
            _inputView.Invoking(i => i.AskUserForPlateausMeasurement())
                .Should().Throw<InvalidInputException>();
        }
        
        [Fact]
        public void AskUserForProbesStartingSetup_GivenInvalidInput_ShouldThrowInvalidInputException()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(InvalidInput);

            _inputView.PlateausMeasurement = new List<int> {5, 5};
            _inputView.Invoking(i => i.AskUserForProbesStartingSetup())
                .Should().Throw<InvalidInputException>();
        }
        
        [Fact]
        public void AskUserForProbesStartingSetup_GivenInvalidProbePosition_ShouldThrowInvalidInputException()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns("10 10");

            _inputView.PlateausMeasurement = new List<int> {5, 5};
            _inputView.Invoking(i => i.AskUserForProbesStartingSetup())
                .Should().Throw<InvalidInputException>();
        }
        
        [Fact]
        public void AskUserForProbesInstructions_GivenInvalidInput_ShouldThrowInvalidInputException()
        {
            ConsoleReader.Setup(consoleReader => consoleReader.GetUserInput())
                .Returns(InvalidInput);
            
            _inputView.Invoking(i => i.AskUserForProbesInstructions())
                .Should().Throw<InvalidInputException>();
        }
    }
}