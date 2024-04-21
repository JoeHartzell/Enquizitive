using Enquizitive.Common;

namespace Enquizitive.Features.Question.DomainEvents;

public record QuestionAnswerTextUpdated(
    Guid Id,
    int Version,
    long Timestamp,
    Guid AnswerId,
    string Text) : IDomainEvent
{

}