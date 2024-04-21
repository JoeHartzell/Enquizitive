using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record CreateAnswerArgs(string Text, bool IsCorrect, string? Rational)
{
   public void Deconstruct(out string text, out bool isCorrect, out string? rational)
   {
      text = Text;
      isCorrect = IsCorrect;
      rational = Rational;
   }
}

public class CreateAnswerArgsValidator : AbstractValidator<CreateAnswerArgs>
{
   public CreateAnswerArgsValidator()
   {
      RuleFor(x => x.Text).NotEmpty();
   }
}