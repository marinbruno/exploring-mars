using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploringMars.Domain.Entities
{
    public class Probe
    {
        public List<Position> PositionHistory { get; }
        
        public DirectionEnum CurrentDirection { get; set; }
        
        public Probe(IReadOnlyCollection<int> coordinates, string startingDirection)
        {
            PositionHistory = new List<Position> {new Position(coordinates)};
            
            Enum.TryParse(startingDirection, out DirectionEnum direction);
            CurrentDirection = direction;
        }

        public void MoveForward(Position plateauLimits)
        {
            var currentPosition = PositionHistory.Last();
            var newPosition = new Position(currentPosition);

            switch (CurrentDirection)
            {
                case DirectionEnum.N:
                    AddNewPosition(plateauLimits, newPosition, currentPosition, true);
                    break;
                case DirectionEnum.W:
                    AddNewPosition(plateauLimits, newPosition, currentPosition, false);
                    break;
                case DirectionEnum.S:
                    AddNewPosition(plateauLimits, newPosition, currentPosition, false);
                    break;
                case DirectionEnum.E:
                    AddNewPosition(plateauLimits, newPosition, currentPosition, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AddNewPosition(Position plateauLimits, Position newPosition, Position currentPosition, bool isMovingTowardsPlateauLimits)
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

            UpdatePositionHistory(newPosition.IsValid(plateauLimits) ? newPosition : currentPosition);
        }

        public void RotateLeft()
        {
            UpdateCurrentDirection(true);
        }

        public void RotateRight()
        {
            UpdateCurrentDirection(false);
        }

        private void UpdatePositionHistory(Position newPosition)
        {
            PositionHistory.Add(newPosition);
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