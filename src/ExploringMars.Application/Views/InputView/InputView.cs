using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExploringMars.Application.Exceptions;

namespace ExploringMars.Application.Views
{
    public class InputView
    {
        public List<int> PlateausMeasurement { get; set; } = new List<int>();
        
        public List<ProbeInputView> ProbeInput { get; set; } = new List<ProbeInputView>();
        
        public List<List<string>> InstructionsInput { get; set; } = new List<List<string>>();

        private const string PlateausMeasurementRegexPattern = @"^(?<length>\d)\s(?<width>\d)$";

        private const string ProbesStartingSetupRegexPattern = @"^(?<XCoordinate>\d)\s(?<YCoordinate>\d)\s(?<direction>[NESW])$";
        
        private const string ProbesInstructionsRegexPattern = @"^(?<instructions>[LRM]+)$";

        public void AskUserForPlateausMeasurement()
        {
            Console.WriteLine("Please, enter the length and the width of the plateau as the following example: > 5 5");
            var plateausMeasurementInput = Console.ReadLine();

            ValidateInput(plateausMeasurementInput, PlateausMeasurementRegexPattern, out var regexMatch);

            int.TryParse(regexMatch.Groups["length"].Value, out var length);
            int.TryParse(regexMatch.Groups["width"].Value, out var width);
                
            PlateausMeasurement.Add(length);
            PlateausMeasurement.Add(width);
        }

        public void AskUserForProbesStartingSetup(bool isFirstProbe)
        {
            var probeOrder = isFirstProbe ? "first" : "second";
            
            Console.WriteLine($"Please, enter the {probeOrder} probe's starting setup, that is, its position (X, Y) and its direction as the following example: > 1 2 N");
            var probesStartingSetupInput = Console.ReadLine();

            ValidateInput(probesStartingSetupInput, ProbesStartingSetupRegexPattern, out var regexMatch);
            
            // validar se coordenadas são validas dado o plateausMeasurement inputado
            // se não, throw exception
            int.TryParse(regexMatch.Groups["XCoordinate"].Value, out var xCoordinate);
            int.TryParse(regexMatch.Groups["YCoordinate"].Value, out var yCoordinate);
            var direction = regexMatch.Groups["direction"].Value;
            
            var probeInput = new ProbeInputView
            {
                Position = new List<int> {xCoordinate, yCoordinate},
                Direction = direction
            };
            
            ProbeInput.Add(probeInput);
        }

        public void AskUserForProbesInstructions(bool isFirstProbe)
        {
            var probeOrder = isFirstProbe ? "first" : "second";
            
            Console.WriteLine($"Now, enter the {probeOrder} probe's instructions.");
            var probesInstructionsInput = Console.ReadLine();

            ValidateInput(probesInstructionsInput, ProbesInstructionsRegexPattern, out var regexMatch);

            var instructions = regexMatch.Groups["instructions"].Value;
            InstructionsInput.Add(instructions.Select(c => c.ToString()).ToList());
        }


        private static void ValidateInput(string userInput, string regexPattern, out Match regexMatch)
        {
            var regex = new Regex(regexPattern);
            regexMatch = regex.Match(userInput);

            if (string.IsNullOrEmpty(userInput) || regexMatch.Success == false)
            {
                ThrowInvalidInputException(regexPattern);
            }
        }

        private static void ThrowInvalidInputException(string regexPattern)
        {
            switch (regexPattern)
            {
                case PlateausMeasurementRegexPattern:
                    throw new InvalidPlateausMeasurementInputException();
                case ProbesStartingSetupRegexPattern:
                    throw new InvalidProbesStartingSetupInputException();
                case ProbesInstructionsRegexPattern:
                    throw new InvalidProbesInstructionsInputException();
            }
        }
    }
}