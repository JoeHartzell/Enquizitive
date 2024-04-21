using FluentValidation;

namespace Enquizitive.Features.Quiz.DTOs;

public sealed record CreateQuizRequest(string Name, string? Description = null)
{
}

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
