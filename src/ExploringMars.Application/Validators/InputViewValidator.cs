using ExploringMars.Application.Views.InputView;
using FluentValidation;

namespace ExploringMars.Application.Validators
{
    public class InputViewValidator : AbstractValidator<InputView>
    {
        private ProbeInputView ProbeInputView { get; set; }
        
        public InputViewValidator(ProbeInputView probeInputView)
        {
            ProbeInputView = probeInputView;
            RuleFor(inputView => inputView).Must(IsValidProbeInput);
        }

        private bool IsValidProbeInput(InputView inputView)
        {
            return inputView.PlateausMeasurement[0] >= ProbeInputView.Position[0] &&
                   inputView.PlateausMeasurement[1] >= ProbeInputView.Position[1];
        }
    }
}