using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizQuestionCreated(Guid Id, int Version) 
    : Event(Id, Version) 
{

}