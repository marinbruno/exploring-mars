using System.Linq;
using System.Threading.Tasks;
using ExploringMars.Application.Views;
using ExploringMars.Application.Views.InputView;
using ExploringMars.Domain;

namespace ExploringMars.Application
{
    public class Controller
    {
        private readonly InputView _inputView;

        private static readonly ProbePathCalculatorService ProbePathCalculatorService = new ProbePathCalculatorService();
        
        public Controller(IConsoleReader consoleReader, InputView inputView = null)
        {
            _inputView = inputView ?? new InputView(consoleReader);
        }

        public async Task GetProbesLandingPositions()
        {
            if (!_inputView.HasPlateausMeasurement())
            {
                _inputView.AskUserForPlateausMeasurement();
            }

            while (_inputView.CountProbeInputs() < 2)
            {
                if (_inputView.CountProbeInputs() == _inputView.CountInstructionInputs())
                {
                    await _inputView.AskUserForProbesStartingSetup();
                }
                
                if (_inputView.CountInstructionInputs() < 2)
                {
                    _inputView.AskUserForProbesInstructions();
                }
            }
            
            var probesLandingPosition = await ProbePathCalculatorService.CalculateProbesLandingPositions(_inputView.PlateausMeasurement,
                    _inputView.ProbeInput.First().Position, _inputView.ProbeInput.First().Direction,
                    _inputView.InstructionsInput.First(),
                    _inputView.ProbeInput.Last().Position, _inputView.ProbeInput.Last().Direction,
                    _inputView.InstructionsInput.Last());
        
            OutputView.AnswerUserProbesLandingPositions(probesLandingPosition);
        }
    }
}