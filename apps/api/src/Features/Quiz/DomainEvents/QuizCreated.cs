using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizCreated(
    Guid Id,
    string Name,
    string? Description
    ) : Event(Id, 1) { }
