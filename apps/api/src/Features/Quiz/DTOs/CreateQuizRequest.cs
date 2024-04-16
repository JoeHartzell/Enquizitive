using FluentValidation;

namespace Enquizitive.Features.Quiz.DTOs;

public sealed class CreateQuizRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
