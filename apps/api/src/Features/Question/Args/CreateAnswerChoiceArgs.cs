using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record CreateAnswerChoiceArgs(string Text, bool IsCorrect, string? Rational)
{
   public void Deconstruct(out string text, out bool isCorrect, out string? rational)
   {
      text = Text;
      isCorrect = IsCorrect;
      rational = Rational;
   }
}

public class CreateAnswerArgsValidator : AbstractValidator<CreateAnswerChoiceArgs>
{
   public CreateAnswerArgsValidator()
   {
      RuleFor(x => x.Text).NotEmpty();
      RuleFor(x => x.Rational)
         .NotEmpty()
         .When(x => x.Rational is not null);
   }
}