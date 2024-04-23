using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record UpdateAnswerChoiceTextArgs(Guid AnswerId, string Text)
{
    public void Deconstruct(out Guid answerId, out string text)
    {
        answerId = AnswerId;
        text = Text;
    }
}

public class UpdateAnswerTextArgsValidator : AbstractValidator<UpdateAnswerChoiceTextArgs>
{
    public UpdateAnswerTextArgsValidator(List<AnswerChoice> answers)
    {
        RuleFor(x => x.AnswerId)
            .Must(x => answers.Any(y => y.Id == x))
            .WithMessage("Answer not found");
        RuleFor(x => x.AnswerId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}