using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ExploringMars.Application.Exceptions;
using ExploringMars.Application.Validators;

namespace ExploringMars.Application.Views.InputView
{
    public class InputView
    {
        public virtual List<int> PlateausMeasurement { get; set; } = new List<int>();
        
        public virtual List<ProbeInputView> ProbeInput { get; set; } = new List<ProbeInputView>();
        
        public virtual List<List<string>> InstructionsInput { get; set; } = new List<List<string>>();

        private const string PlateausMeasurementRegexPattern = @"^(?<length>\d+)\s(?<width>\d+)$";
        private const string ProbesStartingSetupRegexPattern = @"^(?<XCoordinate>\d+)\s(?<YCoordinate>\d+)\s(?<direction>[NESW])$";
        private const string ProbesInstructionsRegexPattern = @"^(?<instructions>[LRM]+)$";

        private readonly IConsoleReader _consoleReader;

        public InputView(IConsoleReader consoleReader)
        {
            _consoleReader = consoleReader;
        }

        public InputView()
        {
            
        }

        public virtual void AskUserForPlateausMeasurement()
        {
            Console.WriteLine("Please, enter the length and the width of the plateau as the following example: > 5 5");
            var plateausMeasurementInput = _consoleReader.GetUserInput();

            ValidateInput(plateausMeasurementInput, PlateausMeasurementRegexPattern, out var regexMatch);

            int.TryParse(regexMatch.Groups["length"].Value, out var length);
            int.TryParse(regexMatch.Groups["width"].Value, out var width);
                
            PlateausMeasurement.Add(length);
            PlateausMeasurement.Add(width);
        }

        public virtual async Task AskUserForProbesStartingSetup()
        {
            var probeOrder = ProbeInput.Count < 1 ? "first" : "second";
            
            Console.WriteLine($"Please, enter the {probeOrder} probe's starting setup, that is, its position (X, Y) and its direction as the following example: > 1 2 N");
            var probesStartingSetupInput = _consoleReader.GetUserInput();

            ValidateInput(probesStartingSetupInput, ProbesStartingSetupRegexPattern, out var regexMatch);
            
            int.TryParse(regexMatch.Groups["XCoordinate"].Value, out var xCoordinate);
            int.TryParse(regexMatch.Groups["YCoordinate"].Value, out var yCoordinate);
            var direction = regexMatch.Groups["direction"].Value;
            
            var probeInput = new ProbeInputView
            {
                Position = new List<int> {xCoordinate, yCoordinate},
                Direction = direction
            };
            
            await ValidateProbeInput(probeInput);
            
            ProbeInput.Add(probeInput);
        }

        public virtual void AskUserForProbesInstructions()
        {
            var probeOrder = ProbeInput.Count < 1 ? "first" : "second";
            
            Console.WriteLine($"Now, enter the {probeOrder} probe's instructions.");
            var probesInstructionsInput = _consoleReader.GetUserInput();

            ValidateInput(probesInstructionsInput, ProbesInstructionsRegexPattern, out var regexMatch);

            var instructions = regexMatch.Groups["instructions"].Value;
            InstructionsInput.Add(instructions.Select(c => c.ToString()).ToList());
        }

        public void ValidateInput(string userInput, string regexPattern, out Match regexMatch)
        {
            var regex = new Regex(regexPattern);
            regexMatch = regex.Match(userInput);

            if (string.IsNullOrEmpty(userInput) || regexMatch.Success == false)
            {
                throw new InvalidInputException();
            }
        }

        private async Task ValidateProbeInput(ProbeInputView probeInput)
        {
            var validationResult = await new InputViewValidator(probeInput).ValidateAsync(this);

            if (!validationResult.IsValid)
            {
                throw new InvalidInputException();
            }
        }

        public virtual int CountProbeInputs()
        {
            return ProbeInput.Count;
        }

        public virtual int CountInstructionInputs()
        {
            return InstructionsInput.Count;
        }

        public virtual bool HasPlateausMeasurement()
        {
            return PlateausMeasurement.Any();
        }
    }
}