using FluentValidation;

namespace Individuals.Commands.PhoneNumber.AddPhoneNumber
{
    public class AddPhoneNumberCommandValidator:AbstractValidator<AddPhoneNumberCommand>
    {
        public AddPhoneNumberCommandValidator()
        {
            RuleFor(x => x.IndividualId)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.PhoneNumbers)
                .NotEmpty()
                .WithMessage("Request must contain minimum one phone number");
            RuleForEach(x => x.PhoneNumbers)
                .SetValidator(new AddPhoneNumberCommandItemValidator());


        }
    }

    public class AddPhoneNumberCommandItemValidator : AbstractValidator<AddPhoneNumberCommandItem>
    {
        public AddPhoneNumberCommandItemValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is mandatory");
            RuleFor(x => x.PhoneNumberType)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
