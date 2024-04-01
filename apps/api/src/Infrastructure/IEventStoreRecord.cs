namespace Enquizitive.Infrastructure;

public interface IEventStoreRecord
{
   /// <summary>
   /// Aggregate identifier.
   /// </summary>
   Guid Id { get; } 
   
   /// <summary>
   /// Aggregate version.
   /// </summary>
   int Version { get; }
   
   /// <summary>
   /// Timestamp of the record.
   /// </summary>
   long Timestamp { get; }
}