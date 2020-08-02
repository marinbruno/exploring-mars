using System.Collections.Generic;

namespace ExploringMars.Domain.Entities
{
    public class PositionHistory
    {
        public List<Position> Positions { get; set; }

        public PositionHistory()
        {
            Positions = new List<Position>();
        }

        public void AddPosition(Position newPosition)
        {
            Positions.Add(newPosition);
        }
    }
}