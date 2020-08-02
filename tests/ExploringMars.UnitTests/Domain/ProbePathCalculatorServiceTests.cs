using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploringMars.Domain;
using FluentAssertions;
using Xunit;

namespace ExploringMars.UnitTests.Domain
{
    public class ProbePathCalculatorServiceTests
    {
        private readonly ProbePathCalculatorService _probePathCalculatorService = new ProbePathCalculatorService();
        
        [Fact]
        public async Task
            CalculateProbesLandingPositions_GivenFirstTestScenario_ShouldReturnItsLandingPositionsAndDirectionsAsExpected() 
        {
            var plateausMeasurement = new List<int> {5, 5};
            var firstProbesStartingPosition = new List<int> {1, 2};
            const string firstProbesStartingDirection = "N";
            var firstProbesInstructions = new List<string> {"L", "M", "L", "M", "L", "M", "L", "M", "M"};
            
            var secondProbesStartingPosition = new List<int> {3, 3};
            const string secondProbesStartingDirection = "E";
            var secondProbesInstructions = new List<string> {"M", "M", "R", "M", "M", "R", "M", "R", "R", "M"};

            var probesLandingPositions = await _probePathCalculatorService.CalculateProbesLandingPositions(
                plateausMeasurement, firstProbesStartingPosition, firstProbesStartingDirection, firstProbesInstructions,
                secondProbesStartingPosition, secondProbesStartingDirection, secondProbesInstructions);

            var firstProbeXCoordinate = probesLandingPositions.First()[0];
            var firstProbeYCoordinate = probesLandingPositions.First()[1];
            var firstProbeDirection = probesLandingPositions.First()[2];
            
            var secondProbeXCoordinate = probesLandingPositions.Last()[0];
            var secondProbeYCoordinate = probesLandingPositions.Last()[1];
            var secondProbeDirection = probesLandingPositions.Last()[2];

            probesLandingPositions.Should().BeOfType<List<List<string>>>();
            firstProbeXCoordinate.Should().Be("1");
            firstProbeYCoordinate.Should().Be("3");
            firstProbeDirection.Should().Be("N");
            secondProbeXCoordinate.Should().Be("5");
            secondProbeYCoordinate.Should().Be("1");
            secondProbeDirection.Should().Be("E");
        }
        
        [Fact]
        public async Task
            CalculateProbesLandingPositions_GivenSecondTestScenario_ShouldReturnItsLandingPositionsAndDirectionsAsExpected() 
        {
            var plateausMeasurement = new List<int> {5, 5};
            var firstProbesStartingPosition = new List<int> {2, 2};
            const string firstProbesStartingDirection = "S";
            var firstProbesInstructions = new List<string> {"M", "M", "M", "M", "M", "M", "M", "M", "M"};
            
            var secondProbesStartingPosition = new List<int> {3, 3};
            const string secondProbesStartingDirection = "E";
            var secondProbesInstructions = new List<string> {"M", "M", "M", "M", "M", "M", "M", "M", "M", "M"};

            var probesLandingPositions = await _probePathCalculatorService.CalculateProbesLandingPositions(
                plateausMeasurement, firstProbesStartingPosition, firstProbesStartingDirection, firstProbesInstructions,
                secondProbesStartingPosition, secondProbesStartingDirection, secondProbesInstructions);

            var firstProbeXCoordinate = probesLandingPositions.First()[0];
            var firstProbeYCoordinate = probesLandingPositions.First()[1];
            var firstProbeDirection = probesLandingPositions.First()[2];
            
            var secondProbeXCoordinate = probesLandingPositions.Last()[0];
            var secondProbeYCoordinate = probesLandingPositions.Last()[1];
            var secondProbeDirection = probesLandingPositions.Last()[2];

            probesLandingPositions.Should().BeOfType<List<List<string>>>();
            firstProbeXCoordinate.Should().Be("2");
            firstProbeYCoordinate.Should().Be("0");
            firstProbeDirection.Should().Be("S");
            secondProbeXCoordinate.Should().Be("5");
            secondProbeYCoordinate.Should().Be("3");
            secondProbeDirection.Should().Be("E");
        }
    }
}