using FluentValidation;

namespace Individuals.Commands.Images.AddImage
{
    public class AddImageCommandValidator:AbstractValidator<AddImageCommand>
    {
        public AddImageCommandValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.FileName)
                .NotEmpty()
                .WithMessage("Field is mandatory");
            RuleFor(x=>x.FileStream)
                .NotEmpty()
                .WithMessage("Field is mandatory");
        }
    }
}
