using Enquizitive.Common;
using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz.DomainEvents;

public record QuizCreated(
    Guid Id,
    int Version,
    long Timestamp,
    string Name,
    string? Description
    ) : Event(Id, Version, Timestamp), IQuizDomainEvent { }