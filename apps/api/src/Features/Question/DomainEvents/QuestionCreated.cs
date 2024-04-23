using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Question.DomainEvents;

public record QuestionCreated(Guid Id, string Text) : Event(Id, 1)
{

}