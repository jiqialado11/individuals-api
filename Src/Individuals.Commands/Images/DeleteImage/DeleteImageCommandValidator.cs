using FluentValidation;

namespace Individuals.Commands.Images.DeleteImage
{
    public class DeleteImageCommandValidator:AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");

        }
    }
}
