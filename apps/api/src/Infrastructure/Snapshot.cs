using Enquizitive.Common;

namespace Enquizitive.Infrastructure;

/// <summary>
/// A specific type of EventStoreRecord that represents a snapshot of an aggregate.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
/// <param name="State"></param>
/// <typeparam name="T"></typeparam>
public record Snapshot(Guid Id, int Version) : ISnapshot
{
    /// <summary>
    /// Timestamp when the snapshot was taken.
    /// </summary>
    public long Timestamp { get; init; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}