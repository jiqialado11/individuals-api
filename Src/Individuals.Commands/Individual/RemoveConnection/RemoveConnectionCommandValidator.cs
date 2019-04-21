using FluentValidation;

namespace Individuals.Commands.Individual.RemoveConnection
{
    public class RemoveConnectionCommandValidator:AbstractValidator<RemoveConnectionCommand>
    {
        public RemoveConnectionCommandValidator()
        {
            RuleFor(x=>x.ConnectedFromIndividualId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.ConnectedToIndividualId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
