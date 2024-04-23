using FluentValidation;

namespace Enquizitive.Features.Question.Args;

public record CreateQuestionArgs(string Text)
{
}

public class CreateQuestionArgsValidator : AbstractValidator<CreateQuestionArgs>
{
   public CreateQuestionArgsValidator()
   {
      RuleFor(x => x.Text).NotEmpty();
   }
}