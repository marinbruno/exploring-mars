using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploringMars.Domain.Entities;

namespace ExploringMars.Domain
{
    public class ProbePathCalculatorService
    {
        public virtual async Task<List<List<string>>> CalculateProbesLandingPositions(List<int> plateausMeasurement,
            List<int> firstProbesStartingPosition, string firstProbesStartingDirection,
            List<string> firstProbesInstructions, List<int> secondProbesStartingPosition,
            string secondProbesStartingDirection, List<string> secondProbesInstructions)
        {
            var plateauLimits = new Position(plateausMeasurement);
            var firstProbe = new Probe(firstProbesStartingPosition, firstProbesStartingDirection);
            var secondProbe = new Probe(secondProbesStartingPosition, secondProbesStartingDirection);

            await CalculateProbesPath(firstProbe, plateauLimits, firstProbesInstructions);
            await CalculateProbesPath(secondProbe, plateauLimits, secondProbesInstructions);

            return new List<List<string>>
            {
                GetProbesLandingPositionsAndDirection(firstProbe),
                GetProbesLandingPositionsAndDirection(secondProbe)
            };
        }

        private async Task CalculateProbesPath(Probe probe, Position plateauLimits, List<string> probesInstructions)
        {
            foreach (var instruction in probesInstructions)
            {
                switch (instruction)
                {
                    case "M":
                        await probe.MoveForward(plateauLimits);
                        break;
                    case "L":
                        probe.RotateLeft();
                        break;
                    case "R":
                        probe.RotateRight();
                        break;
                }
            }
        }

        private List<string> GetProbesLandingPositionsAndDirection(Probe probe)
        {
            return new List<string>
            {
                probe.PositionHistory.Positions.Last().XCoordinate.ToString(),
                probe.PositionHistory.Positions.Last().YCoordinate.ToString(),
                probe.CurrentDirection.ToString()
            };
        }
    }
}