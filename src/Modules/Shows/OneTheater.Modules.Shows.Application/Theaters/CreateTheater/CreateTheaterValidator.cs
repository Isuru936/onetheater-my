using FluentValidation;

namespace OneTheater.Modules.Shows.Application.Theaters.CreateTheater;
internal sealed class CreateTheaterValidator : AbstractValidator<CreateTheaterCommand>
{
    public CreateTheaterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Theater name cannot be empty")
            .MaximumLength(100);
    }
}
