using FluentValidation;

namespace Individuals.Commands.Individual.UpdateIndividual
{
    public class UpdateIndividualCommandValidator:AbstractValidator<UpdateIndividualCommand>
    {
        public UpdateIndividualCommandValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.PersonalNumber)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.CityId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
