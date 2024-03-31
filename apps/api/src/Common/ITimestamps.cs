namespace Enquizitive.Common;

public interface ITimestamps
{
   DateTimeOffset CreatedAt { get; }
   DateTimeOffset UpdatedAt { get; }
}