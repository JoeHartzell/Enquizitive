using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record UpdateAnswerChoiceRationalArgs(string? Rational)
{
   public void Deconstruct(out string? rational)
   {
      rational = Rational;
   } 
}

public class UpdateAnswerRationalArgsValidator : AbstractValidator<UpdateAnswerChoiceRationalArgs>
{
   public UpdateAnswerRationalArgsValidator()
   {
      RuleFor(x => x.Rational)
         .NotEmpty()
         .When(x => x.Rational is not null);
   }
}