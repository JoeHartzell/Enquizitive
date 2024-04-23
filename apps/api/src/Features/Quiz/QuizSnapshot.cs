using Enquizitive.Infrastructure;

namespace Enquizitive.Features.Quiz;

public record QuizSnapshot(
    Guid Id,
    int Version,
    string Name,
    string Description,
    DateTimeOffset CreatedAt
    ) : Snapshot(Id, Version)
{

}