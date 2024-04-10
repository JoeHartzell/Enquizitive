using Enquizitive.Features.Quiz.DTOs;
using FluentValidation;

namespace Enquizitive.Features.Quiz.Validators;

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}