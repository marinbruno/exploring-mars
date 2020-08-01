using System;
using System.Collections.Generic;
using System.Linq;
using ExploringMars.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ExploringMars.UnitTests.Domain.Entities
{
    public class ProbeTests
    {
        private static readonly List<int> ListOfIntegers = new List<int> {1, 3};

        private static readonly Probe ReferenceProbe = new Probe(ListOfIntegers, "N");
        
        [Theory]
        [InlineData("N")]
        [InlineData("S")]
        [InlineData("W")]
        [InlineData("E")]
        public void Probe_GivenValidCoordinatesAndDirection_ShouldReturnItsPositionHistoryAndDirectionAsExpected(string startingDirection)
        {
            var probe = new Probe(ListOfIntegers, startingDirection);
            Enum.TryParse(startingDirection, out DirectionEnum direction);

            probe.PositionHistory.First().XCoordinate.Should().Be(ListOfIntegers[0]);
            probe.PositionHistory.First().YCoordinate.Should().Be(ListOfIntegers[1]);
            probe.CurrentDirection.Should().Be(direction);
        }

        [Theory]
        [InlineData(DirectionEnum.N, DirectionEnum.W)]
        [InlineData(DirectionEnum.W, DirectionEnum.S)]
        [InlineData(DirectionEnum.S, DirectionEnum.E)]
        [InlineData(DirectionEnum.E, DirectionEnum.N)]
        public void
            RotateLeft_GivenAnyCurrentDirection_ShouldUpdateItsCurrentDirectionAsExpected(DirectionEnum currentDirection, DirectionEnum expectedDirection)
        {
            ReferenceProbe.CurrentDirection = currentDirection;
            
            ReferenceProbe.RotateLeft();

            ReferenceProbe.CurrentDirection.Should().Be(expectedDirection);
        }
        
        [Theory]
        [InlineData(DirectionEnum.N, DirectionEnum.E)]
        [InlineData(DirectionEnum.W, DirectionEnum.N)]
        [InlineData(DirectionEnum.S, DirectionEnum.W)]
        [InlineData(DirectionEnum.E, DirectionEnum.S)]
        public void
            RotateRight_GivenAnyCurrentDirection_ShouldUpdateItsCurrentDirectionAsExpected(DirectionEnum currentDirection, DirectionEnum expectedDirection)
        {
            ReferenceProbe.CurrentDirection = currentDirection;
            
            ReferenceProbe.RotateRight();

            ReferenceProbe.CurrentDirection.Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData("N")]
        [InlineData("S")]
        [InlineData("W")]
        [InlineData("E")]    
        public void MoveForward_GivenAnInvalidNewPosition_ShouldAddNewPositionWithTheSameCoordinatesAsTheOriginal(string startingDirection)
        {
            var probe = new Probe(new []{0,0}, startingDirection);
            var plateauLimits = new Position(new []{0,0});
            
            probe.MoveForward(plateauLimits);

            probe.PositionHistory.First().XCoordinate.Should().Be(probe.PositionHistory.Last().XCoordinate);
            probe.PositionHistory.First().YCoordinate.Should().Be(probe.PositionHistory.Last().YCoordinate);
        }

        [Theory]
        [InlineData("S", 1, 2)]
        [InlineData("W", 0, 3)]
        [InlineData("N", 1, 4)]
        [InlineData("E", 2, 3)]
        public void 
            MoveForward_GivenAValidNewPosition_ShouldAddNewPositionIncrementingItsCoordinateByOne(string startingDirection, int lastXCoordinate, int lastYCoordinate)
        {
            var probe = new Probe(ListOfIntegers, startingDirection);
            var plateauLimits = new Position(new []{5, 5});
            
            probe.MoveForward(plateauLimits);

            probe.PositionHistory.Last().XCoordinate.Should().Be(lastXCoordinate);
            probe.PositionHistory.Last().YCoordinate.Should().Be(lastYCoordinate);
        }
    }
}