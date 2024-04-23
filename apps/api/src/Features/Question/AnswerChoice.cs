namespace Enquizitive.Features.Question;

public record AnswerChoice(Guid Id, string Text, bool IsCorrect, string? Rational)
{
}