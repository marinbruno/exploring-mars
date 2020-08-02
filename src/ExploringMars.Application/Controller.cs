using System;
using System.Linq;
using ExploringMars.Application.Views;
using ExploringMars.Application.Views.InputView;
using ExploringMars.Domain;

namespace ExploringMars.Application
{
    public class Controller
    {
        private static readonly OutputView OutputView = new OutputView();

        private static readonly ProbePathCalculatorService ProbePathCalculatorService = new ProbePathCalculatorService();

        private readonly IConsoleReader _consoleReader;
        
        private readonly InputView _inputView;

        public Controller(IConsoleReader consoleReader)
        {
            _consoleReader = consoleReader;
            _inputView = new InputView(_consoleReader);
        }
        
        public void GetProbesLandingPositions()
        {
            if (!_inputView.PlateausMeasurement.Any())
            {
                _inputView.AskUserForPlateausMeasurement();
            }

            while (_inputView.ProbeInput.Count < 2)
            {
                if (_inputView.ProbeInput.Count == _inputView.InstructionsInput.Count)
                {
                    _inputView.AskUserForProbesStartingSetup();
                }
                
                if (_inputView.InstructionsInput.Count < 2)
                {
                    _inputView.AskUserForProbesInstructions();
                }
            }
            
            var probesLandingPosition = ProbePathCalculatorService.CalculateProbesLandingPositions(_inputView.PlateausMeasurement,
                    _inputView.ProbeInput.First().Position, _inputView.ProbeInput.First().Direction,
                    _inputView.InstructionsInput.First(),
                    _inputView.ProbeInput.Last().Position, _inputView.ProbeInput.Last().Direction,
                    _inputView.InstructionsInput.Last());
        
            OutputView.AnswerUserProbesLandingPositions(probesLandingPosition);
        }
    }
}