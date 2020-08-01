using System;
using System.Collections.Generic;
using ExploringMars.Application.Dtos;
using ExploringMars.Application.Exceptions;
using ExploringMars.Application.Views;
using ExploringMars.Domain;

namespace ExploringMars.Application
{
    public class Controller
    {
        private static readonly InputView InputView = new InputView();
        
        private static readonly OutputView OutputView = new OutputView();

        private static readonly ProbePathCalculatorService ProbePathCalculatorService = new ProbePathCalculatorService();
        
        public static void GetProbesLandingPositions()
        {
            try
            {
                var plateausMeasurement = InputView.AskUserForPlateausMeasurement();
            
                var firstProbesStartingSetup = GetProbesUserData(out var firstProbesInstructions);
                var secondProbesStartingSetup = GetProbesUserData(out var secondProbesInstructions);

                var probesLandingPosition = ProbePathCalculatorService.CalculateProbesLandingPositions(plateausMeasurement,
                    firstProbesStartingSetup.Position, firstProbesStartingSetup.Direction, firstProbesInstructions,
                    secondProbesStartingSetup.Position, secondProbesStartingSetup.Direction, secondProbesInstructions);
            
                OutputView.AnswerUserProbesLandingPositions(probesLandingPosition);
            }
            catch (InvalidInputException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static ProbesStartingSetupDto GetProbesUserData(out List<string> firstProbesInstructions)
        {
            var firstProbesStartingSetup = InputView.AskUserForProbesStartingSetup();
            
            firstProbesInstructions = InputView.AskUserForProbesInstructions();
            return firstProbesStartingSetup;
        }
    }
}