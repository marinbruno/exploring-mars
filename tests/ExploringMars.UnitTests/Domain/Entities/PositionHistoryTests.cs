using System.Collections.Generic;
using ExploringMars.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ExploringMars.UnitTests.Domain.Entities
{
    public class PositionHistoryTests
    {
        [Fact]
        public void AddPosition_GivenValidPosition_ShouldAddANewPositionToPositionHistory()
        {
            var positionHistory = new PositionHistory();
            
            positionHistory.AddPosition(new Position(new List<int>{5, 5}));

            positionHistory.Positions.Count.Should().Be(1);
        }
    }
}