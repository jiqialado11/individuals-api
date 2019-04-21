using FluentValidation;

namespace Individuals.Commands.PhoneNumber.DeletePhoneNumber
{
    public class DeletePhoneNumberCommandValidator:AbstractValidator<DeletePhoneNumberCommand>
    {
        public DeletePhoneNumberCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
