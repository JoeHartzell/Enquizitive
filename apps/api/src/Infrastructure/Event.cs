namespace Enquizitive.Infrastructure;

/// <summary>
/// A specific type of EventStoreRecord that represents a domain event.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
/// <param name="Timestamp"></param>
public abstract record Event(Guid Id, int Version, long Timestamp) : IEventStoreRecordData
{

}