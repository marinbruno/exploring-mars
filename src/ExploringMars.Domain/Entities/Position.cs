using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploringMars.Domain.Validators;

namespace ExploringMars.Domain.Entities
{
    public class Position
    {
        public int XCoordinate { get; private set; }

        public int YCoordinate { get; private set; }

        public Position(IReadOnlyCollection<int> coordinates)
        {
            XCoordinate = coordinates.First();
            YCoordinate = coordinates.Last();
        }

        public Position(Position position)
        {
            XCoordinate = position.XCoordinate;
            YCoordinate = position.YCoordinate;
        }

        public async Task<bool> IsValid(Position upperPlateauLimits)
        {
            var validationResult = await new PositionValidator(upperPlateauLimits).ValidateAsync(this);

            return validationResult.IsValid;
        }

        public void MoveLengthwise(bool isMovingTowardsPlateauLimits)
        {
            XCoordinate += isMovingTowardsPlateauLimits ? +1 : -1;
        }

        public void MoveWidthwise(bool isMovingTowardsPlateauLimits)
        {
            YCoordinate += isMovingTowardsPlateauLimits ? +1 : -1;
        }
    }
}