using System;
using System.Collections.Generic;
using System.Linq;
using ExploringMars.Domain.Entities;

namespace ExploringMars.Domain.Services
{
    public class ProbePathCalculatorService
    {
        public List<List<string>> CalculateProbesLandingPositions(List<int> plateausMeasurement,
            List<int> firstProbesStartingPosition, string firstProbesStartingDirection,
            List<string> firstProbesInstructions, List<int> secondProbesStartingPosition,
            string secondProbesStartingDirection, List<string> secondProbesInstructions)
        {
            var plateauLimits = new Position(plateausMeasurement);
            var firstProbe = new Probe(firstProbesStartingPosition, firstProbesStartingDirection,
                firstProbesInstructions);
            var secondProbe = new Probe(secondProbesStartingPosition, secondProbesStartingDirection,
                secondProbesInstructions);

            firstProbe.Validate();
            secondProbe.Validate();

            return new List<List<string>>
            {
                CalculateProbesPath(firstProbe, plateauLimits),
                CalculateProbesPath(firstProbe, plateauLimits)
            };
        }

        private List<string> CalculateProbesPath(Probe probe, Position plateauLimits)
        {
            foreach (var instruction in probe.Instructions)
            {
                switch (instruction)
                {
                    case "M":
                        probe.MoveForward(plateauLimits);
                        break;
                    case "L":
                        probe.RotateLeft();
                        break;
                    case "R":
                        probe.RotateRight();
                        break;
                }
            }
            
            return new List<string>
            {
                probe.Positions.Last().XCoordinate.ToString(),
                probe.Positions.Last().YCoordinate.ToString(),
                nameof(probe.CurrentDirection)
            };
        }
    }
}