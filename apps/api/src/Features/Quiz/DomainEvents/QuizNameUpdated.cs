using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizNameUpdated(Guid Id, int Version, string Name) 
    : Event(Id, Version) 
{
    
}