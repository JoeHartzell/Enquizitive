using FluentValidation;

namespace Enquizitive.Features.Question.DTOs;

public sealed record CreateQuestionRequest(string Text)
{
    
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(x => x.Text).NotEmpty();
    }
}