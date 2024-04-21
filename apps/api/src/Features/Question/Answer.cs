namespace Enquizitive.Features.Question;

public record Answer(Guid Id, string Text, bool IsCorrect, string? Rational)
{
}