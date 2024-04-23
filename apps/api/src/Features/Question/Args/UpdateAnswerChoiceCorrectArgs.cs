using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record UpdateAnswerChoiceCorrectArgs(bool IsCorrect)
{
    public void Deconstruct(out bool isCorrect)
    {
        isCorrect = IsCorrect;
    }
};

public class UpdateAnswerCorrectArgsValidator : AbstractValidator<UpdateAnswerChoiceCorrectArgs>
{
    public UpdateAnswerCorrectArgsValidator()
    {
        RuleFor(x => x.IsCorrect).NotEmpty();
    }
}