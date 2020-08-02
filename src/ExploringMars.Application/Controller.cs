using System.Linq;
using ExploringMars.Application.Views;
using ExploringMars.Application.Views.InputView;
using ExploringMars.Domain;

namespace ExploringMars.Application
{
    public static class Controller
    {
        private static readonly InputView InputView = new InputView();
        
        private static readonly OutputView OutputView = new OutputView();

        private static readonly ProbePathCalculatorService ProbePathCalculatorService = new ProbePathCalculatorService();
        
        public static void GetProbesLandingPositions()
        {
            if (!InputView.PlateausMeasurement.Any())
            {
                InputView.AskUserForPlateausMeasurement();
            }

            while (InputView.ProbeInput.Count < 2)
            {
                if (InputView.ProbeInput.Count == InputView.InstructionsInput.Count)
                {
                    InputView.AskUserForProbesStartingSetup();
                }
                
                if (InputView.InstructionsInput.Count < 2)
                {
                    InputView.AskUserForProbesInstructions();
                }
            }
            
            var probesLandingPosition = ProbePathCalculatorService.CalculateProbesLandingPositions(InputView.PlateausMeasurement,
                    InputView.ProbeInput.First().Position, InputView.ProbeInput.First().Direction,
                    InputView.InstructionsInput.First(),
                    InputView.ProbeInput.Last().Position, InputView.ProbeInput.Last().Direction,
                    InputView.InstructionsInput.Last());
        
            OutputView.AnswerUserProbesLandingPositions(probesLandingPosition);
        }
    }
}