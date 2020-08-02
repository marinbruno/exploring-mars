using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploringMars.Domain.Entities
{
    public class Probe
    {
        public PositionHistory PositionHistory { get; } = new PositionHistory();
        
        public DirectionEnum CurrentDirection { get; set; }
        
        public Probe(IReadOnlyCollection<int> coordinates, string startingDirection)
        {
            PositionHistory.AddPosition(new Position(coordinates));
            
            Enum.TryParse(startingDirection, out DirectionEnum direction);
            CurrentDirection = direction;
        }

        public async Task MoveForward(Position plateauLimits)
        {
            var currentPosition = PositionHistory.Positions.Last();
            var newPosition = new Position(currentPosition);

            switch (CurrentDirection)
            {
                case DirectionEnum.N:
                    await AddNewPosition(plateauLimits, newPosition, currentPosition, true);
                    break;
                case DirectionEnum.W:
                    await AddNewPosition(plateauLimits, newPosition, currentPosition, false);
                    break;
                case DirectionEnum.S:
                    await AddNewPosition(plateauLimits, newPosition, currentPosition, false);
                    break;
                case DirectionEnum.E:
                    await AddNewPosition(plateauLimits, newPosition, currentPosition, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task AddNewPosition(Position plateauLimits, Position newPosition, Position currentPosition, bool isMovingTowardsPlateauLimits)
        {
            switch (CurrentDirection)
            {
                case DirectionEnum.N:
                case DirectionEnum.S:
                    newPosition.MoveWidthwise(isMovingTowardsPlateauLimits);
                    break;
                case DirectionEnum.E:
                case DirectionEnum.W:
                    newPosition.MoveLengthwise(isMovingTowardsPlateauLimits);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            PositionHistory.AddPosition(await newPosition.IsValid(plateauLimits) ? newPosition : currentPosition);
        }

        public void RotateLeft()
        {
            UpdateCurrentDirection(true);
        }

        public void RotateRight()
        {
            UpdateCurrentDirection(false);
        }

        private void UpdateCurrentDirection(bool isCounterClockwise)
        {
            CurrentDirection = CurrentDirection switch
            {
                DirectionEnum.N => isCounterClockwise ? DirectionEnum.W : DirectionEnum.E,
                DirectionEnum.W => isCounterClockwise ? DirectionEnum.S : DirectionEnum.N,
                DirectionEnum.S => isCounterClockwise ? DirectionEnum.E : DirectionEnum.W,
                DirectionEnum.E => isCounterClockwise ? DirectionEnum.N : DirectionEnum.S,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}