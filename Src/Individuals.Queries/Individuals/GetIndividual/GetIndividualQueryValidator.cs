using FluentValidation;

namespace Individuals.Queries.Individuals.GetIndividual
{
    public class GetIndividualQueryValidator:AbstractValidator<GetIndividualQuery>
    {
        public GetIndividualQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
