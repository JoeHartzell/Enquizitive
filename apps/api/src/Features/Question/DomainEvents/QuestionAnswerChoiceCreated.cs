using Enquizitive.Common;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question.DomainEvents;

public record QuestionAnswerChoiceCreated(
    Guid Id,
    int Version,
    long Timestamp,
    Guid AnswerId,
    string Text,
    bool IsCorrect,
    string? Rational
    ) : Event(Id, Version, Timestamp), IDomainEvent
{

}