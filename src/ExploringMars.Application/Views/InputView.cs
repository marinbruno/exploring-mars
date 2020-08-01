using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ExploringMars.Application.Dtos;
using ExploringMars.Application.Exceptions;

namespace ExploringMars.Application.Views
{
    public class InputView
    {
        private const string PlateausMeasurementRegexPattern = @"^(?<length>\d)\s(?<width>\d)$";

        private const string ProbesStartingSetupRegexPattern = @"^(?<XCoordinate>\d)\s(?<YCoordinate>\d)\s(?<direction>[NESW])$";
        
        private const string ProbesInstructionsRegexPattern = @"^(?<instructions>[LRM]+)$";
        
        public static List<int> AskUserForPlateausMeasurement()
        {
            Console.WriteLine("Please, enter the length and the width of the plateau as the following example: > 5 5");
            var plateausMeasurementInput = Console.ReadLine();
            
            
            var regexMatch = GetRegexMatch(plateausMeasurementInput, PlateausMeasurementRegexPattern);

            Int32.TryParse(regexMatch.Groups["length"].Value, out var length);
            Int32.TryParse(regexMatch.Groups["width"].Value, out var width);
                
            return new List<int>
            {
                length,
                width
            };
        }

        public ProbesStartingSetupDto AskUserForProbesStartingSetup()
        {
            Console.WriteLine("Please, enter the probe's starting setup, that is, its position (X, Y) and its direction as the following example: > 1 2 N");
            var probesStartingSetupInput = Console.ReadLine();

            var regexMatch = GetRegexMatch(probesStartingSetupInput, ProbesStartingSetupRegexPattern);
            
            Int32.TryParse(regexMatch.Groups["XCoordinate"].Value, out var XCoordinate);
            Int32.TryParse(regexMatch.Groups["YCoordinate"].Value, out var YCoordinate);
            var direction = regexMatch.Groups["direction"].Value;
            
            return new ProbesStartingSetupDto
            {
                Position = new List<int>
                {
                    XCoordinate,
                    YCoordinate
                },
                Direction = direction
            };
        }

        public List<string> AskUserForProbesInstructions()
        {
            Console.WriteLine("Now, enter the probe's instructions.");
            var probesInstructionsInput = Console.ReadLine();

            var regexMatch = GetRegexMatch(probesInstructionsInput, ProbesInstructionsRegexPattern);

            var instructions = regexMatch.Groups["instructions"].Value;
            return instructions.Select(c => c.ToString()).ToList();
        }

        public void AnswerUserProbesLandingPositions(List<List<string>> probesLandingPositions)
        {
            Console.WriteLine("You did not enter a valid input.");
            AskUserForPlateausMeasurement();
        }
        
        
        private static Match GetRegexMatch(string userInput, string regexPattern)
        {
            var regex = new Regex(regexPattern);

            if (string.IsNullOrEmpty(userInput))
            {
                switch (regexPattern)
                {
                    case PlateausMeasurementRegexPattern:
                        throw new InvalidPlateausMeasurementInputException();
                    case ProbesStartingSetupRegexPattern:
                        throw new InvalidProbesStartingSetupInputException();
                    case ProbesInstructionsRegexPattern:
                        throw new InvalidProbesInstructionsExceptions();
                }
            }
            
            var regexMatch = regex.Match(userInput);
            return regexMatch;
        }
    }
}