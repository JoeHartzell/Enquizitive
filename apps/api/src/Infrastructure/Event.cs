using Enquizitive.Common;

namespace Enquizitive.Infrastructure;

/// <summary>
/// A specific type of EventStoreRecord that represents a domain event.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
public abstract record Event(Guid Id, int Version) : IDomainEvent
{
    public long Timestamp { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}