using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizNameUpdated(Guid Id, int Version, long Timestamp, string Name) 
    : Event(Id, Version, Timestamp), IQuizDomainEvent
{
    
}