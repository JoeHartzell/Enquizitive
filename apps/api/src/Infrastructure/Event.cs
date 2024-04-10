namespace Enquizitive.Infrastructure;

/// <summary>
/// Represents an event that is stored in the event store.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
/// <param name="Timestamp"></param>
public abstract record Event(Guid Id, int Version, long Timestamp) : IEventStoreRecord
{

}