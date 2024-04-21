using Enquizitive.Common;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question.DomainEvents;

public record QuestionCreated(Guid Id, long Timestamp, string Text) : Event(Id, 1, Timestamp), IDomainEvent
{

}