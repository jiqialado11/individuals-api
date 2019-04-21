using FluentValidation;

namespace Individuals.Commands.Individual.DeleteIndividual
{
    public class DeleteIndividualCommandValidator:AbstractValidator<DeleteIndividualCommand>
    {
        public DeleteIndividualCommandValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            
        }
    }
}
