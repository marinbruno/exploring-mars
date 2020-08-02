using System.Collections.Generic;
using System.Threading.Tasks;
using ExploringMars.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ExploringMars.UnitTests.Domain.Entities
{
    public class PositionTests
    {
        private static readonly List<int> ListOfIntegers = new List<int> {1, 3};
        
        private static readonly Position ReferencePosition = new Position(ListOfIntegers);

        [Fact]
        public void Position_GivenValidInput_ShouldReturnItsCoordinatesAsExpected() 
        {
            var newPosition = new Position(ListOfIntegers);

            newPosition.XCoordinate.Should().Be(ListOfIntegers[0]);
            newPosition.YCoordinate.Should().Be(ListOfIntegers[1]);
        }

        [Fact]
        public void MoveLengthwise_GivenItIsMovingTowardsPlateauLimits_ShouldIncrementXCoordinateByOne()
        {
            var newPosition = new Position(ReferencePosition);

            newPosition.MoveLengthwise(true);

            newPosition.XCoordinate.Should().Be(ReferencePosition.XCoordinate + 1);
        }
        
        [Fact]
        public void MoveLengthwise_GivenItIsNotMovingTowardsPlateauLimits_ShouldIncrementXCoordinateByOne()
        {
            var newPosition = new Position(ReferencePosition);

            newPosition.MoveLengthwise(false);

            newPosition.XCoordinate.Should().Be(ReferencePosition.XCoordinate - 1);
        }
        
        [Fact]
        public void MoveWidthwise_GivenItIsMovingTowardsPlateauLimits_ShouldIncrementYCoordinateByOne()
        {
            var newPosition = new Position(ReferencePosition);

            newPosition.MoveWidthwise(true);

            newPosition.YCoordinate.Should().Be(ReferencePosition.YCoordinate + 1);
        }
        
        [Fact]
        public void MoveWidthwise_GivenItIsNotMovingTowardsPlateauLimits_ShouldIncrementYCoordinateByOne()
        {
            var newPosition = new Position(ReferencePosition);

            newPosition.MoveWidthwise(false);

            newPosition.YCoordinate.Should().Be(ReferencePosition.YCoordinate - 1);
        }

        [Fact]
        public async Task IsValid_GivenAnInvalidPosition_ShouldReturnFalse()
        {
            var plateauLimits = new Position(ReferencePosition);
            plateauLimits.MoveLengthwise(false);
            var isValid = await ReferencePosition.IsValid(plateauLimits);

            isValid.Should().BeFalse();
        }
        
        [Fact]
        public async Task IsValid_GivenAValidPosition_ShouldReturnTrue()
        {
            var plateauLimits = new Position(ReferencePosition);
            plateauLimits.MoveLengthwise(true);
            var isValid = await ReferencePosition.IsValid(plateauLimits);

            isValid.Should().BeTrue();
        }
    }
}