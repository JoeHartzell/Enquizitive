using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
namespace Enquizitive.Infrastructure;

[DynamoDBTable("Enquizitive")]
public class EventStoreRecord<TData> where TData : IEventStoreRecordData
{
    /// <summary>
    /// The primary key of the record.
    /// <example>Quiz#123</example>
    /// </summary>
    [DynamoDBHashKey("pk")]
    public required string Key { get; set; }

    /// <summary>
    /// The sort key of the record.
    /// <example>Event#version/timestamp</example>
    /// </summary>
    [DynamoDBRangeKey("sk")]
    public required string SortKey { get; set; }

    [DynamoDBProperty("timestamp")]
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    [DynamoDBProperty("version")]
    public int? Version { get; set; } = 0;

    [DynamoDBProperty("type")]
    public required string Type { get; set; }

    [DynamoDBProperty("data")]
    public string SerializedData
    {
        get => JsonSerializer.Serialize(Data);
        set => Data = JsonSerializer.Deserialize<TData>(value);
    }

    [DynamoDBIgnore]
    public required TData Data { get; set; }
}
