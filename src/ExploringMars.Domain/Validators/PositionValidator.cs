using ExploringMars.Domain.Entities;
using FluentValidation;

namespace ExploringMars.Domain.Validators
{
    public class PositionValidator : AbstractValidator<Position>
    {
        private Position UpperPlateauLimits { get; }

        private Position LowerPlateauLimits { get; } = new Position(new []{0, 0});

        public PositionValidator(Position upperPlateauLimits)
        {
            UpperPlateauLimits = upperPlateauLimits;
            RuleFor(desiredPosition => desiredPosition).Must(IsValidDesiredPosition);
        }

        private bool IsValidDesiredPosition(Position desiredPosition)
        {
            return desiredPosition.XCoordinate <= UpperPlateauLimits.XCoordinate &&
                   desiredPosition.YCoordinate <= UpperPlateauLimits.YCoordinate &&
                   desiredPosition.XCoordinate >= LowerPlateauLimits.XCoordinate &&
                   desiredPosition.YCoordinate >= LowerPlateauLimits.YCoordinate;
        }
    }
}