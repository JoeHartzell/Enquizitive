using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question.DomainEvents;

public record QuestionAnswerChoiceCreated(
    Guid Id,
    int Version,
    Guid AnswerId,
    string Text,
    bool IsCorrect,
    string? Rational
    ) : Event(Id, Version)
{

}