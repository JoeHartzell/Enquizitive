using Enquizitive.Common;

namespace Enquizitive.Infrastructure;

/// <summary>
/// A specific type of EventStoreRecord that represents a snapshot of an aggregate.
/// </summary>
/// <param name="Id"></param>
/// <param name="Version"></param>
/// <param name="Timestamp"></param>
/// <param name="Data"></param>
/// <typeparam name="T"></typeparam>
public abstract record Snapshot<T>(Guid Id, int Version, long Timestamp, T Data) : IEventStoreRecordData 
{

}