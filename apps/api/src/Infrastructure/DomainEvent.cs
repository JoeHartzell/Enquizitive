using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
using Enquizitive.Common;

namespace Enquizitive.Infrastructure;

[DynamoDBTable("Enquizitive")]
public class DomainEvent<TPayload> where TPayload : IDomainEvent
{
    [DynamoDBHashKey("pk")]
    public string Key { get; set; } = string.Empty;

    [DynamoDBRangeKey("sk")]
    public long SortKey { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    [DynamoDBIgnore]
    public TPayload Payload { get; set; }

    [DynamoDBProperty("eventType")]
    public string Type { get; set; }

    [DynamoDBProperty("payload")]
    public string SerializedPayload
    {
        get => JsonSerializer.Serialize(Payload);
        set => Payload = JsonSerializer.Deserialize<TPayload>(value);
    }
}
