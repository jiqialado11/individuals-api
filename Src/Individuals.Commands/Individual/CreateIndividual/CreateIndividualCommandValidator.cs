using FluentValidation;

namespace Individuals.Commands.Individual.CreateIndividual
{
    public class CreateIndividualCommandValidator:AbstractValidator<CreateIndividualCommand>
    {
        public CreateIndividualCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.LastName)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.PersonalNumber)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.CityId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.Gender)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
