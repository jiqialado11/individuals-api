using FluentValidation;

namespace Individuals.Commands.PhoneNumber.UpdatePhoneNumber
{
    public class UpdatePhoneNumberCommandValidator:AbstractValidator<UpdatePhoneNumberCommand>
    {
        public UpdatePhoneNumberCommandValidator()
        {
            RuleFor(x => x.PhoneNumberId)
                .NotEmpty()
                .WithMessage("Field is mandatory");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x => x.PhoneNumberType)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
