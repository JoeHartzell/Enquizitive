namespace Enquizitive.Infrastructure;

/// <summary>
/// Represents a snapshot that is stored in the event store.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
/// <param name="Timestamp"></param>
/// <param name="Data"></param>
/// <typeparam name="T"></typeparam>
public abstract record Snapshot<T>(Guid Id, int Version, long Timestamp, T Data) : IEventStoreRecord
{

}