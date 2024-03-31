using Enquizitive.Common;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizCreated(
    Guid Id,
    string Name,
    string? Description
    ) : IDomainEvent { }