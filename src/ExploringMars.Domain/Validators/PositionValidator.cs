using ExploringMars.Domain.Entities;
using FluentValidation;

namespace ExploringMars.Domain.Validators
{
    public class NewPositionValidator : AbstractValidator<Position>
    {
        private Position UpperPlateauLimits { get; set; }

        private Position LowerPlateauLimits { get; set; } = new Position(new []{0, 0});

        public NewPositionValidator(Position upperPlateauLimits)
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