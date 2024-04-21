using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizQuestionCreated(Guid Id, int Version, long Timestamp) 
    : Event(Id, Version, Timestamp), IQuizDomainEvent
{

}