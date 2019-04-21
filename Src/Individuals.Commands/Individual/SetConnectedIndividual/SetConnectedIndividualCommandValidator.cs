using FluentValidation;

namespace Individuals.Commands.Individual.SetConnectedIndividual
{
    public class SetConnectedIndividualCommandValidator:AbstractValidator<SetConnectedIndividualCommand>
    {
        public SetConnectedIndividualCommandValidator()
        {
            RuleFor(x=>x.ConnectedFromIndividualId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.ConnectedToIndividualId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.ConnectionType)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
